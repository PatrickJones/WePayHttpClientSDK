using Newtonsoft.Json;
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
    public class Checkout
    {
        //_new and charge_back do not match what is returned, adjust in your code.
        public enum AccountStates
        {
            _new = 1, 
            authorized = 2, 
            reserved = 3, 
            captured = 4, 
            settled = 5, 
            cancelled = 6, 
            refunded = 7, 
            charge_back = 8, 
            failed = 9, 
            expired = 10 
        }

        public async Task<GetCheckoutResponse> GetCheckoutAsync(GetCheckoutRequest req)
        {
            GetCheckoutResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<GetCheckoutRequest, GetCheckoutResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new GetCheckoutResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<FindCheckoutResponse> FindCheckoutAsync(FindCheckoutRequest req)
        {
            FindCheckoutResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<FindCheckoutRequest, FindCheckoutResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new FindCheckoutResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<CreateCheckoutResponse> CreateCheckoutAsync(CreateCheckoutRequest req)
        {
            CreateCheckoutResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<CreateCheckoutRequest, CreateCheckoutResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new CreateCheckoutResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<CancelCheckoutResponse> CancelCheckoutAsync(CancelCheckoutRequest req)
        {
            CancelCheckoutResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<CancelCheckoutRequest, CancelCheckoutResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new CancelCheckoutResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<RefundCheckoutResponse> RefundCheckoutAsync(RefundCheckoutRequest req)
        {
            RefundCheckoutResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<RefundCheckoutRequest, RefundCheckoutResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new RefundCheckoutResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<CaptureCheckoutResponse> CaptureCheckoutAsync(CaptureCheckoutRequest req)
        {
            CaptureCheckoutResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<CaptureCheckoutRequest, CaptureCheckoutResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new CaptureCheckoutResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<ModifyCheckoutResponse> ModifyCheckoutAsync(ModifyCheckoutRequest req)
        {
            ModifyCheckoutResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<ModifyCheckoutRequest, ModifyCheckoutResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new ModifyCheckoutResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }
    }

    public class GetCheckoutRequest
    {
        //The unique ID of the checkout you want to look up.
        [Required]    
        public Int64 checkout_id { get; set; }
    }

    //parameters marked "if available" will only show up if they have values.
    public class GetCheckoutResponse
    {
        public Int64 checkout_id { get; set; }
        public Int64 account_id { get; set; }
        public Int64 preapproval_id { get; set; }
        public Int64 create_time { get; set; }
        [MaxLength(255)]
        public string state { get; set; }
        [MaxLength(255)]
        public string soft_descriptor { get; set; }
        [MaxLength(255)]
        public string short_description { get; set; }
        [MaxLength(2047)]
        public string long_description { get; set; }
        [MaxLength(255)]
        public string currency { get; set; }
        public decimal amount { get; set; }
        public decimal fee { get; set; }
        public decimal gross { get; set; }
        public decimal app_fee { get; set; }
        public decimal tax { get; set; }
        public decimal amount_refunded { get; set; }
        public decimal amount_charged_back { get; set; }
        [MaxLength(255)]
        public string fee_payer { get; set; }
        [MaxLength(255)]
        public string reference_id { get; set; }
        [MaxLength(2083)]
        public string redirect_uri { get; set; }
        [MaxLength(2083)]
        public string callback_uri { get; set; }
        [MaxLength(2083)]
        public string dispute_uri { get; set; }
        [MaxLength(255)]
        public string payer_email { get; set; }
        [MaxLength(255)]
        public string payer_name { get; set; }
        [MaxLength(255)]
        public string cancel_reason { get; set; }
        [MaxLength(255)]
        public string refund_reason { get; set; }
        public bool auto_capture { get; set; }
        public bool require_shipping { get; set; }
        public AddressStructure shipping_address { get; set; }
        [MaxLength(255)]
        public string mode { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class FindCheckoutRequest
    {
        //The unique ID of the account whose checkouts you are searching.
        [Required]
        public Int64 account_id { get; set; }
        //The start position for your search (default 0).
        public Int64 start { get; set; }
        //The maximum number of returned entries (default 50).
        public Int64 limit { get; set; }
        //The unique reference id of the checkout (set by the application in /checkout/create).
        [MaxLength(255)]
        public string reference_id { get; set; }
        //What state the checkout is in (see the checkout states section reference for details).
        [MaxLength(255)]
        public string state { get; set; }
        //The ID of the preapproval that was used to create the checkout. Useful if you want to look up all of the payments for an auto_recurring preapproval.
        public Int64 preapproval_id { get; set; }
        //All checkouts after given start time. Can be a unix_timestamp or a valid, parse-able date-time.
        public Int64 start_time { get; set; }
        //All checkouts before given end time. Can be a unix_timestamp or a valid, parse-able date-time.
        public Int64 end_time { get; set; }
        //Sort the results of the search by time created. Use 'DESC' for most recent to least recent. Use 'ASC' for least recent to most recent. Defaults to 'DESC'.
        [MaxLength(255)]
        public string sort_order { get; set; }
        //All checkouts that have the given shipping fee.
        public decimal shipping_fee { get; set; }
    }

    public class FindCheckoutResponse
    {
        //NOT a WePay parameter per se'..using existing type so as not create another checkout object (think efficiency, not laziness)
        public List<GetCheckoutResponse> checkouts { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    //IMPORTANT: Checkouts expire 30 minutes after they are created if there is no activity on them (e.g. they were abandoned after creation).
    public class CreateCheckoutRequest
    {
        [Required]
        public Int64 account_id { get; set; }
        [Required]
        [MaxLength(255)]
        public string short_description { get; set; }
        [MaxLength(255)]
        [Required]
        public string type { get; set; }
        [MaxLength(255)]
        public string currency { get; set; }
        [Required]
        public decimal amount { get; set; }
        [MaxLength(2047)]
        public string long_description { get; set; }
        [MaxLength(65535)]
        public string payer_email_message { get; set; }
        [MaxLength(255)]
        public string reference_id { get; set; }
        public decimal app_fee { get; set; }
        //IMPORTANT: https://www.wepay.com/developer/reference/checkout#create
        [MaxLength(255)]
        public string fee_payer { get; set; }
        [MaxLength(2083)]
        public string redirect_uri { get; set; }
        [MaxLength(2083)]
        public string callback_uri { get; set; }
        [MaxLength(2083)]
        public string fallback_uri { get; set; }
        public bool auto_capture { get; set; }
        public bool require_shipping { get; set; }
        public decimal charge_tax { get; set; }
        [MaxLength(255)]
        public string mode { get; set; }
        public Int64 preapproval_id { get; set; }
        public PrefillInfoStructure prefill_info { get; set; }
        [MaxLength(255)]
        public string funding_sources { get; set; }
        //If you are using credit card tokenization use the credit_card_id you received from the /credit_card/create call.
        //https://www.wepay.com/developer/tutorial/tokenization
        public Int64 payment_method_id { get; set; }
        //Set to 'credit_card' for tokenization. https://www.wepay.com/developer/tutorial/tokenization
        [MaxLength(255)]
        public string payment_mehtod_type { get; set; }
    }

    public class CreateCheckoutResponse
    {
        public Int64 checkout_id { get; set; }
        [MaxLength(2083)]
        public string checkout_uri { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class CancelCheckoutRequest
    {
        [Required]
        public Int64 checkout_id { get; set; }
        [MaxLength(255)]
        [Required]
        public string cancel_reason { get; set; }
    }

    public class CancelCheckoutResponse
    {
        public Int64 checkout_id { get; set; }
        [MaxLength(255)]
        public string state { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class RefundCheckoutRequest
    {
        [Required]
        public Int64 checkout_id { get; set; }
        [MaxLength(255)]
        [Required]
        public string refund_reason { get; set; }
        public decimal amount { get; set; }
        public decimal app_fee { get; set; }
        [MaxLength(65535)]
        public string payer_email_message { get; set; }
    }

    public class RefundCheckoutResponse
    {
        public Int64 checkout_id { get; set; }
        [MaxLength(255)]
        public string state { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class CaptureCheckoutRequest
    {
        //The unique ID of the checkout to be captured.
        [Required]
        public Int64 checkout_id { get; set; }
    }

    public class CaptureCheckoutResponse
    {
        public Int64 checkout_id { get; set; }
        [MaxLength(255)]
        public string state { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class ModifyCheckoutRequest
    {
        //The unique ID of the checkout you want to modify.
        [Required]
        public Int64 checkout_id { get; set; }
        //The uri that will receive any instant payment notifications sent. Needs to be a full uri (ex https://www.wepay.com ) and must NOT be localhost or 127.0.0.1 or include wepay.com.
        [MaxLength(2083)]
        public string callback_uri { get; set; }
    }

    public class ModifyCheckoutResponse
    {
        public Int64 checkout_id { get; set; }
        public Int64 account_id { get; set; }
        public Int64 preapproval_id { get; set; }
        public Int64 create_time { get; set; }
        [MaxLength(255)]
        public string state { get; set; }
        [MaxLength(255)]
        public string soft_descriptor { get; set; }
        [MaxLength(255)]
        public string short_description { get; set; }
        [MaxLength(2047)]
        public string long_description { get; set; }
        [MaxLength(255)]
        public string currency { get; set; }
        public decimal amount { get; set; }
        public decimal fee { get; set; }
        public decimal gross { get; set; }
        public decimal app_fee { get; set; }
        public decimal tax { get; set; }
        public decimal amount_refunded { get; set; }
        public decimal amount_charged_back { get; set; }
        [MaxLength(255)]
        public string fee_payer { get; set; }
        [MaxLength(255)]
        public string reference_id { get; set; }
        [MaxLength(2083)]
        public string redirect_uri { get; set; }
        [MaxLength(2083)]
        public string callback_uri { get; set; }
        [MaxLength(2083)]
        public string dispute_uri { get; set; }
        [MaxLength(255)]
        public string payer_email { get; set; }
        [MaxLength(255)]
        public string payer_name { get; set; }
        [MaxLength(255)]
        public string cancel_reason { get; set; }
        [MaxLength(255)]
        public string refund_reason { get; set; }
        public bool auto_capture { get; set; }
        public bool require_shipping { get; set; }
        public AddressStructure shipping_address { get; set; }
        [MaxLength(255)]
        public string mode { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }
}
