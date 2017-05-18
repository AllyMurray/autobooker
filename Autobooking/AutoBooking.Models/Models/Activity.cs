using Newtonsoft.Json;

namespace AutoBooking.Models.Models
{
    public class Activity
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }
    }
}
