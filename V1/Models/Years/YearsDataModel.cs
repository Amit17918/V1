using System.Text.Json.Serialization;

namespace V1.Models.Years
{
    public class YearsDataModel
    {
        [JsonPropertyName("ID")]
        public string ID { get; set; }
        [JsonPropertyName("YearName")]
        public string YearName { get; set; }
        [JsonPropertyName("TradeID")]
        public string TradeID { get; set; }
        [JsonPropertyName("TradeName")]
        public string TradeName { get; set; }
    }
}
