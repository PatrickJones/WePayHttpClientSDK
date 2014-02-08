using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WePayMVC5HttpClientSDK.WePayAPITypes
{
    public class Batch
    {
        public async Task<CreateBatchResponse> CreateBatchAsync(CreateBatchRequest req)
        {
            CreateBatchResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<CreateBatchRequest, CreateBatchResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new CreateBatchResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }
    }

    public class CreateBatchRequest
    {
        [Required]
        public Int64 client_id { get; set; }
        [Required]
        [MaxLength(255)]
        public string client_secret { get; set; }
        [Required]
        public List<BatchCall> calls { get; set; }
    }

    public class CreateBatchResponse
    {
        public List<BatchCallResponses> calls { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }


    public class BatchCall
    {
        [Required]
        [MaxLength(255)]
        public string call { get; set; }
        [Required]//ONLY IF THE API CALL REQUIRES AUTHORIZATION
        public string authorization { get; set; }
        [MaxLength(255)]
        public string reference_id { get; set; }
        [Required]//ONLY IF THE API CALL REQUIRES AUTHORIZATION
        public Dictionary<string, string> parameters { get; set; }
    }

    public class BatchCallResponses
    {
        public string call { get; set; }
        public string reference_id { get; set; }
        public Dictionary<string, object> response { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }
}
