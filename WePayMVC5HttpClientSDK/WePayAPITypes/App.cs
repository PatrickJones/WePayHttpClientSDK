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
    //WePay Documentation: https://www.wepay.com/developer/reference/app
    public class App
    {
        public enum AppStates
        {
            active = 1, disabled = 2, deleted = 3
        }

        public async Task<GetAppResponse> GetAppAsync(GetAppRequest req)
        {
            GetAppResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<GetAppRequest, GetAppResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new GetAppResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<ModifyAppResponse> ModifyAppAsync(ModifyAppRequest req)
        {
            ModifyAppResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<ModifyAppRequest, ModifyAppResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new ModifyAppResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }
    }

    public class GetAppRequest
    {
        [Required]
        public Int64 client_id { get; set; }
        [Required]
        [MaxLength(255)]
        public string client_secret { get; set; }
    }

    public class GetAppResponse
    {
        public Int64 client_id { get; set; }
        [MaxLength(255)]
        public string status { get; set; }
        //"The 'theme structure' (a JSON object, not a JSON serialized string) you want to be used for app's flows and emails"
        public JObject theme_object { get; set; }
        //An array of Google Analytics domains associated with the app. https://www.wepay.com/developer/tutorial/analytics
        public List<string> gaq_domains { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    //Documentation says only "client_id" and "client_secret" are 'Required', but all are (400 bad request otherwise)
    public class ModifyAppRequest
    {
        [Required]
        public Int64 client_id { get; set; }
        [Required]
        [MaxLength(255)]
        public string client_secret { get; set; }
        //"The 'theme structure' (a JSON object, not a JSON serialized string) you want to be used for app's flows and emails"
        [Required]
        public JObject theme_object { get; set; }
        //An array of Google Analytics domains associated with the app. https://www.wepay.com/developer/tutorial/analytics
        [Required]
        public List<string> gaq_domains { get; set; }
    }

    public class ModifyAppResponse
    {
        public Int64 client_id { get; set; }
        [MaxLength(255)]
        public string status { get; set; }
        //"The 'theme structure' (a JSON object, not a JSON serialized string) you want to be used for app's flows and emails"
        public JObject theme_object { get; set; }
        //An array of Google Analytics domains associated with the app. https://www.wepay.com/developer/tutorial/analytics
        public List<string> gaq_domains { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }
}
