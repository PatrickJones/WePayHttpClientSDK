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
    //IMPORTANT: Read Note on PCI Compliance https://www.wepay.com/developer/reference/credit_card
    public class CreditCard
    {
        public enum CreditCardStates
        {
            _new = 1,
            authorized = 2,
            expired = 3,
            deleted = 4,
            invalid = 5
        }

        public async Task<GetCreditCardResponse> GetCreditCardAsync(GetCreditCardRequest req)
        {
            GetCreditCardResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<GetCreditCardRequest, GetCreditCardResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new GetCreditCardResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<CreateCreditCardResponse> CreateCreditCardAsync(CreateCreditCardRequest req)
        {
            CreateCreditCardResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<CreateCreditCardRequest, CreateCreditCardResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new CreateCreditCardResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<AuthorizeCreditCardResponse> AuthorizeCreditCardAsync(AuthorizeCreditCardRequest req)
        {
            AuthorizeCreditCardResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<AuthorizeCreditCardRequest, AuthorizeCreditCardResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new AuthorizeCreditCardResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<FindCreditCardResponse> FindCreditCardAsync(FindCreditCardRequest req)
        {
            FindCreditCardResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<FindCreditCardRequest, FindCreditCardResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new FindCreditCardResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }

        public async Task<DeleteCreditCardResponse> DeleteCreditCardAsync(DeleteCreditCardRequest req)
        {
            DeleteCreditCardResponse resp;
            try
            {
                var weClient = new WePayHttpClient();
                resp = await weClient.InvokePost<DeleteCreditCardRequest, DeleteCreditCardResponse>(req);
            }
            catch (Exception ex)
            {
                resp = new DeleteCreditCardResponse { ErrorResponse = new ErrorHandler(ex).FormatError() };
            }

            return resp;
        }
    }

    public class GetCreditCardRequest
    {
        [Required]
        public Int64 client_id { get; set; }
        [Required]
        [MaxLength(255)]
        public string client_secret { get; set; }
        [Required]
        public Int64 credit_card_id { get; set; }
    }

    public class GetCreditCardResponse
    {
        public Int64 credit_card_id { get; set; }
        [MaxLength(255)]
        public string credit_card_name { get; set; }
        [MaxLength(255)]
        public string state { get; set; }
        [MaxLength(255)]
        public string user_name { get; set; }
        [MaxLength(255)]
        public string email { get; set; }
        public Int64 create_time { get; set; }
        [MaxLength(255)]
        public string reference_id { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class CreateCreditCardRequest
    {
        [Required]
        public Int64 client_id { get; set; }
        [Required]
        [MaxLength(255)]
        public string cc_number { get; set; }
        //The CVV (AKA card security code, CVV2, CVC etc) on the card.
        [Required]
        public Int32 cvv { get; set; }
        [Required]
        public Int32 expiration_month { get; set; }
        [Required]
        public Int32 expiration_year { get; set; }
        [Required]
        [MaxLength(255)]
        public string user_name { get; set; }
        [Required]
        [MaxLength(255)]
        public string email { get; set; }
        //The billing address on the card (a valid JSON object, not a JSON serialized string). 
        [Required]
        public AddressStructure address { get; set; }
        //The IP address of the user this card belongs to. This should be sent if you are not using WePay's Javascript library.
        [MaxLength(16)]
        public string original_ip { get; set; }
        //The user-agent (for web) or the IMEI (for mobile) of the user this card belongs to. This should be sent if you are not using WePay's Javascript library.
        [MaxLength(255)]
        public string original_device { get; set; }
    }

    public class CreateCreditCardResponse
    {
        public Int64 credit_card_id { get; set; }
        [MaxLength(255)]
        public string state { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class AuthorizeCreditCardRequest
    {
        [Required]
        public Int64 client_id { get; set; }
        [Required]
        [MaxLength(255)]
        public string client_secret { get; set; }
        [Required]
        public Int64 credit_card_id { get; set; }
    }

    public class AuthorizeCreditCardResponse
    {
        public Int64 credit_card_id { get; set; }
        [MaxLength(255)]
        public string state { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class FindCreditCardRequest
    {
        [Required]
        public Int64 client_id { get; set; }
        [Required]
        [MaxLength(255)]
        public string client_secret { get; set; }
        [MaxLength(255)]
        public string reference_id { get; set; }
        public Int64 limit { get; set; }
        public Int64 start { get; set; }
        [MaxLength(255)]
        public string sort_order { get; set; }
    }

    public class FindCreditCardResponse
    {
        //NOT a WePay parameter per se'..using existing type so as not create another creditcard object (think efficiency, not laziness)
        //WePay Documentation: An array of credit_cards matching the search parameters. Each element of the array will include the same data as returned from the /credit_card call. 
        public List<GetCreditCardResponse> CreditCards { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }

    public class DeleteCreditCardRequest
    {
        [Required]
        public Int64 client_id { get; set; }
        [Required]
        [MaxLength(255)]
        public string client_secret { get; set; }
        [Required]
        public Int64 credit_card_id { get; set; }
    }

    public class DeleteCreditCardResponse
    {
        public Int64 credit_card_id { get; set; }
        [MaxLength(255)]
        public string state { get; set; }

        [JsonIgnore]
        public ErrorResponse ErrorResponse { get; set; }
    }
}
