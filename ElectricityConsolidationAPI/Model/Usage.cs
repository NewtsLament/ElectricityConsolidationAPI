using System.Text.Json.Serialization;

namespace ElectricityConsolidationAPI.Model
{
    public class Usage
    {
        [JsonPropertyName("dateStart")]
        public DateTime DateAndTimeStart { get; set; }
        [JsonPropertyName("dateEnd")]
        public DateTime DateAndTimeEnd { get; set; }
        [JsonPropertyName("consumption")]
        public Decimal Consumption { get; set; }
    }
}
