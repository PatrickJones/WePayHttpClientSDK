using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WePayMVC5HttpClientSDK.WePayAPIStructures;

namespace WePayMVC5HttpClientSDK.WePayAPITypes
{
    public class Account
    {
        public enum AccountStates
        {
            action_required = 1, pending = 2, active = 3, disabled = 4, deleted = 5
        }

        public async Task<GetAccountResponse> GetAccountAsync(GetAccountRequest req)
        {
            GetAccountResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<GetAccountRequest, GetAccountResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new GetAccountResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<FindAccountResponse> FindAccountAsync(FindAccountRequest req)
        {
            FindAccountResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<FindAccountRequest, FindAccountResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new FindAccountResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<CreateAccountResponse> CreateAccountAsync(CreateAccountRequest req)
        {
            CreateAccountResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<CreateAccountRequest, CreateAccountResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new CreateAccountResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<ModifyAccountResponse> ModifyAccountAsync(ModifyAccountRequest req)
        {
            ModifyAccountResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<ModifyAccountRequest, ModifyAccountResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new ModifyAccountResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<DeleteAccountResponse> DeleteAccountAsync(DeleteAccountRequest req)
        {
            DeleteAccountResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<DeleteAccountRequest, DeleteAccountResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new DeleteAccountResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<GetAccountUpdateUriResponse> GetAccountUpdateUriAsync(GetAccountUpdateUriRequest req)
        {
            GetAccountUpdateUriResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<GetAccountUpdateUriRequest, GetAccountUpdateUriResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new GetAccountUpdateUriResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<GetAccountReserveDetailsResponse> GetAccountReserveDetailsAsync(GetAccountReserveDetailsRequest req)
        {
            GetAccountReserveDetailsResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<GetAccountReserveDetailsRequest, GetAccountReserveDetailsResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new GetAccountReserveDetailsResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }
    }

    public class GetAccountRequest
    {
        [Required]
        public Int64 account_id { get; set; }
    }

    public class GetAccountResponse
    {
        public Int64 account_id { get; set; }
        [MaxLength(255)]
        public string name { get; set; }
        [MaxLength(255)]
        public string state { get; set; }
        [MaxLength(65535)]
        public string description { get; set; }
        [MaxLength(255)]
        public string reference_id { get; set; }
        public List<string> gaq_domains { get; set; }
        public JObject theme_object { get; set; }
        [MaxLength(255)]
        public string type { get; set; }
        public Int64 create_time { get; set; }
        public List<AccountBalance> balances { get; set; }
        public List<AccountStatus> statuses { get; set; }
        public List<string> action_reasons { get; set; }
        [MaxLength(2)]
        public string country { get; set; }//The 2-letters ISO-3166-1 country code at https://en.wikipedia.org/wiki/ISO_3166-1#Officially_assigned_code_elements
        public List<string> currencies { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class FindAccountRequest
    {
        [MaxLength(255)]
        public string name { get; set; }
        [MaxLength(255)]
        public string reference_id { get; set; }
        [MaxLength(255)]
        public string sort_order { get; set; }
    }

    public class FindAccountResponse
    {
        //NOT a WePay parameter per se'..using existing type so as not create another account object (think efficiency, not laziness)
        //WePay Documentation: An array of accounts matching the search parameters. Each element of the array will include the same data as returned from the /account call. 
        public List<GetAccountResponse> accounts { get; set; }
                
        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class CreateAccountRequest
    {
        [Required]
        [MaxLength(255)]
        public string name { get; set; }
        [Required]
        [MaxLength(65535)]
        public string description { get; set; }
        [MaxLength(255)]
        public string reference_id { get; set; }
        [MaxLength(255)]
        public string type { get; set; }
        [MaxLength(2083)]
        public string image_uri { get; set; }
        //An array of Google Analytics domains associated with the app. https://www.wepay.com/developer/tutorial/analytics
        public List<string> gaq_domains { get; set; }
        public JObject theme_object { get; set; }
        public Int64 mcc { get; set; }
        [MaxLength(2083)]
        public string callback_uri { get; set; }
        [MaxLength(2)]
        public string country { get; set; }//The 2-letters ISO-3166-1 country code at https://en.wikipedia.org/wiki/ISO_3166-1#Officially_assigned_code_elements
        public List<string> currencies { get; set; }
    }

    public class CreateAccountResponse
    {
        public Int64 account_id { get; set; }
        [MaxLength(255)]
        public string name { get; set; }
        [MaxLength(255)]
        public string state { get; set; }
        [MaxLength(65535)]
        public string description { get; set; }
        [MaxLength(255)]
        public string reference_id { get; set; }
        //An array of Google Analytics domains associated with the app. https://www.wepay.com/developer/tutorial/analytics
        public List<string> gaq_domains { get; set; }
        public JObject theme_object { get; set; }
        [MaxLength(255)]
        public string type { get; set; }
        public Int64 create_time { get; set; }
        public List<AccountBalance> balances { get; set; }
        public List<AccountStatus> statuses { get; set; }
        public List<string> action_reasons { get; set; }
        [MaxLength(2)]
        public string country { get; set; }//The 2-letters ISO-3166-1 country code at https://en.wikipedia.org/wiki/ISO_3166-1#Officially_assigned_code_elements
        public List<string> currencies { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class ModifyAccountRequest
    {
        public Int64 account_id { get; set; }
        [MaxLength(255)]
        public string name { get; set; }
        [MaxLength(65535)]
        public string description { get; set; }
        [MaxLength(255)]
        public string reference_id { get; set; }
        [MaxLength(2083)]
        public string image_uri { get; set; }
        //An array of Google Analytics domains associated with the app. https://www.wepay.com/developer/tutorial/analytics
        public List<string> gaq_domains { get; set; }
        public JObject theme_object { get; set; }
        [MaxLength(2083)]
        public string callback_uri { get; set; }
    }

    public class ModifyAccountResponse
    {
        public Int64 account_id { get; set; }
        [MaxLength(255)]
        public string name { get; set; }
        [MaxLength(255)]
        public string state { get; set; }
        [MaxLength(65535)]
        public string description { get; set; }
        [MaxLength(255)]
        public string reference_id { get; set; }
        //An array of Google Analytics domains associated with the app. https://www.wepay.com/developer/tutorial/analytics
        public List<string> gaq_domains { get; set; }
        public JObject theme_object { get; set; }
        [MaxLength(255)]
        public string type { get; set; }
        public Int64 create_time { get; set; }
        public List<AccountBalance> balances { get; set; }
        public List<AccountStatus> statuses { get; set; }
        public List<string> action_reasons { get; set; }
        [MaxLength(2)]
        public string country { get; set; }//The 2-letters ISO-3166-1 country code at https://en.wikipedia.org/wiki/ISO_3166-1#Officially_assigned_code_elements
        public List<string> currencies { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class DeleteAccountRequest
    {
        [Required]
        public Int64 account_id { get; set; }
        [MaxLength(255)]
        public string reason { get; set; }
    }

    public class DeleteAccountResponse
    {
        public Int64 account_id { get; set; }
        [MaxLength(255)]
        public string state { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class GetAccountUpdateUriRequest
    {
        [Required]
        public Int64 account_id { get; set; }
        [MaxLength(255)]
        public string mode { get; set; }
        [MaxLength(2083)]
        public string redirect_uri { get; set; }
    }

    public class GetAccountUpdateUriResponse
    {
        public Int64 account_id { get; set; }
        [MaxLength(2083)]
        public string uri { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class GetAccountReserveDetailsRequest
    {
        [Required]
        public Int64 account_id { get; set; }
        [MaxLength(255)]
        public string currency { get; set; }
    }

    public class GetAccountReserveDetailsResponse
    {
        public Int64 account_id { get; set; }
        [MaxLength(255)]
        public string currency { get; set; }
        public Int64 reserved_amount { get; set; }
        public List<WithdrawalSchedule> withdrawals_schedule { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    //Not part of WePay API, but need for the response of the /account call
    public class AccountBalance
    {
        public string currency { get; set; }
        public double balance { get; set; }
        public double incoming_pending_amount { get; set; }
        public double outgoing_pending_amount { get; set; }
        public double reserved_amount { get; set; }
        public double disputed_amount { get; set; }
        public string withdrawal_period { get; set; }
        public Int64? withdrawal_next_time { get; set; }
        public string withdrawal_bank_name { get; set; }
    }

    //Not part of WePay API, but need for the response of the /account call
    public class AccountStatus
    {
        public string currency { get; set; }
        public string incoming_payments_status { get; set; }
        public string outgoing_payments_status { get; set; }
    }

    //Not part of WePay API, but need for the response of the /account call
    public class WithdrawalSchedule
    {
        public Int64? time { get; set; }
        public double amount { get; set; }
    }
}
