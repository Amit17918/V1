using System.Text.Json.Serialization;

namespace V1.Models.Trade
{
    public class TradeModel
    {
        [JsonPropertyName("Trade")]
        public List<TradeDataModel> TradeDataModel { get; set; }
    }

    public class TradeDataModel
    {
        [JsonPropertyName("ID")]
        public string ID { get; set; }
        [JsonPropertyName("TradeName")]
        public string TradeName { get; set; }
        [JsonPropertyName("Title")]
        public string Title { get; set; }
        [JsonPropertyName("TradeIcon")]
        public object TradeIcon { get; set; }
    }
}
