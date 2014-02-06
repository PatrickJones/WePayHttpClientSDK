using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WePayMVC5HttpClientSDK
{
    public static class WePayUrls
    {
        // <summary>
        /// Used by WePayHttpClient.cs Invoke* methods to set url to call by comparing request parameter (RequestT) name to key to get url
        /// </summary>
        public static Dictionary<string, string> WePayActionUrls = new Dictionary<string, string>
        {
            {"AuthorizationRequest","oauth2/authorize"},
            {"TokenRequest","oauth2/token"},
            {"GetAppRequest","app"},
            {"ModifyAppRequest","app/modify"},
            {"GetUserRequest","user"},
            {"ModifyUserRequest","user/modify"},
            {"RegisterUserRequest","user/register"},
            {"ResendUserConfirmationRequest","user/resend_confirmation"},
            {"GetAccountRequest","account"},
            {"FindAccountRequest","account/find"},
            {"CreateAccountRequest","account/create"},
            {"ModifyAccountRequest","account/modify"},
            {"DeleteAccountRequest","account/delete"},
            {"GetAccountUpdateUriRequest","account/get_update_uri"},
            {"GetAccountReserveDetailsRequest","account/get_reserve_details"},
            {"GetCheckoutRequest","checkout"},
            {"FindCheckoutRequest","checkout/find"},
            {"CreateCheckoutRequest","checkout/create"},
            {"CancelCheckoutRequest","checkout/cancel"},
            {"RefundCheckoutRequest","checkout/refund"},
            {"CaptureCheckoutRequest","checkout/capture"},
            {"ModifyCheckoutRequest","checkout/modify"},
            {"GetPreapprovalRequest","preapproval"},
            {"FindPreapprovalRequest","preapproval/find"},
            {"CreatePreapprovalRequest","preapproval/create"},
            {"CancelPreapprovalRequest","preapproval/cancel"},
            {"ModifyPreapprovalRequest","preapproval/modify"},
            {"GetWithdrawalRequest","withdrawal"},
            {"FindWithdrawalRequest","withdrawal/find"},
            {"CreateWithdrawalRequest","withdrawal/create"},
            {"ModifyWithdrawalRequest","withdrawal/modify"},
            {"GetCreditCardRequest","credit_card"},
            {"CreateCreditCardRequest","credit_card/create"},
            {"AuthorizeCreditCardRequest","credit_card/authorize"},
            {"FindCreditCardRequest","credit_card/find"},
            {"DeleteCreditCardRequest","credit_card/delete"},
            {"GetSubscriptionPlanRequest","subscription_plan"},
            {"FindSubscriptionPlanRequest","subscription_plan/find"},
            {"CreateSubscriptionPlanRequest","subscription_plan/create"},
            {"DeleteSubscriptionPlanRequest","subscription_plan/delete"},
            {"GetSubscriptionPlanButtonRequest","subscription_plan/get_button"},
            {"ModifySubscriptionPlanRequest","subscription_plan/modify"},
            {"GetSubscriptionRequest","subscription"},
            {"FindSubscriptionRequest","subscription/find"},
            {"CreateSubscriptionRequest","subscription/create"},
            {"CancelSubscriptionRequest","subscription/cancel"},
            {"ModifySubscriptionRequest","subscription/modify"},
            {"GetSubscriptionChargeRequest","subscription_charge"},
            {"FindSubscriptionChargeRequest","subscription_charge/find"},
            {"RefundSubscriptionChargeRequest","subscription_charge/refund"},
            {"CreateBatchRequest","batch/create"}
        };
    }
}
