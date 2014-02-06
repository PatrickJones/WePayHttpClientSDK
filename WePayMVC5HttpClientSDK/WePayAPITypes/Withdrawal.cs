using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WePayMVC5HttpClientSDK.WePayAPITypes
{
    public class Withdrawal
    {
        public enum WithdrawalStates
        {
            _new = 1,
            started = 2,
            captured = 3,
            settled = 4,
            failed = 5,
            expired = 6
        }

        public async Task<GetWithdrawalResponse> GetWithdrawalAsync(GetWithdrawalRequest req)
        {
            GetWithdrawalResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<GetWithdrawalRequest, GetWithdrawalResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new GetWithdrawalResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<FindWithdrawalResponse> FindWithdrawalAsync(FindWithdrawalRequest req)
        {
            FindWithdrawalResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<FindWithdrawalRequest, FindWithdrawalResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new FindWithdrawalResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<CreateWithdrawalResponse> CreateWithdrawalAsync(CreateWithdrawalRequest req)
        {
            CreateWithdrawalResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<CreateWithdrawalRequest, CreateWithdrawalResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new CreateWithdrawalResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<ModifyWithdrawalResponse> ModifyWithdrawalAsync(ModifyWithdrawalRequest req)
        {
            ModifyWithdrawalResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<ModifyWithdrawalRequest, ModifyWithdrawalResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new ModifyWithdrawalResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }
    }

    public class GetWithdrawalRequest
    {
        [Required]
        public Int64 withdrawal_id { get; set; }
    }

    public class GetWithdrawalResponse
    {
        public Int64 withdrawal_id { get; set; }
        public Int64 account_id { get; set; }
        [MaxLength(255)]
        public string state { get; set; }
        [MaxLength(2083)]
        public string withdrawal_uri { get; set; }
        [MaxLength(2083)]
        public string redirect_uri { get; set; }
        [MaxLength(2083)]
        public string callback_uri { get; set; }
        public decimal amount { get; set; }
        [MaxLength(3)]   
        public string currency { get; set; }
        [MaxLength(255)]
        public string note { get; set; }
        public bool recipient_confirmed { get; set; }
        [MaxLength(255)]
        public string type { get; set; }
        public Int64 create_time { get; set; }
        public Int64 capture_time { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class FindWithdrawalRequest
    {
        [Required]
        public Int64 account_id { get; set; }
        public Int64 limit { get; set; }
        public Int64 start { get; set; }
        [MaxLength(255)]
        public string sort_order { get; set; }
    }

    public class FindWithdrawalResponse
    {
        //NOT a WePay parameter per se'..using existing type so as not create another withdrawal object (think efficiency, not laziness)
        //WePay Documentation: The response will be an array of withdrawals with the same details as the /withdrawal call. 
        public List<GetWithdrawalResponse> Withdrawals { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class CreateWithdrawalRequest
    {
        [Required]
        public Int64 account_id { get; set; }
        [MaxLength(3)]
        public string currency { get; set; }
        [MaxLength(2083)]
        public string redirect_uri { get; set; }
        [MaxLength(2083)]
        public string callback_uri { get; set; }
        [MaxLength(2083)]
        public string fallback_uri { get; set; }
        [MaxLength(255)]
        public string note { get; set; }
        [MaxLength(255)]
        public string mode { get; set; }
    }

    public class CreateWithdrawalResponse
    {
        public Int64 withdrawal_id { get; set; }
        [MaxLength(2083)]
        public string withdrawal_uri { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class ModifyWithdrawalRequest
    {
        [Required]
        public Int64 withdrawal_id { get; set; }
        [MaxLength(2083)]
        public string callback_uri { get; set; }
    }

    public class ModifyWithdrawalResponse
    {
        public Int64 withdrawal_id { get; set; }
        public Int64 account_id { get; set; }
        [MaxLength(255)]
        public string state { get; set; }
        [MaxLength(2083)]
        public string withdrawal_uri { get; set; }
        [MaxLength(2083)]
        public string redirect_uri { get; set; }
        [MaxLength(2083)]
        public string callback_uri { get; set; }
        public decimal amount { get; set; }
        [MaxLength(3)]
        public string currency { get; set; }
        [MaxLength(255)]
        public string note { get; set; }
        public bool recipient_confirmed { get; set; }
        [MaxLength(255)]
        public string type { get; set; }
        public Int64 create_time { get; set; }
        public Int64 capture_time { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }
}
