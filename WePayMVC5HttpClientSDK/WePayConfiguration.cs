using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WePayMVC5HttpClientSDK
{
    public static class WePayConfiguration
    {
        public static string accessToken { get; set; }
        public static int accountId { get; set; }
        public static int clientId { get; set; }
        public static string clientSecret { get; set; }
        public static bool productionMode { get; set; }
        public static string authScope = "manage_accounts,view_balance,collect_payments,refund_payments,view_user,preapprove_payments,send_money";
        public static string endpoint(bool prod)
        {
            if (prod) return @"https://wepayapi.com/v2/";
            return @"https://stage.wepayapi.com/v2/";
        }

        // Used by WePayHttpClient.cs Invoke* methods to set url to call by comparing request parameter (RequestT) name to key to get url
        public static Dictionary<string, string> ActionUrls = WePayUrls.WePayActionUrls;
        
    }
}
