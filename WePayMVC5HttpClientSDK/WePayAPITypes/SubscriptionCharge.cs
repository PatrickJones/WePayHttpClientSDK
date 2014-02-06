using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WePayMVC5HttpClientSDK.WePayAPITypes
{
    public class SubscriptionCharge
    {
        public enum SubscriptionChargeStates
        {
            _new = 1,
            authorized = 2,
            failed = 3,
            refunded = 4,
            captured = 5,
            settled = 6,
            charge_back = 7
        }

        public async Task<GetSubscriptionChargeResponse> GetSubscriptionChargeAsync(GetSubscriptionChargeRequest req)
        {
            GetSubscriptionChargeResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<GetSubscriptionChargeRequest, GetSubscriptionChargeResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new GetSubscriptionChargeResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<FindSubscriptionChargeResponse> FindSubscriptionChargeAsync(FindSubscriptionChargeRequest req)
        {
            FindSubscriptionChargeResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<FindSubscriptionChargeRequest, FindSubscriptionChargeResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new FindSubscriptionChargeResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<RefundSubscriptionChargeResponse> RefundSubscriptionChargeAsync(RefundSubscriptionChargeRequest req)
        {
            RefundSubscriptionChargeResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<RefundSubscriptionChargeRequest, RefundSubscriptionChargeResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new RefundSubscriptionChargeResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }
    }

    public class GetSubscriptionChargeRequest
    {
        [Required]
        public Int64 subscription_charge_id { get; set; }
    }

    public class GetSubscriptionChargeResponse
    {
        public Int64 subscription_charge_id { get; set; }
        public Int64 subscription_plan_id { get; set; }
        public Int64 subscription_id { get; set; }
        [MaxLength(255)]
        public string type { get; set; }
        public decimal amount { get; set; }
        [MaxLength(3)]
        public string currency { get; set; }
        public decimal fee { get; set; }
        public decimal app_fee { get; set; }
        public decimal gross { get; set; }
        public Int64 quantity { get; set; }
        public decimal amount_refunded { get; set; }
        public decimal amount_charged_back { get; set; }
        [MaxLength(255)]
        public string state { get; set; }
        public Int64 create_time { get; set; }
        public Int64 end_time { get; set; }
        public Int64 prorate_time { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class FindSubscriptionChargeRequest
    {
        [Required]
        public Int64 subscription_id { get; set; }
        public Int64 start { get; set; }
        public Int64 limit { get; set; }
        public Int64 start_time { get; set; }
        public Int64 end_time { get; set; }
        [MaxLength(255)]
        public string type { get; set; }
        public decimal amount { get; set; }
        [MaxLength(255)]
        public string state { get; set; }
    }

    public class FindSubscriptionChargeResponse
    {
        //NOT a WePay parameter per se'..using existing type so as not create another subscription charge object (think efficiency, not laziness)
        //WePay Documentation: An array of subscriptions matching the search parameters. Each element of the array will include the same data as returned from the /subscription call. 
        public List<GetSubscriptionChargeResponse> SubscriptionCharges { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class RefundSubscriptionChargeRequest
    {
        [Required]
        public Int64 subscription_charge_id { get; set; }
        [MaxLength(255)]
        public string refund_reason { get; set; }
    }

    public class RefundSubscriptionChargeResponse
    {
        public Int64 subscription_plan_id { get; set; }
        public Int64 subscription_id { get; set; }
        public Int64 subscription_charge_id { get; set; }
        [MaxLength(255)]
        public string state { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }
}
