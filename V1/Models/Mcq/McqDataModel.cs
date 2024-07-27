using System.Text.Json.Serialization;

namespace V1.Models.Mcq
{
    public class McqDataModel
    {
        [JsonPropertyName("ID")]
        public string ID { get; set; }

        [JsonPropertyName("McqUrl")]
        public string McqUrl { get; set; }

        [JsonPropertyName("TopicID")]
        public string TopicID { get; set; }
       
        [JsonPropertyName("Title")]
        public string Title { get; set; }
        
        [JsonPropertyName("TopicName")]
        public string TopicName { get; set; }

        [JsonPropertyName("SubjectName")]
        public string SubjectName { get; set; }

        [JsonPropertyName("YearName")]
        public string YearName { get; set; }

        [JsonPropertyName("TradeName")]
        public string TradeName { get; set; } 
        
        [JsonPropertyName("Remark")]
        public string Remark { get; set; }
        
        [JsonPropertyName("SubjectID")]
        public string SubjectID { get; set; }
        
        [JsonPropertyName("TradeID")]
        public string TradeID { get; set; }
        
        [JsonPropertyName("YearID")]
        public string YearID { get; set; }

    }
}
