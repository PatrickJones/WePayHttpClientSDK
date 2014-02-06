using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WePayMVC5HttpClientSDK.WePayAPITypes
{
    //WePay Documentation: https://www.wepay.com/developer/reference/user
    public class User
    {
        public enum UserStates
        {
            pending = 1, registered = 2, deleted = 3
        }

        public async Task<GetUserResponse> GetUserAsync()
        {
            GetUserResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokeGet<GetUserRequest, GetUserResponse>();
            }
            catch (Exception ex)
            {
                resp = new GetUserResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<ModifyUserResponse> ModifyUserAsync(ModifyUserRequest req)
        {
            ModifyUserResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<ModifyUserRequest, ModifyUserResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new ModifyUserResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<RegisteruserResponse> RegisterUserAsync(RegisterUserRequest req)
        {
            //throw new NotImplementedException("Requires custom implementation as it has a few restriction.");

            RegisteruserResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<RegisterUserRequest, RegisteruserResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new RegisteruserResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<ResendUserConfirmationResponse> ResendUserConfirmationAsync(ResendUserConfirmationRequest req)
        {
            ResendUserConfirmationResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<ResendUserConfirmationRequest, ResendUserConfirmationResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new ResendUserConfirmationResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }
    }

    public class GetUserRequest
    {
        //There are no arguments necessary for this call. Only an access token is required.
        //It is included in case you want to add to or derive from it.
    }

    public class GetUserResponse
    {
        public Int64 user_id { get; set; }
        [MaxLength(255)]
        public string user_name { get; set; }
        [MaxLength(127)]
        public string first_name { get; set; }
        [MaxLength(127)]
        public string last_name { get; set; }
        [MaxLength(255)]
        public string email { get; set; }
        [MaxLength(255)]
        public string state { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    //This call allows you to add a callback_uri to the user object. If you add a callback_uri you will receive IPNs with the user_id each time the user revokes their access_token or is deleted. 
    public class ModifyUserRequest
    {
        [MaxLength(2083)]
        public string callback_uri { get; set; }
    }

    public class ModifyUserResponse
    {
        public Int64 user_id { get; set; }
        [MaxLength(255)]
        public string user_name { get; set; }
        [MaxLength(127)]
        public string first_name { get; set; }
        [MaxLength(127)]
        public string last_name { get; set; }
        [MaxLength(255)]
        public string email { get; set; }
        [MaxLength(255)]
        public string state { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    //IMPORTANT: This call does not use OAuth2 authorization so you should not pass an access token, and you must specify the client_id and client_secret for your application. 
    //IMPORTANT: This call creates a TEMPORARY ACCESS TOKEN, read https://www.wepay.com/developer/reference/user#register
    //IMPORTANT: Accounts created with a temporary access token have a few restrictions.
    public class RegisterUserRequest
    {
        [Required]
        public Int64 client_id { get; set; }
        [Required]
        [MaxLength(255)]
        public string client_secret { get; set; }
        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string scope { get; set; }
        [Required]
        [MaxLength(127)]
        public string first_name { get; set; }
        [Required]
        [MaxLength(127)]
        public string last_name { get; set; }
        [Required]
        [MaxLength(16)]
        public string original_ip { get; set; }
        [Required]
        [MaxLength(255)]
        public string original_device { get; set; }
        [MaxLength(2083)]
        public string redirect_uri { get; set; }
        [MaxLength(2083)]
        public string callback_uri { get; set; }
    }

    public class RegisteruserResponse
    {
        public Int64 user_id { get; set; }
        [MaxLength(255)]
        public string access_token { get; set; }
        [MaxLength(255)]
        public string token_type { get; set; }
        public Int64 expires_in { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    //only for already registered users
    public class ResendUserConfirmationRequest
    {
        [MaxLength(65535)]
        public string email_message { get; set; }
    }

    public class ResendUserConfirmationResponse
    {
        public Int64 user_id { get; set; }
        [MaxLength(127)]
        public string first_name { get; set; }
        [MaxLength(127)]
        public string last_name { get; set; }
        [MaxLength(255)]
        public string email { get; set; }
        [MaxLength(255)]
        public string state { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }
}
