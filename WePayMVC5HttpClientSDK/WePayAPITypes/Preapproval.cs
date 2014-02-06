using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WePayMVC5HttpClientSDK.WePayAPIStructures;

namespace WePayMVC5HttpClientSDK.WePayAPITypes
{
    public class Preapproval
    {
        public enum PreapprovalStates
        {
            _new = 1,
            approved = 2,
            expired = 3,
            revoked = 4,
            cancelled = 5,
            stopped = 6,
            completed = 7,
            retrying = 8
        }

        public async Task<GetPreapprovalResponse> GetPreapprovalAsync(GetPreapprovalRequest req)
        {
            GetPreapprovalResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<GetPreapprovalRequest, GetPreapprovalResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new GetPreapprovalResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<FindPreapprovalResponse> FindPreapprovalAsync(FindPreapprovalRequest req)
        {
            FindPreapprovalResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<FindPreapprovalRequest, FindPreapprovalResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new FindPreapprovalResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<CreatePreapprovalResponse> CreatePreapprovalAsync(CreatePreapprovalRequest req)
        {
            CreatePreapprovalResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<CreatePreapprovalRequest, CreatePreapprovalResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new CreatePreapprovalResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<CancelPreapprovalResponse> CancelPreapprovalAsync(CancelPreapprovalRequest req)
        {
            CancelPreapprovalResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<CancelPreapprovalRequest, CancelPreapprovalResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new CancelPreapprovalResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<ModifyPreapprovalResponse> ModifyPreapprovalAsync(ModifyPreapprovalRequest req)
        {
            ModifyPreapprovalResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<ModifyPreapprovalRequest, ModifyPreapprovalResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new ModifyPreapprovalResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }
    }

    public class GetPreapprovalRequest
    {
        [Required]
        public Int64 preapproval_id { get; set; }
    }

    public class GetPreapprovalResponse
    {
        public Int64 preapproval_id { get; set; }
        [MaxLength(2083)]
        public string preapproval_uri { get; set; }
        [MaxLength(2083)]
        public string manage_uri { get; set; }
        public Int64 account_id { get; set; }
        [MaxLength(255)]
        public string short_description { get; set; }
        [MaxLength(2047)]
        public string long_description { get; set; }
        [MaxLength(255)]
        public string currency { get; set; }
        public decimal amount { get; set; }
        [MaxLength(255)]
        public string fee_payer { get; set; }
        [MaxLength(255)]
        public string state { get; set; }
        [MaxLength(2083)]
        public string redirect_uri { get; set; }
        public decimal app_fee { get; set; }
        [MaxLength(255)]
        public string period { get; set; }
        public Int64 frequency { get; set; }
        public Int64 start_time { get; set; }
        public Int64 end_time { get; set; }
        [MaxLength(255)]
        public string reference_id { get; set; }
        [MaxLength(2083)]
        public string callback_uri { get; set; }
        public AddressStructure shipping_address { get; set; }
        public decimal shipping_fee { get; set; }
        public decimal tax { get; set; }
        public bool auto_recur { get; set; }
        [MaxLength(255)]
        public string payer_name { get; set; }
        [MaxLength(255)]
        public string payer_email { get; set; }
        public Int64 create_time { get; set; }
        public Int64 next_due_time { get; set; }
        public Int64 last_checkout_id { get; set; }
        public Int64 last_checkout_time { get; set; }
        [MaxLength(255)]
        public string mode { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class FindPreapprovalRequest
    {
        public Int64 account_id { get; set; }
        [MaxLength(255)]
        public string state { get; set; }
        [MaxLength(255)]
        public string reference_id { get; set; }
        public Int64 start { get; set; }
        public Int64 limit { get; set; }
        [MaxLength(255)]
        public string sort_order { get; set; }
        public Int64 last_checkout_id { get; set; }
        public decimal shipping_fee { get; set; }
    }

    public class FindPreapprovalResponse
    {
        //NOT a WePay parameter per se'..using existing type so as not create another preapproval object (think efficiency, not laziness)
        //WePay Documentation: An array of preapprovals matching the search parameters. Each element of the array will include the same data as returned from the /preapproval call. 
        public List<GetPreapprovalResponse> Preapprovals { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    //IMPORTANT: If reference_id is passed, it MUST be unique for the application/user pair or an error will be returned. 
    public class CreatePreapprovalRequest
    {
        public Int64 account_id { get; set; }
        public decimal amount { get; set; }
        [MaxLength(3)]
        public string currency { get; set; }
        [Required]
        [MaxLength(255)]
        public string short_description { get; set; }
        //Can be: daily, weekly, biweekly, monthly, quarterly, yearly, or once. The API application can charge the payer every period.
        [Required]
        [MaxLength(255)]
        public string period { get; set; }
        [MaxLength(255)]
        public string reference_id { get; set; }
        public decimal app_fee { get; set; }
        //IMPORTANT: https://www.wepay.com/developer/reference/preapproval#create
        [MaxLength(255)]
        public string fee_payer { get; set; }
        [MaxLength(2083)]
        public string redirect_uri { get; set; }
        [MaxLength(2083)]
        public string callback_uri { get; set; }
        [MaxLength(2083)]
        public string fallback_uri { get; set; }
        //Defaults to false. If set to true then the payer will be require to enter their shipping address when they approve the preapproval.
        public bool require_shipping { get; set; }
        public decimal shipping_fee { get; set; }
        //Defaults to false. If set to true then any applicable taxes will be charged.
        public bool charge_tax { get; set; }
        [MaxLength(65535)]
        public string payer_email_message { get; set; }
        [MaxLength(2047)]
        public string long_description { get; set; }
        public Int64 frequency { get; set; }
        public Int64 start_time { get; set; }
        public Int64 end_time { get; set; }
        public bool auto_recur { get; set; }
        [MaxLength(255)]
        public string mode { get; set; }
        public PrefillInfoStructure prefill_info { get; set; }
        [MaxLength(255)]
        public string funding_sources { get; set; }
        //If you are using credit card tokenization use the credit_card_id you received from the /credit_card/create call.
        public Int64 payment_method_id { get; set; }
        //Set to 'credit_card' for tokenization.
        [MaxLength(255)]
        public string payment_method_type { get; set; }

    }

    public class CreatePreapprovalResponse
    {
        [Required]
        public Int64 preapproval_id { get; set; }
        [MaxLength(2083)]
        public string preapproval_uri { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class CancelPreapprovalRequest
    {
        [Required]
        public Int64 preapproval_id { get; set; }
    }

    public class CancelPreapprovalResponse
    {
        public Int64 preapproval_id { get; set; }
        [MaxLength(255)]
        public string state { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class ModifyPreapprovalRequest
    {
        [Required]
        public Int64 preapproval_id { get; set; }
        [MaxLength(2083)]
        public string callback_uri { get; set; }
    }

    public class ModifyPreapprovalResponse
    {
        public Int64 preapproval_id { get; set; }
        [MaxLength(2083)]
        public string preapproval_uri { get; set; }
        [MaxLength(2083)]
        public string manage_uri { get; set; }
        public Int64 account_id { get; set; }
        [MaxLength(255)]
        public string short_description { get; set; }
        [MaxLength(2047)]
        public string long_description { get; set; }
        [MaxLength(255)]
        public string currency { get; set; }
        public decimal amount { get; set; }
        [MaxLength(255)]
        public string fee_payer { get; set; }
        [MaxLength(255)]
        public string state { get; set; }
        [MaxLength(2083)]
        public string redirect_uri { get; set; }
        public decimal app_fee { get; set; }
        [MaxLength(255)]
        public string period { get; set; }
        public Int64 frequency { get; set; }
        public Int64 start_time { get; set; }
        public Int64 end_time { get; set; }
        [MaxLength(255)]
        public string reference_id { get; set; }
        [MaxLength(2083)]
        public string callback_uri { get; set; }
        public AddressStructure shipping_address { get; set; }
        public decimal shipping_fee { get; set; }
        public decimal tax { get; set; }
        public bool auto_recur { get; set; }
        [MaxLength(255)]
        public string payer_name { get; set; }
        [MaxLength(255)]
        public string payer_email { get; set; }
        public Int64 create_time { get; set; }
        public Int64 next_due_time { get; set; }
        public Int64 last_checkout_id { get; set; }
        public Int64 last_checkout_time { get; set; }
        [MaxLength(255)]
        public string mode { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }
}
