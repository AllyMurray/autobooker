using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AutoBooking.Models.Models
{
    public class WhiteListedEmail
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "emailAddresses")]
        public string[] EmailAddresses { get; set; }
    }
}
