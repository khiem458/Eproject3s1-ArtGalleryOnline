using Microsoft.Extensions.Configuration;
using PayPal.Api;
using System;

namespace ArtGalleryOnline.Models
{
    public static class PaypalConfiguration
    {
        // Variables for storing the clientID and clientSecret key  
        public static string ClientId { get; private set; }
        public static string ClientSecret { get; private set; }

        // Method to configure the PayPal settings using IConfiguration
        public static void Configure(IConfiguration config)
        {
            ClientId = config["PayPalSetting:ClientId"];
            ClientSecret = config["PayPalSetting:SecretKey"];
        }

        // Method to get the APIContext with the configured settings
        public static APIContext GetAPIContext()
        {
            if (string.IsNullOrEmpty(ClientId) || string.IsNullOrEmpty(ClientSecret))
            {
                throw new ArgumentNullException("PayPal settings are not configured properly.");
            }

            // Getting access token from PayPal  
            string accessToken = new OAuthTokenCredential(ClientId, ClientSecret, PayPal.Api.ConfigManager.Instance.GetProperties()).GetAccessToken();
            // Return APIContext object by invoking it with the access token  
            APIContext apiContext = new APIContext(accessToken);
            apiContext.Config = PayPal.Api.ConfigManager.Instance.GetProperties();
            return apiContext;
        }
    }
}
