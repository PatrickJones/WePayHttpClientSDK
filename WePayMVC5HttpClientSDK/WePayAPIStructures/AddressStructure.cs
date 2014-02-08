using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//These class structures are based on the WePay Documentation https://www.wepay.com/developer/reference/structures
namespace WePayMVC5HttpClientSDK.WePayAPIStructures
{
    //WePay Documentation has two constructions address (US & Non-Us) https://www.wepay.com/developer/reference/structures under the Address Structure
    public class AddressStructure
    {
        [MaxLength(60)]
        public string address1 { get; set; }
        [MaxLength(60)]
        public string address2 { get; set; }
        [MaxLength(30)]
        public string city { get; set; }
        [MaxLength(2)]
        public string state { get; set; }//The 2-letters US state code. Only for US addresses at https://www.usps.com/send/official-abbreviations.htm
        [MaxLength(30)]
        public string region { get; set; }
        [MaxLength(10)]
        public string zip { get; set; }
        [MaxLength(14)]
        public string postcode { get; set; }
        [MaxLength(2)]
        public string country { get; set; }//The 2-letters ISO-3166-1 country code at https://en.wikipedia.org/wiki/ISO_3166-1#Officially_assigned_code_elements

        public JObject ToJSON()
        {
            return JObject.FromObject(this);
        }
    }
}
