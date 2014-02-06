using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WePayMVC5HttpClientSDK
{
    public class ErrorHandler
    {
        public ErrorHandler(Exception ex)
        {
            except = ex;
        }

        public Exception except { get; set; }

        public ErrorResponse FormatError()
        {
            var eResp = new ErrorResponse();

            if (except is WePayException)
            {
                var we = except as WePayException;
                eResp.ExceptionName = "WePayException";
                eResp.Message = we.error_description;
                eResp.Exception = except;
            }

            if (except is HttpRequestException)
            {
                var hre = except as HttpRequestException;
                eResp.ExceptionName = "HttpRequestException";
                eResp.Message = hre.Message;
                eResp.Exception = except;
            }

            return eResp;
        }
    }

    public class ErrorResponse
    {
        [JsonIgnore]
        public string ExceptionName { get; set; }
        [JsonIgnore]
        public string Message { get; set; }
        [JsonIgnore]
        public Exception Exception { get; set; }
    }
}
