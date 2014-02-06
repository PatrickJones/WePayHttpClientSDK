using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WePayMVC5HttpClientSDK.WePayAPITypes;

namespace WePayMVC5HttpClientSDK
{
    /// <summary>
    /// Methods to call WePay Service using HttpClient.
    /// There are seperate 'InvokeGet' and 'InvokePost' mehtods because we are using HttpRequestMessage,
    /// which requires specific 'GET' or 'POST' parameter. Don't worry, each WePayAPIType is set to invoke the
    /// right one for you.
    /// </summary>
    public class WePayHttpClient
    {
        public WePayHttpClient()
        {
            //needed to handle null values in 'Request' serialization, so you don't have to set EVERY property.
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            { 
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        public async Task<ResponseT> InvokePost<RequestT, ResponseT>(RequestT request) where ResponseT : class
                                                                                       where RequestT : class     
        {
            string callAction = "";
            ResponseT result;
            HttpClient h_client = new HttpClient();

            //get action to call from dictionary
            if (WePayConfiguration.ActionUrls.ContainsKey(typeof(RequestT).Name))
	        {
		        callAction = WePayConfiguration.ActionUrls[typeof(RequestT).Name];
	        }

            //string uri for request
            string wpUri = WePayConfiguration.endpoint(WePayConfiguration.productionMode) + callAction;
            
            //construct request message
            var h_request = new HttpRequestMessage(HttpMethod.Post, wpUri);
            h_request.Headers.UserAgent.TryParseAdd("WePay C# HttpClient SDK");
            h_request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", WePayConfiguration.accessToken);

            h_request.Content = new StringContent(JsonConvert.SerializeObject(request), UTF8Encoding.UTF8, "application/json");
           
            try
            {
                HttpResponseMessage response = await h_client.SendAsync(h_request);

                //if call failed will recieve error message from WePay : https://www.wepay.com/developer/reference/errors
                if (!response.IsSuccessStatusCode)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    WePayError weEx = JsonConvert.DeserializeObject<WePayError>(error);
                    throw new WePayException { error = weEx.error, error_description = weEx.error_description, error_code = weEx.error_code };
                }

                string success = await response.Content.ReadAsStringAsync();

                //if true, then it is an Array...and it is a problem for JsonConvert
                //this is ONLY NECESSARY when response an array, NOT and object that has an array IN it as a property
                if (success.StartsWith("["))
                {   
                    //get class name to use in switch statement
                    var respType = typeof(ResponseT).Name;

                    switch (respType)
                    {
                        case "FindAccountResponse":
                            FindAccountResponse far = new FindAccountResponse();
                            far.accounts = JsonConvert.DeserializeObject<List<GetAccountResponse>>(success);
                            return far as ResponseT;
                        case "FindCheckoutResponse":
                            FindCheckoutResponse fcr = new FindCheckoutResponse();
                            fcr.checkouts = JsonConvert.DeserializeObject<List<GetCheckoutResponse>>(success);
                            return fcr as ResponseT;
                        case "FindPreapprovalResponse":
                            FindPreapprovalResponse fpr = new FindPreapprovalResponse();
                            fpr.Preapprovals = JsonConvert.DeserializeObject<List<GetPreapprovalResponse>>(success);
                            return fpr as ResponseT;
                        case "FindWithdrawalResponse":
                            FindWithdrawalResponse fwr = new FindWithdrawalResponse();
                            fwr.Withdrawals = JsonConvert.DeserializeObject<List<GetWithdrawalResponse>>(success);
                            return fwr as ResponseT;
                        case "FindCreditCardResponse":
                            FindCreditCardResponse fccr = new FindCreditCardResponse();
                            fccr.CreditCards = JsonConvert.DeserializeObject<List<GetCreditCardResponse>>(success);
                            return fccr as ResponseT;
                        case "FindSubscriptionPlanResponse":
                            FindSubscriptionPlanResponse fspr = new FindSubscriptionPlanResponse();
                            fspr.SubscriptionPlans = JsonConvert.DeserializeObject<List<GetSubscriptionPlanResponse>>(success);
                            return fspr as ResponseT;
                        case "FindSubscriptionResponse":
                            FindSubscriptionResponse fsr = new FindSubscriptionResponse();
                            fsr.Subscriptions = JsonConvert.DeserializeObject<List<GetSubscriptionResponse>>(success);
                            return fsr as ResponseT;
                        case "FindSubscriptionChargeResponse":
                            FindSubscriptionChargeResponse fscr = new FindSubscriptionChargeResponse();
                            fscr.SubscriptionCharges = JsonConvert.DeserializeObject<List<GetSubscriptionChargeResponse>>(success);
                            return fscr as ResponseT;
                        default:
                            throw new HttpRequestException("Unable to convert response JSON Array.");
                    }
                }

                result = JsonConvert.DeserializeObject<ResponseT>(success);
            }
            catch (Exception)
            {
                throw new HttpRequestException("Request to WePay Service failed.");
            }

            return result;
        }

        //use case: need to dynamically pass in access token
        public async Task<ResponseT> InvokePost<RequestT, ResponseT>(RequestT request, string accessToken)
            where ResponseT : class
            where RequestT : class     
        {
            string callAction = "";
            ResponseT result;
            HttpClient h_client = new HttpClient();

            //get action to call from dictionary
            if (WePayConfiguration.ActionUrls.ContainsKey(typeof(RequestT).Name))
            {
                callAction = WePayConfiguration.ActionUrls[typeof(RequestT).Name];
            }

            //string uri for request
            string wpUri = WePayConfiguration.endpoint(WePayConfiguration.productionMode) + callAction;

            //construct request message
            var h_request = new HttpRequestMessage(HttpMethod.Post, wpUri);
            h_request.Headers.UserAgent.TryParseAdd("WePay C# HttpClient SDK");
            h_request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            h_request.Content = new StringContent(JsonConvert.SerializeObject(request), UTF8Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await h_client.SendAsync(h_request);

                //if call failed will recieve error message from WePay : https://www.wepay.com/developer/reference/errors
                if (!response.IsSuccessStatusCode)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    WePayError weEx = JsonConvert.DeserializeObject<WePayError>(error);
                    throw new WePayException { error = weEx.error, error_description = weEx.error_description, error_code = weEx.error_code };
                }

                string success = await response.Content.ReadAsStringAsync();

                //if true, then it is an Array...and it is a problem for JsonConvert
                //this is ONLY NECESSARY when response an array, NOT and object that has an array IN it as a property
                if (success.StartsWith("["))
                {
                    //get class name to use in switch statement
                    var respType = typeof(ResponseT).Name;

                    switch (respType)
                    {
                        case "FindAccountResponse":
                            FindAccountResponse far = new FindAccountResponse();
                            far.accounts = JsonConvert.DeserializeObject<List<GetAccountResponse>>(success);
                            return far as ResponseT;
                        case "FindCheckoutResponse":
                            FindCheckoutResponse fcr = new FindCheckoutResponse();
                            fcr.checkouts = JsonConvert.DeserializeObject<List<GetCheckoutResponse>>(success);
                            return fcr as ResponseT;
                        case "FindPreapprovalResponse":
                            FindPreapprovalResponse fpr = new FindPreapprovalResponse();
                            fpr.Preapprovals = JsonConvert.DeserializeObject<List<GetPreapprovalResponse>>(success);
                            return fpr as ResponseT;
                        case "FindWithdrawalResponse":
                            FindWithdrawalResponse fwr = new FindWithdrawalResponse();
                            fwr.Withdrawals = JsonConvert.DeserializeObject<List<GetWithdrawalResponse>>(success);
                            return fwr as ResponseT;
                        case "FindCreditCardResponse":
                            FindCreditCardResponse fccr = new FindCreditCardResponse();
                            fccr.CreditCards = JsonConvert.DeserializeObject<List<GetCreditCardResponse>>(success);
                            return fccr as ResponseT;
                        case "FindSubscriptionPlanResponse":
                            FindSubscriptionPlanResponse fspr = new FindSubscriptionPlanResponse();
                            fspr.SubscriptionPlans = JsonConvert.DeserializeObject<List<GetSubscriptionPlanResponse>>(success);
                            return fspr as ResponseT;
                        case "FindSubscriptionResponse":
                            FindSubscriptionResponse fsr = new FindSubscriptionResponse();
                            fsr.Subscriptions = JsonConvert.DeserializeObject<List<GetSubscriptionResponse>>(success);
                            return fsr as ResponseT;
                        case "FindSubscriptionChargeResponse":
                            FindSubscriptionChargeResponse fscr = new FindSubscriptionChargeResponse();
                            fscr.SubscriptionCharges = JsonConvert.DeserializeObject<List<GetSubscriptionChargeResponse>>(success);
                            return fscr as ResponseT;
                        default:
                            throw new HttpRequestException("Unable to convert response JSON Array.");
                    }
                }

                result = JsonConvert.DeserializeObject<ResponseT>(success);
            }
            catch (Exception)
            {
                throw new HttpRequestException("Request to WePay Service failed.");
            }

            return result;
        }

        public async Task<ResponseT> InvokeGet<RequestT, ResponseT>()
            where ResponseT : class
            where RequestT : class     
        {
            string callAction = "";
            ResponseT result;
            HttpClient h_client = new HttpClient();

            //get action to call from dictionary
            if (WePayConfiguration.ActionUrls.ContainsKey(typeof(RequestT).Name))
            {
                callAction = WePayConfiguration.ActionUrls[typeof(RequestT).Name];
            }

            //string uri for request
            string wpUri = WePayConfiguration.endpoint(WePayConfiguration.productionMode) + callAction;

            //construct request message
            var h_request = new HttpRequestMessage(HttpMethod.Get, wpUri);
            h_request.Headers.UserAgent.TryParseAdd("WePay C# HttpClient SDK");
            h_request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", WePayConfiguration.accessToken);

            try
            {
                HttpResponseMessage response = await h_client.SendAsync(h_request);

                //if call failed will recieve error message from WePay : https://www.wepay.com/developer/reference/errors
                if (!response.IsSuccessStatusCode)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    WePayError weEx = JsonConvert.DeserializeObject<WePayError>(error);
                    throw new WePayException { error = weEx.error, error_description = weEx.error_description, error_code = weEx.error_code };
                }

                string success = await response.Content.ReadAsStringAsync();

                //if true, then it is an Array...and it is a problem for JsonConvert
                //this is ONLY NECESSARY when response an array, NOT and object that has an array IN it as a property
                if (success.StartsWith("["))
                {
                    //get class name to use in switch statement
                    var respType = typeof(ResponseT).Name;

                    switch (respType)
                    {
                        case "FindAccountResponse":
                            FindAccountResponse far = new FindAccountResponse();
                            far.accounts = JsonConvert.DeserializeObject<List<GetAccountResponse>>(success);
                            return far as ResponseT;
                        case "FindCheckoutResponse":
                            FindCheckoutResponse fcr = new FindCheckoutResponse();
                            fcr.checkouts = JsonConvert.DeserializeObject<List<GetCheckoutResponse>>(success);
                            return fcr as ResponseT;
                        case "FindPreapprovalResponse":
                            FindPreapprovalResponse fpr = new FindPreapprovalResponse();
                            fpr.Preapprovals = JsonConvert.DeserializeObject<List<GetPreapprovalResponse>>(success);
                            return fpr as ResponseT;
                        case "FindWithdrawalResponse":
                            FindWithdrawalResponse fwr = new FindWithdrawalResponse();
                            fwr.Withdrawals = JsonConvert.DeserializeObject<List<GetWithdrawalResponse>>(success);
                            return fwr as ResponseT;
                        case "FindCreditCardResponse":
                            FindCreditCardResponse fccr = new FindCreditCardResponse();
                            fccr.CreditCards = JsonConvert.DeserializeObject<List<GetCreditCardResponse>>(success);
                            return fccr as ResponseT;
                        case "FindSubscriptionPlanResponse":
                            FindSubscriptionPlanResponse fspr = new FindSubscriptionPlanResponse();
                            fspr.SubscriptionPlans = JsonConvert.DeserializeObject<List<GetSubscriptionPlanResponse>>(success);
                            return fspr as ResponseT;
                        case "FindSubscriptionResponse":
                            FindSubscriptionResponse fsr = new FindSubscriptionResponse();
                            fsr.Subscriptions = JsonConvert.DeserializeObject<List<GetSubscriptionResponse>>(success);
                            return fsr as ResponseT;
                        case "FindSubscriptionChargeResponse":
                            FindSubscriptionChargeResponse fscr = new FindSubscriptionChargeResponse();
                            fscr.SubscriptionCharges = JsonConvert.DeserializeObject<List<GetSubscriptionChargeResponse>>(success);
                            return fscr as ResponseT;
                        default:
                            throw new HttpRequestException("Unable to convert response JSON Array.");
                    }
                }

                result = JsonConvert.DeserializeObject<ResponseT>(success);
            }
            catch (Exception)
            {
                throw new HttpRequestException("Request to WePay Service failed.");
            }

            return result;
        }

        public async Task<ResponseT> InvokeGet<RequestT, ResponseT>(string accessToken)
            where ResponseT : class
            where RequestT : class     
        {
            string callAction = "";
            ResponseT result;
            HttpClient h_client = new HttpClient();

            //get action to call from dictionary
            if (WePayConfiguration.ActionUrls.ContainsKey(typeof(RequestT).Name))
            {
                callAction = WePayConfiguration.ActionUrls[typeof(RequestT).Name];
            }

            //string uri for request
            string wpUri = WePayConfiguration.endpoint(WePayConfiguration.productionMode) + callAction;

            //construct request message
            var h_request = new HttpRequestMessage(HttpMethod.Get, wpUri);
            h_request.Headers.UserAgent.TryParseAdd("WePay C# HttpClient SDK");
            h_request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            try
            {
                HttpResponseMessage response = await h_client.SendAsync(h_request);

                //if call failed will recieve error message from WePay : https://www.wepay.com/developer/reference/errors
                if (!response.IsSuccessStatusCode)
                {
                    string error = await response.Content.ReadAsStringAsync();
                    WePayError weEx = JsonConvert.DeserializeObject<WePayError>(error);
                    throw new WePayException { error = weEx.error, error_description = weEx.error_description, error_code = weEx.error_code };
                }

                string success = await response.Content.ReadAsStringAsync();

                //if true, then it is an Array...and it is a problem for JsonConvert
                //this is ONLY NECESSARY when response an array, NOT and object that has an array IN it as a property
                if (success.StartsWith("["))
                {
                    //get class name to use in switch statement
                    var respType = typeof(ResponseT).Name;

                    switch (respType)
                    {
                        case "FindAccountResponse":
                            FindAccountResponse far = new FindAccountResponse();
                            far.accounts = JsonConvert.DeserializeObject<List<GetAccountResponse>>(success);
                            return far as ResponseT;
                        case "FindCheckoutResponse":
                            FindCheckoutResponse fcr = new FindCheckoutResponse();
                            fcr.checkouts = JsonConvert.DeserializeObject<List<GetCheckoutResponse>>(success);
                            return fcr as ResponseT;
                        case "FindPreapprovalResponse":
                            FindPreapprovalResponse fpr = new FindPreapprovalResponse();
                            fpr.Preapprovals = JsonConvert.DeserializeObject<List<GetPreapprovalResponse>>(success);
                            return fpr as ResponseT;
                        case "FindWithdrawalResponse":
                            FindWithdrawalResponse fwr = new FindWithdrawalResponse();
                            fwr.Withdrawals = JsonConvert.DeserializeObject<List<GetWithdrawalResponse>>(success);
                            return fwr as ResponseT;
                        case "FindCreditCardResponse":
                            FindCreditCardResponse fccr = new FindCreditCardResponse();
                            fccr.CreditCards = JsonConvert.DeserializeObject<List<GetCreditCardResponse>>(success);
                            return fccr as ResponseT;
                        case "FindSubscriptionPlanResponse":
                            FindSubscriptionPlanResponse fspr = new FindSubscriptionPlanResponse();
                            fspr.SubscriptionPlans = JsonConvert.DeserializeObject<List<GetSubscriptionPlanResponse>>(success);
                            return fspr as ResponseT;
                        case "FindSubscriptionResponse":
                            FindSubscriptionResponse fsr = new FindSubscriptionResponse();
                            fsr.Subscriptions = JsonConvert.DeserializeObject<List<GetSubscriptionResponse>>(success);
                            return fsr as ResponseT;
                        case "FindSubscriptionChargeResponse":
                            FindSubscriptionChargeResponse fscr = new FindSubscriptionChargeResponse();
                            fscr.SubscriptionCharges = JsonConvert.DeserializeObject<List<GetSubscriptionChargeResponse>>(success);
                            return fscr as ResponseT;
                        default:
                            throw new HttpRequestException("Unable to convert response JSON Array.");
                    }
                }

                result = JsonConvert.DeserializeObject<ResponseT>(success);
            }
            catch (Exception)
            {
                throw new HttpRequestException("Request to WePay Service failed.");
            }

            return result;
        }
    }

    public class WePayError
    {
        public string error { get; set; }
        public string error_description { get; set; }
        public string error_code { get; set; }
    }

    public class WePayException : Exception
    {
        public string error { get; set; }
        public string error_description { get; set; }
        public string error_code { get; set; }
    }
}
