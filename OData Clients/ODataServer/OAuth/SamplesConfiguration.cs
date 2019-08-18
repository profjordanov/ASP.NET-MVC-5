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

using System.Configuration;
namespace Common
{
    /// <summary>
    /// Defines the configuration that the sample needs. Refer to Readme for how to updated the various parameters
    /// in this file once registration with ACS is done.
    /// </summary>
    public static class SamplesConfiguration
    {
        public static string ServiceNamespace
        {
            get
            {
                return ConfigurationManager.AppSettings["ServiceNamespace"];
            }
        }

        public static string RelyingPartyRealm
        {
            get
            {
                return ConfigurationManager.AppSettings["RelyingPartyRealm"];
            }
        }

        public static string RelyingPartySigningKey
        {
            get
            {
                return ConfigurationManager.AppSettings["RelyingPartySigningKey"];
            }
        }

        public static string AcsHostUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["AcsHostUrl"];
            }
        } 


        ////
        //// ACS service Namespace
        ////
        //public const string ServiceNamespace = "...Copy Service Namespace...";

        ////
        //// Management Service Configuration information.
        ////
        //public const string ManagementServiceIdentityName = "ManagementClient";
        //public const string ManagementServiceIdentityKey = "...Copy Service Identity Key...";

        ////
        //// Relying Party Configuration.
        ////
        //public const string RelyingPartyApplicationName = "Customer Information Service";
        //public const string RelyingPartyRealm = "http://contoso/CustomerInformationService/";
        //public const string RelyingPartySigningKey = "...Copy Relying Party Key...";
        
        ////
        //// Client Configuration Information.
        ////
        //public const string ClientIdentity = "FabrikamClient";
        //public const string ClientSecret = "FabrikamSecret";
        
        ////
        //// Identity Provider Configuration.
        ////
        //public const string IdentityProvider = "https://sampleidentityprovider";

        ////
        //// ACS endpoint information.
        ////        
        //public const string AcsHostUrl = "accesscontrol.windows.net";
        //public const string AcsManagementServicesRelativeUrl = "v2/mgmt/service/";
        
        ////
        //// Authorization Server Endpoints.
        ////
        //public const string EndUserLoginUrl = "https://localhost/AuthorizationServer/login.aspx";
        //public const string EndUserConsentUrl = "https://localhost/AuthorizationServer/consent.aspx";

        ////
        //// Protected Resource Endpoint.
        ////
        //public const string ProtectedResourceUrl = "https://localhost/CustomerInformationService/service.svc/CustomerInfo";

        ////
        //// The Uri the client is redirected to after user authentication.
        ////
        //public const string RedirectUrlAfterEndUserConsent = "https://localhost/WebClient/OAuthHandler.ashx";
    }
}
