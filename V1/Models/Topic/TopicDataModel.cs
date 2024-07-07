using System.Text.Json.Serialization;

namespace V1.Models.Topic
{
    public class TopicDataModel
    {
        [JsonPropertyName("ID")]
        public string ID { get; set; }

        [JsonPropertyName("TradeName")]
        public string TradeName { get; set; }

        [JsonPropertyName("YearName")]
        public string YearName { get; set; }

        [JsonPropertyName("TopicName")]
        public string TopicName { get; set; }

        [JsonPropertyName("Title")]
        public string Title { get; set; }

        [JsonPropertyName("SubjectID")]
        public string SubjectID { get; set; }

        [JsonPropertyName("SubjectName")]
        public string SubjectName { get; set; }
        [JsonPropertyName("YearID")]
        public string YearID { get; set; }
        [JsonPropertyName("TradeID")]
        public string TradeID { get; set; }

    }
}
