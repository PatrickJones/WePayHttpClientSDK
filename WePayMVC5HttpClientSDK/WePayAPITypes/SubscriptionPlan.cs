using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WePayMVC5HttpClientSDK.WePayAPITypes
{
    //IMPORTANT: The Subscriptions API is in beta. Please provide feedback - api@wepay.com
    //https://www.wepay.com/developer/reference/subscription_plan
    public class SubscriptionPlan
    {
        public enum SubscriptionPlanStates
        {
            available = 1,
            deleted = 2
        }

        public async Task<GetSubscriptionPlanResponse> GetSubscriptionPlanAsync(GetSubscriptionPlanRequest req)
        {
            GetSubscriptionPlanResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<GetSubscriptionPlanRequest, GetSubscriptionPlanResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new GetSubscriptionPlanResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<FindSubscriptionPlanResponse> FindSubscriptionPlanAsync(FindSubscriptionPlanRequest req)
        {
            FindSubscriptionPlanResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<FindSubscriptionPlanRequest, FindSubscriptionPlanResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new FindSubscriptionPlanResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<CreateSubscriptionPlanResponse> CreateSubscriptionPlanAsync(CreateSubscriptionPlanRequest req)
        {
            CreateSubscriptionPlanResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<CreateSubscriptionPlanRequest, CreateSubscriptionPlanResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new CreateSubscriptionPlanResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<DeleteSubscriptionPlanResponse> DeleteSubscriptionPlanAsync(DeleteSubscriptionPlanRequest req)
        {
            DeleteSubscriptionPlanResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<DeleteSubscriptionPlanRequest, DeleteSubscriptionPlanResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new DeleteSubscriptionPlanResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<ModifySubscriptionPlanResponse> ModifySubscriptionPlanAsync(ModifySubscriptionPlanRequest req)
        {
            ModifySubscriptionPlanResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<ModifySubscriptionPlanRequest, ModifySubscriptionPlanResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new ModifySubscriptionPlanResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }
    }

    public class GetSubscriptionPlanRequest
    {
        [Required]
        public Int64 subscription_plan_id { get; set; }
    }

    public class GetSubscriptionPlanResponse
    {
        public Int64 subscription_plan_id { get; set; }
        public Int64 account_id { get; set; }
        [MaxLength(255)]
        public string name { get; set; }
        [MaxLength(2047)]
        public string short_description { get; set; }
        [MaxLength(3)]  
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
        public Int64 number_of_subscriptions { get; set; }
        [MaxLength(2083)]
        public string callback_uri { get; set; }
        public Int64 trial_length { get; set; }
        public decimal setup_fee { get; set; }
        [MaxLength(255)]
        public string reference_id { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class FindSubscriptionPlanRequest
    {
        public Int64 account_id { get; set; }
        public Int64 start { get; set; }
        public Int64 limit { get; set; }
        [MaxLength(255)]
        public string state { get; set; }
        [MaxLength(255)]
        public string reference_id { get; set; }

    }

    public class FindSubscriptionPlanResponse
    {
        //NOT a WePay parameter per se'..using existing type so as not create another subscriptionplan object (think efficiency, not laziness)
        //WePay Documentation: An array of subscription plans matching the search parameters. Each element of the array will include the same data as returned from the /subscription_plan call. 
        public List<GetSubscriptionPlanResponse> SubscriptionPlans { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class CreateSubscriptionPlanRequest
    {
        [Required]
        public Int64 account_id { get; set; }
        [Required]
        [MaxLength(255)]
        public string name { get; set; }
        [Required]
        [MaxLength(2047)]
        public string short_description { get; set; }
        [Required]
        public decimal amount { get; set; }
        [MaxLength(3)]
        public string currency { get; set; }
        [Required]
        [MaxLength(255)]
        public string period { get; set; }
        public decimal app_fee { get; set; }
        [MaxLength(2083)]
        public string callback_uri { get; set; }
        public Int64 trial_length { get; set; }
        public decimal setup_fee { get; set; }
        [MaxLength(255)]
        public string reference_id { get; set; }
    }

    public class CreateSubscriptionPlanResponse
    {
        public Int64 subscription_plan_id { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class DeleteSubscriptionPlanRequest
    {
        [Required]
        public Int64 subscription_plan_id { get; set; }
        [MaxLength(255)]
        public string reason { get; set; }
    }

    public class DeleteSubscriptionPlanResponse
    {
        public Int64 subscription_plan_id { get; set; }
        [MaxLength(255)]
        public string state{ get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class GetSubscriptionPlanButtonRequest
    {
        //IMPORTANT: Buttons are being retired. Buttons are being retired. Starting March 13, 2014 you will no longer be able to accept payments using buttons. https://www.wepay.com/developer/buttons/subscribe
        //https://support.wepay.com/entries/37155707-Retiring-WePay-Tools
    }

    public class GetSubscriptionPlanButtonResponse
    {
        //IMPORTANT: Buttons are being retired. Buttons are being retired. Starting March 13, 2014 you will no longer be able to accept payments using buttons. https://www.wepay.com/developer/buttons/subscribe
        //https://support.wepay.com/entries/37155707-Retiring-WePay-Tools
    }

    public class ModifySubscriptionPlanRequest
    {
        [Required]
        public Int64 subscription_plan_id { get; set; }
        [MaxLength(255)]
        public string name { get; set; }
        [MaxLength(2047)]
        public string short_description { get; set; }
        public decimal amount { get; set; }
        public decimal app_fee { get; set; }
        [MaxLength(2083)]
        public string callback_uri { get; set; }
        public Int64 trial_length { get; set; }
        public decimal setup_fee { get; set; }
        [MaxLength(255)]
        public string update_subscriptions { get; set; }
        public Int64 transition_expire_days { get; set; }
        [MaxLength(255)]
        public string reference_id { get; set; }
    }

    public class ModifySubscriptionPlanResponse
    {
        public Int64 subscription_plan_id { get; set; }
        public Int64 account_id { get; set; }
        [MaxLength(255)]
        public string name { get; set; }
        [MaxLength(2047)]
        public string short_description { get; set; }
        [MaxLength(3)]
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
        public Int64 number_of_subscriptions { get; set; }
        [MaxLength(2083)]
        public string callback_uri { get; set; }
        public Int64 trial_length { get; set; }
        public decimal setup_fee { get; set; }
        [MaxLength(255)]
        public string reference_id { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }
}
