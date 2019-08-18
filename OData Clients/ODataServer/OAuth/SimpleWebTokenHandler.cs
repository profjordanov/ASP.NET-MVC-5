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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IdentityModel.Tokens;
using System.Web;
using Common;
using Microsoft.IdentityModel.Claims;

namespace CustomerInformationService
{
    /// <summary>
    /// This class can parse and validate a Simple Web Token received on the incoming request.
    /// </summary>
    public class SimpleWebTokenHandler
    {
        // constants
        const string AudienceLabel = "Audience";
        const string ExpiresOnLabel = "ExpiresOn";
        const string IssuerLabel = "Issuer";
        const string Digest256Label = "HMACSHA256";
        const string DefaultNameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
        const string AcsNameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        
        static DateTime swtBaseTime = new DateTime( 1970, 1, 1, 0, 0, 0, 0 );
        static string tenantUri = string.Format( CultureInfo.CurrentCulture, "https://{0}.{1}/", SamplesConfiguration.ServiceNamespace, SamplesConfiguration.AcsHostUrl );
        static string symmetricSignatureKey = SamplesConfiguration.RelyingPartySigningKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleWebTokenHandler"/> class.
        /// </summary>
        public SimpleWebTokenHandler()
        {
        }

        /// <summary>
        /// Reads a serialized token and converts it into a <see cref="SecurityToken"/>.
        /// </summary>
        /// <param name="rawToken">The token in serialized form.</param>
        /// <returns>The parsed form of the token.</returns>
        public SecurityToken ReadToken( string rawToken )
        {
            char parameterSeparator = '&';
            Uri audienceUri = null;
            string issuer = null;
            string signature = null;
            string unsignedString = null;
            string expires = null;

            if ( string.IsNullOrEmpty( rawToken ) )
            {
                throw new ArgumentNullException( "rawToken" );
            }

            //
            // Find the last parameter. The signature must be last per SWT specification.
            //
            int lastSeparator = rawToken.LastIndexOf( parameterSeparator );

            // Check whether the last parameter is an hmac.
            //
            if ( lastSeparator > 0 )
            {
                string lastParamStart = parameterSeparator + Digest256Label + "=";
                string lastParam = rawToken.Substring( lastSeparator );

                // Strip the trailing hmac to obtain the original unsigned string for later hmac verification.
                // e.g. name1=value1&name2=value2&HMACSHA256=XXX123 -> name1=value1&name2=value2
                //
                if ( lastParam.StartsWith( lastParamStart, StringComparison.Ordinal ) )
                {
                    unsignedString = rawToken.Substring( 0, lastSeparator );
                }
            }
            else
            {
                throw new InvalidTokenReceivedException( "The Simple Web Token must have a signature at the end. The incoming token did not have a signature at the end of the token." );
            }

            // Signature is a mandatory parameter, and it must be the last one.
            // If there's no trailing hmac, Return error.
            //
            if ( unsignedString == null )
            {
                throw new InvalidTokenReceivedException( "The Simple Web Token must have a signature at the end. The incoming token did not have a signature at the end of the token." );
            }

            // Create a collection of SWT claims
            //
            NameValueCollection rawClaims = ParseToken( rawToken );

            audienceUri = new Uri( rawClaims[AudienceLabel] );
            if ( audienceUri != null )
            {
                rawClaims.Remove( AudienceLabel );
            }
            else
            {
                throw new InvalidTokenReceivedException( "Then incoming token does not have an AudienceUri." );
            }

            expires = rawClaims[ExpiresOnLabel];
            if ( expires != null )
            {
                rawClaims.Remove( ExpiresOnLabel );
            }
            else
            {
                throw new InvalidTokenReceivedException( "Then incoming token does not have an expiry time." );
            }

            issuer = rawClaims[IssuerLabel];
            if ( issuer != null )
            {
                rawClaims.Remove( IssuerLabel );
            }
            else
            {
                throw new InvalidTokenReceivedException( "Then incoming token does not have an Issuer" );
            }

            signature = rawClaims[Digest256Label];
            if ( signature != null )
            {
                rawClaims.Remove( Digest256Label );
            }
            else
            {
                throw new InvalidTokenReceivedException( "Then incoming token does not have a signature" );
            }

            List<Claim> claims = DecodeClaims( issuer, rawClaims );

            SimpleWebToken swt = new SimpleWebToken( audienceUri, issuer, DecodeExpiry( expires ), claims, signature, unsignedString );
            return swt;
        }

        /// <summary>
        /// This methos validates the Simple Web Token.
        /// </summary>
        /// <param name="token">A simple web token.</param>
        /// <returns>A Claims Collection which contains all the claims from the token.</returns>
        public ClaimsIdentityCollection ValidateToken( SecurityToken token )
        {
            SimpleWebToken realToken = token as SimpleWebToken;
            if ( realToken == null )
            {
                throw new InvalidTokenReceivedException( "The received token is of incorrect token type.Expected SimpleWebToken" );
            }

            if ( StringComparer.OrdinalIgnoreCase.Compare( realToken.AudienceUri.ToString(), SamplesConfiguration.RelyingPartyRealm ) != 0 )
            {
                throw new InvalidTokenReceivedException("The Audience Uri of the incoming token is not expected. Expected AudienceUri is " + SamplesConfiguration.RelyingPartyRealm);
            }

            if ( StringComparer.OrdinalIgnoreCase.Compare( realToken.Issuer, tenantUri ) != 0 )
            {
                throw new InvalidTokenReceivedException( "The Issuer of the token is not trusted. Trusted issuer is " + tenantUri );
            }

            if ( !realToken.SignVerify( Convert.FromBase64String( symmetricSignatureKey ) ) )
            {
                throw new InvalidTokenReceivedException( "Signature verification of the incoming token failed." );
            }

            if ( DateTime.Compare( realToken.ValidTo, DateTime.UtcNow ) <= 0 )
            {
                throw new ExpiredTokenReceivedException( "The incoming token has expired. Get a new access token from the Authorization Server." );
            }

            ClaimsIdentityCollection identities = new ClaimsIdentityCollection();
            ClaimsIdentity identity = new ClaimsIdentity();

            foreach ( var claim in realToken.Claims )
            {
                identity.Claims.Add( claim );
            }

            identities.Add( identity );

            return identities;
        }

        /// <summary>
        /// Parses the token into a collection.
        /// </summary>
        /// <param name="encodedToken">The serialized token.</param>
        /// <returns>A colleciton of all name-value pairs from the token.</returns>
        NameValueCollection ParseToken( string encodedToken )
        {
            NameValueCollection claimCollection = new NameValueCollection();
            foreach ( string nameValue in encodedToken.Split( '&' ) )
            {
                string[] keyValueArray = nameValue.Split( '=' );

                if ( ( keyValueArray.Length != 2 )
                   && !String.IsNullOrEmpty( keyValueArray[0] ) )
                {
                    // the signature may have multiple '=' in the end
                    throw new InvalidTokenReceivedException( "The received token is not correctly formed" );
                }

                if ( String.IsNullOrEmpty( keyValueArray[1] ) )
                {
                    // ignore parameter with empty values
                    continue;
                }

                string key = HttpUtility.UrlDecode( keyValueArray[0].Trim() );               // Names must be decoded for the claim type case
                string value = HttpUtility.UrlDecode( keyValueArray[1].Trim().Trim( '"' ) ); // remove any unwanted "
                claimCollection.Add( key, value );
            }

            return claimCollection;
        }

        /// <summary>Create <see cref="Claim"/> from the incoming token.
        /// </summary>
        /// <param name="issuer">The issuer of the token.</param>
        /// <param name="rawClaims">The name value pairs from the token.</param>        
        /// <returns>A list of Claims created from the token.</returns>
        protected List<Claim> DecodeClaims( string issuer, NameValueCollection rawClaims )
        {   
            if ( rawClaims == null )
            {
                throw new ArgumentNullException( "rawClaims" );
            }

            List<Claim> decodedClaims = new List<Claim>();

            foreach ( string key in rawClaims.Keys )
            {
                if ( string.IsNullOrEmpty( rawClaims[key] ) )
                {
                    throw new InvalidTokenReceivedException( "Claim value cannot be empty" );
                }

                decodedClaims.Add( new Claim( key, rawClaims[key], ClaimValueTypes.String, issuer ) );
                if ( key == AcsNameClaimType )
                {
                    // add a default name claim from the Name identifier claim.
                    decodedClaims.Add( new Claim( DefaultNameClaimType, rawClaims[key], ClaimValueTypes.String, issuer ) );
                }
            }

            return decodedClaims;
        }

        /// <summary>
        /// Convert the expiryTime to the <see cref="DateTime"/> format.
        /// </summary>
        /// <param name="expiry">The expiry time from the token.</param>
        /// <returns>The local expiry time of the token.</returns>
        protected DateTime DecodeExpiry( string expiry )
        {
            long totalSeconds = 0;
            if ( !long.TryParse( expiry, out totalSeconds ) )
            {
                throw new InvalidTokenReceivedException( "The incoming token has an unexpected expiration time format" );
            }

            long maxSeconds = (long)( DateTime.MaxValue - swtBaseTime ).TotalSeconds - 1;
            if ( totalSeconds > maxSeconds )
            {
                totalSeconds = maxSeconds;
            }

            return swtBaseTime + TimeSpan.FromSeconds( totalSeconds );
        }
    }
}