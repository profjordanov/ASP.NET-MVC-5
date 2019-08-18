//-----------------------------------------------------------------------------
//
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
//
//-----------------------------------------------------------------------------

using System;
using System.Globalization;
using System.IdentityModel.Tokens;
using System.Net;
using System.Threading;
using System.Web;
using Microsoft.IdentityModel.Claims;
using Microsoft.IdentityModel.Configuration;
using Microsoft.IdentityModel.Protocols.OAuth;
using Microsoft.IdentityModel.Web;
using CustomerInformationService;

namespace ODataServer
{
    /// <summary>
    /// This class is an IHttpModule is used to check the access token on every incoming request to the site.
    /// </summary>
    public class OAuthProtectionModule : IHttpModule
    {
        static string realm = "DirectoryService";
        static string errorMessageIdentifier = "errorMessage";
        ServiceConfiguration _serviceConfiguration;

        /// <summary>
        /// Gets or sets the <see cref="ServiceConfiguration"/> in effect for this module.
        /// </summary>
        /// <value>The <see cref="ServiceConfiguration"/> instance for this class.</value>
        public ServiceConfiguration ServiceConfiguration
        {
            get
            {
                return _serviceConfiguration;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                _serviceConfiguration = value;
            }
        }

        /// <summary>
        /// This method is used to do all the initialization for this class.
        /// </summary>
        /// <param name="context">The <see cref="HttpApplication"/> object which contains this module.</param>
        public void Init(HttpApplication context)
        {
            _serviceConfiguration = FederatedAuthentication.ServiceConfiguration;
            InitializeModule(context);
        }
        
        /// <summary>
        /// Initializes the module and prepares it to handle events from the module's ASP.NET application
        /// object.
        /// </summary>
        /// <param name="context">The <see cref="HttpApplication"/> object which contains this module.</param>
        /// <remarks>
        /// Use the InitializeModule method to load any additional configuration state as well as 
        /// register event handling methods with specific events raised by the HttpApplication.
        /// </remarks>
        protected void InitializeModule(HttpApplication context)
        {
            context.AuthenticateRequest += OnAuthenticateRequest;
            context.PostAuthenticateRequest += OnPostAuthenticateRequest;
            context.EndRequest += OnEndRequest;            
        }

        /// <summary>
        /// Handle the HTTP pipeline AuthenticateRequest event, after ensuring that the module has been initialized.
        /// </summary>       
        /// <param name="sender">Sender of this event.</param>
        /// <param name="args">Event arguments.</param>
        void OnAuthenticateRequest( object sender, EventArgs args )
        {
            HttpContext context = HttpContext.Current;
            ResourceAccessErrorResponse error;
            string accessToken;

            // checks if it is an OAuth Request and gets the access token
            if (TryReadAccessToken(context.Request, out accessToken))
            {
                // Parses the token and validates it.
                if (!ReadAndValidateToken(accessToken, out error))
                {                    
                    context.Items.Add(errorMessageIdentifier, error);
                }
            }

            // do nothing if no access token is found in the request
        }

        /// <summary>
        /// Event handler for Application.PostAuthenticateRequest. Runs any <see cref="ClaimsAuthenticationManager"/> if one is configured.
        /// Tries to create a <see cref="ClaimsPrincipal"/> if none is already set.
        /// </summary>        
        /// <param name="sender">Sender of this event.</param>
        /// <param name="args">Event arguments.</param>
        void OnPostAuthenticateRequest( object sender, EventArgs args )
        {
            IClaimsPrincipal icp = HttpContext.Current.User as IClaimsPrincipal;

            if (icp == null)
            {
                icp = ClaimsPrincipal.CreateFromHttpContext( HttpContext.Current );

                //
                // Run the Claims Authentication Manager if one is configured
                //
                ClaimsAuthenticationManager authenticationManager = ServiceConfiguration.ClaimsAuthenticationManager;
                if ( authenticationManager != null && icp != null && icp.Identity != null )
                {
                    icp = authenticationManager.Authenticate( HttpContext.Current.Request.Url.AbsoluteUri, icp );
                }

                SetPrincipal( icp );
            }
        }

        /// <summary>
        /// Handle the HTTP pipeline EndRequest event. This sends an error message back if nay errors have occured in the processing pipeline.
        /// </summary>        
        /// <param name="sender">Sender of this event.</param>
        /// <param name="args">Event arguments.</param>
        void OnEndRequest( object sender, EventArgs args )
        {
            if ( ( HttpContext.Current.Items[errorMessageIdentifier] != null ) &&
                ( HttpContext.Current.Items[errorMessageIdentifier] is ResourceAccessErrorResponse ) )
            {
                ResourceAccessErrorResponse message = HttpContext.Current.Items[errorMessageIdentifier] as ResourceAccessErrorResponse;
                SendErrorResponse( message, HttpContext.Current );
            }
        }

        /// <summary>
        /// This method parses the incoming token and validates it.
        /// </summary>
        /// <param name="accessToken">The incoming access token.</param>
        /// <param name="error">This out paramter is set if any error occurs.</param>
        /// <returns>True on success, False on error.</returns>
        protected bool ReadAndValidateToken( string accessToken, out ResourceAccessErrorResponse error )
        {
            bool tokenValid = false;
            error = null;

            SecurityToken token = null;
            ClaimsIdentityCollection claimsIdentityCollection = null;

            try
            {
                SimpleWebTokenHandler handler = new SimpleWebTokenHandler();

                // read the token
                token = handler.ReadToken( accessToken );

                // validate the token
                claimsIdentityCollection = handler.ValidateToken( token );

                // create a claims Principal from the token
                IClaimsPrincipal authenticatedClaimsPrincipal =
                    ServiceConfiguration.ClaimsAuthenticationManager.Authenticate(
                        HttpContext.Current.Request.Url.AbsoluteUri, new ClaimsPrincipal( claimsIdentityCollection ) );

                if ( authenticatedClaimsPrincipal != null )
                {
                    tokenValid = true;

                    // Set the ClaimsPrincipal so that it is accessible to the application
                    SetPrincipal( authenticatedClaimsPrincipal );
                }
            }
            catch ( InvalidTokenReceivedException ex )
            {
                error = new ResourceAccessErrorResponse( realm, ex.ErrorCode, ex.ErrorDescription );
            }
            catch ( ExpiredTokenReceivedException ex )
            {
                error = new ResourceAccessErrorResponse( realm, ex.ErrorCode, ex.ErrorDescription );
            }
            catch ( Exception )
            {
                error = new ResourceAccessErrorResponse( realm, OAuthConstants.ErrorCode.InvalidToken, "Token validation failed" );
            }

            return tokenValid;
        }

        /// <summary>
        /// This method looks for the access token in the incoming request.
        /// </summary>
        /// <param name="request">The incoming request message.</param>
        /// <param name="accessToken">This out parameter contains the access token if found.</param>
        /// <returns>True if access token is found , otherwise false.</returns>
        protected bool TryReadAccessToken(HttpRequest request, out string accessToken)
        {
            accessToken = null;
            
            // search for tokens in the Authorization header            
            accessToken = GetTokenFromAuthorizationHeader(request);
            if (!string.IsNullOrEmpty(accessToken))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// This method sets the given <see cref="IClaimsPrincipal"/> so that applications can use it.
        /// </summary>
        /// <param name="principal">The generated <see cref="IClaimsPrincipal"/>.</param>
        protected void SetPrincipal(IClaimsPrincipal principal)
        {
            HttpContext.Current.User = principal;
            Thread.CurrentPrincipal = principal;
        }

        /// <summary>
        /// Gets the access token from the Authorization header of the incoming request.
        /// </summary>
        /// <param name="request">The Http request message.</param>
        /// <returns>The access token.</returns>
        public string GetTokenFromAuthorizationHeader(HttpRequest request)
        {
            if ( request == null )
            {
                throw new ArgumentNullException( "request" );
            }

            string authHeader;
            string token = null;
            authHeader = request.Headers["Authorization"];

            // the authorization header looks like
            // Authorization: OAuth <access token>
            if (!string.IsNullOrEmpty(authHeader))
            {
                if (String.CompareOrdinal(authHeader, 0, OAuthConstants.AuthenticationType, 0, OAuthConstants.AuthenticationType.Length) == 0)
                {
                    token = authHeader.Remove(0, OAuthConstants.AuthenticationType.Length + 1);
                }
            }
            
            return token;
        }

        /// <summary>
        /// Serializes the error message and sends it in response to the incoming request.
        /// </summary>
        /// <param name="errorMessage">The OAuth error message.</param>
        /// <param name="context">The curent application <see cref="HttpContext"/>.</param>
        public void SendErrorResponse(ResourceAccessErrorResponse errorMessage, HttpContext context)
        {
            if (errorMessage == null)
            {
                throw new ArgumentNullException("errorMessage");
            }

            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponse response = context.Response;
            if (errorMessage.ErrorCode == null)
            {
                throw new ArgumentNullException( "errorMessage.ErrorCode" );
            }

            // set the appropriate status code 
            if (errorMessage.ErrorCode == OAuthConstants.ErrorCode.InsufficientScope)
            {
                response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
            else if (errorMessage.ErrorCode == OAuthConstants.ErrorCode.InvalidRequest)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }

            string errorHeader = GetErrorString(errorMessage);
            response.Headers.Add("WWW-Authenticate", errorHeader);

            response.End();
        }

        /// <summary>
        /// Converts the error message into its wire format.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <returns>The serilized form of the error. </returns>
        string GetErrorString(ResourceAccessErrorResponse message)
        {
            string errorString;
            string headerFirstElementFormat = " {0}=\"{1}\"";
            string headerFormat = ", {0}=\"{1}\"";
            errorString = OAuthConstants.AuthenticationType;

            errorString += string.Format(CultureInfo.InvariantCulture, headerFirstElementFormat, OAuthConstants.Realm, message.Realm);
            errorString += string.Format( CultureInfo.InvariantCulture, headerFormat, OAuthConstants.Error, message.ErrorCode );

            if (message.ErrorDescription != null)
            {
                errorString += string.Format( CultureInfo.InvariantCulture, headerFormat, OAuthConstants.ErrorDescription, message.ErrorDescription );                
            }           

            if (message.ErrorUri != null)
            {
                errorString += string.Format( CultureInfo.InvariantCulture, headerFormat, OAuthConstants.ErrorUri, message.ErrorUri.ToString() );                
            }

            if (message.Scope != null)
            {
                errorString += string.Format( CultureInfo.InvariantCulture, headerFormat, OAuthConstants.Scope, message.Scope );
            }

            return errorString;
        }

        /// <summary>
        /// Disposes of the resources used by the module.
        /// </summary>
        public virtual void Dispose()
        {
        }
    }
}
