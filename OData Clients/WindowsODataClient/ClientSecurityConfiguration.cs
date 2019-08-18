using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace WindowsODataClient
{
    public static class ClientSecurityConfiguration
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

        public static string AcsHostUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["AcsHostUrl"];
            }
        }

        public static string OAuthPwd
        {
            get
            {
                return ConfigurationManager.AppSettings["OAuthPwd"];
            }
        }

        public static string OAuthUserName
        {
            get
            {
                return ConfigurationManager.AppSettings["OAuthUserName"];
            }
        }
        public static string FormsPwd
        {
            get
            {
                return ConfigurationManager.AppSettings["FormsPwd"];
            }
        }

        public static string FormsUserName
        {
            get
            {
                return ConfigurationManager.AppSettings["FormsUserName"];
            }
        } 
    }
}
