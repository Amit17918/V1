using System.Text.Json.Serialization;

namespace V1.Models.Subjects
{
    public class SubjectsDataModel
    {
        [JsonPropertyName("ID")]
        public string ID { get; set; }
        [JsonPropertyName("SubjectName")]
        public string SubjectName { get; set; }
        [JsonPropertyName("TradeName")]
        public string TradeName { get; set; }
        [JsonPropertyName("Title")]
        public string Title { get; set; }
        [JsonPropertyName("YearID")]
        public string YearID { get; set; }
        [JsonPropertyName("YearName")]
        public string YearName { get; set; }
        [JsonPropertyName("TradeID")]
        public string TradeID { get; set; }
    }
}
