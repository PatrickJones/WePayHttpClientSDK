using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WePayMVC5HttpClientSDK
{
    public class OAuth2
    {
        public string GetAuthorizationUri()
        {
            //WePay Documentation: This is the endpoint that you send the user to so they can grant your application permission to make calls on their behalf. It is not an API call but an actual uri that you send the user to. 
            // https://www.wepay.com/developer/reference/oauth2
            return WePayConfiguration.endpoint(WePayConfiguration.productionMode) + WePayConfiguration.ActionUrls["AuthorizationRequest"];
        }

        public async Task<TokenResponse> GetTokenAsync(TokenRequest req)
        {
            TokenResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<TokenRequest, TokenResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new TokenResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }
    }

    //The following parameters should be uri encoded to the endpoint uri: 
    public class AuthorizationRequest
    {
        [Required]
        public Int64 client_id { get; set; }
        [Required]
        public string redirect_uri { get; set; }
        [Required]
        public string scope { get; set; }
        public string state { get; set; }
        public string user_name { get; set; }
        public string user_email { get; set; }
    }

    //The following parameters will be url-encoded in the redirect uri and will be needed to make the /v2/oauth2/token call: 
    public class AuthorizationResponse
    {
        //The authorization code used to get the access token. This code expires in 10 minutes
        public string code { get; set; }
        public string state { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class TokenRequest
    {
        [Required]
        public Int64 client_id { get; set; }
        [Required]
        public string redirect_uri { get; set; }
        [Required]
        public string client_secret { get; set; }
        //The code returned by /oauth2/authorize. This code expires in 10 minutes.
        [Required]
        public string code { get; set; }
        public string callback_uri { get; set; }
    }

    public class TokenResponse
    {
        public Int64 user_id { get; set; }
        public string access_token { get; set; }
        public string token_type { get; set; }
        public Int64 expires_in { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }
}
