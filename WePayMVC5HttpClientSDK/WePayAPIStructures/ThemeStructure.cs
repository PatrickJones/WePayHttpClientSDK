using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//These class structures are based on the WePay Documentation https://www.wepay.com/developer/reference/structures
//Documentation doesn't specify 'Required', but all are (400 bad request otherwise), except "theme_id"
namespace WePayMVC5HttpClientSDK.WePayAPIStructures
{
    public class ThemeStructure
    {
        [JsonIgnore]
        public int theme_id { get; set; }
        [Required]
        [MaxLength(255)]
        public string name { get; set; }
        [Required]
        [MaxLength(15)]
        public string primary_color { get; set; }
        [Required]
        [MaxLength(255)]
        public string secondary_color { get; set; }
        [Required]
        [MaxLength(30)]
        public string background_color { get; set; }
        [Required]
        [MaxLength(2)]
        public string button_color { get; set; }

        public JObject ToJSON()
        {
            return JObject.FromObject(this);
        }
    }
}