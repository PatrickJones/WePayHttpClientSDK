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
    public class Subscription
    {
        public enum SubscriptionStates
        {
            _new = 1,
            trial = 2,
            active = 3,
            transition = 4,
            ended = 5,
            cancelled = 6,
            failed = 7,
            expired = 8
        }

        public async Task<GetSubscriptionResponse> GetSubscriptionAsync(GetSubscriptionRequest req)
        {
            GetSubscriptionResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<GetSubscriptionRequest, GetSubscriptionResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new GetSubscriptionResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<FindSubscriptionResponse> FindSubscriptionAsync(FindSubscriptionRequest req)
        {
            FindSubscriptionResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<FindSubscriptionRequest, FindSubscriptionResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new FindSubscriptionResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<CreateSubscriptionResponse> CreateSubscriptionAsync(CreateSubscriptionRequest req)
        {
            CreateSubscriptionResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<CreateSubscriptionRequest, CreateSubscriptionResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new CreateSubscriptionResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<CancelSubscriptionResponse> CancelSubscriptionAsync(CancelSubscriptionRequest req)
        {
            CancelSubscriptionResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<CancelSubscriptionRequest, CancelSubscriptionResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new CancelSubscriptionResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<ModifySubscriptionResponse> ModifySubscriptionAsync(ModifySubscriptionRequest req)
        {
            ModifySubscriptionResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<ModifySubscriptionRequest, ModifySubscriptionResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new ModifySubscriptionResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }
    }

    public class GetSubscriptionRequest
    {
        [Required]
        public Int64 subscription_id { get; set; }
    }

    public class GetSubscriptionResponse
    {
        public Int64 subscription_plan_id { get; set; }
        public Int64 subscription_id { get; set; }
        [MaxLength(2083)]
        public string subscription_uri { get; set; }
        [MaxLength(255)]
        public string payer_name { get; set; }
        [MaxLength(255)]
        public string payer_email { get; set; }
        [MaxLength(255)]
        public string currency { get; set; }
        public decimal amount { get; set; }
        //The period for each subscription. Either "weekly", "monthly", "yearly", or "quarterly".
        [MaxLength(255)]
        public string period { get; set; }
        public decimal app_fee { get; set; }
        [MaxLength(255)]
        public string fee_payer { get; set; }
        [MaxLength(255)]
        public string state { get; set; }
        public Int64 create_time { get; set; }
        [MaxLength(2083)]
        public string redirect_uri { get; set; }
        [MaxLength(2083)]
        public string callback_uri { get; set; }
        public Int64 payment_method_id { get; set; }
        [MaxLength(255)]
        public string payment_method_type { get; set; }
        public Int64 quantity { get; set; }
        [MaxLength(255)]
        public string mode { get; set; }
        public Int64 trial_days_remaining { get; set; }
        public Int64 transition_expire_time { get; set; }
        public bool transition_prorate { get; set; }
        public Int64 transition_quantity { get; set; }
        public Int64 transition_subscription_plan_id { get; set; }
        [MaxLength(255)]
        public string reference_id { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class FindSubscriptionRequest
    {
        [Required]
        public Int64 subscription_plan_id { get; set; }
        public Int64 start { get; set; }
        public Int64 limit { get; set; }
        public Int64 start_time { get; set; }
        public Int64 end_time { get; set; }
        [MaxLength(255)]
        public string state { get; set; }
        [MaxLength(255)]
        public string reference_id { get; set; }
    }

    public class FindSubscriptionResponse
    {
        //NOT a WePay parameter per se'..using existing type so as not create another subscription object (think efficiency, not laziness)
        //WePay Documentation: An array of subscriptions matching the search parameters. Each element of the array will include the same data as returned from the /subscription call. 
        public List<GetSubscriptionResponse> Subscriptions { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class CreateSubscriptionRequest
    {
        [Required]
        public Int64 subscription_plan_id { get; set; }
        [MaxLength(2083)]
        public string redirect_uri { get; set; }
        [MaxLength(2083)]
        public string callback_uri { get; set; }
        public Int64 payment_method_id { get; set; }
        [MaxLength(255)]
        public string payment_method_type { get; set; }
        [MaxLength(255)]
        public string mode { get; set; }
        public Int64 quantity { get; set; }
        [MaxLength(255)]
        public string reference_id { get; set; }
        public PrefillInfoStructure prefill_info { get; set; }
    }

    public class CreateSubscriptionResponse
    {
        public Int64 subscription_id { get; set; }
        [MaxLength(2083)]
        public string subscription_uri { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class CancelSubscriptionRequest
    {
        [Required]
        public Int64 subscription_id { get; set; }
        [MaxLength(255)]
        public string reason { get; set; }
    }

    public class CancelSubscriptionResponse
    {
        public Int64 subscription_plan_id { get; set; }
        public Int64 subscription_id { get; set; }
        [MaxLength(255)]
        public string state { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class ModifySubscriptionRequest
    {
        [Required]
        public Int64 subscription_id { get; set; }
        public Int64 subscription_plan_id { get; set; }
        public Int64 quantity { get; set; }
        public bool prorate { get; set; }
        public Int64 transition_expire_days { get; set; }
        [MaxLength(2083)]
        public string redirect_uri { get; set; }
        [MaxLength(2083)]
        public string callback_uri { get; set; }
        public Int64 payment_method_id { get; set; }
        [MaxLength(255)]
        public string payment_method_type { get; set; }
        [MaxLength(255)]
        public string reference_id { get; set; }
        public Int64 extend_trial_days { get; set; }
    }

    public class ModifySubscriptionResponse
    {
        public Int64 subscription_plan_id { get; set; }
        public Int64 subscription_id { get; set; }
        [MaxLength(2083)]
        public string subscription_uri { get; set; }
        [MaxLength(255)]
        public string payer_name { get; set; }
        [MaxLength(255)]
        public string payer_email { get; set; }
        [MaxLength(255)]
        public string currency { get; set; }
        public decimal amount { get; set; }
        [MaxLength(255)]
        public string period { get; set; }
        public decimal app_fee { get; set; }
        [MaxLength(255)]
        public string fee_payer { get; set; }
        [MaxLength(255)]
        public string state { get; set; }
        public Int64 create_time { get; set; }
        [MaxLength(2083)]
        public string redirect_uri { get; set; }
        [MaxLength(2083)]
        public string callback_uri { get; set; }
        public Int64 payment_method_id { get; set; }
        [MaxLength(255)]
        public string payment_method_type { get; set; }
        public Int64 quantity { get; set; }
        [MaxLength(255)]
        public string mode { get; set; }
        public Int64 trial_days_remaining { get; set; }
        public Int64 transition_expire_time { get; set; }
        public bool transition_prorate { get; set; }
        public Int64 transition_quantity { get; set; }
        public Int64 transition_subscription_plan_id { get; set; }
        [MaxLength(255)]
        public string reference_id { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }
}
