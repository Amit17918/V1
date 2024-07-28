using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using V1.Models.Mcq;
using V1.Models.Topic;
using V1.Models.Trade;

namespace V1.Controllers
{
    public class McqController : Controller
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly HttpResponseMessage httpResponseMessage;

        [HttpPost]
        public async Task<IActionResult> GetMcq()
        {
            var responseString = await client.PostAsync("https://sbstudentmcq.000webhostapp.com/StudentMCQ/API/GetMcq.php?filter", null);
            var responseContent = await responseString.Content.ReadAsStringAsync();
            var object_ = JObject.Parse(responseContent);
            var jsonData = JsonConvert.SerializeObject(object_["MCQ"]);
            List<McqDataModel> mcqDataModellist = JsonConvert.DeserializeObject<List<McqDataModel>>(jsonData);
            return Json(mcqDataModellist);
        }

        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetDropDownTopicData(string SubjectsID)
        {
            string request = "and Subject.ID='" + SubjectsID + "'";
            var responseString = await client.PostAsync("https://sbstudentmcq.000webhostapp.com/StudentMCQ/API/GetTopic.php?filter="+ request, null);
            var responseContent = await responseString.Content.ReadAsStringAsync();
            var object_ = JObject.Parse(responseContent);
            var jsonData = JsonConvert.SerializeObject(object_["Topics"]);
            List<TopicDataModel> tradeModels = JsonConvert.DeserializeObject<List<TopicDataModel>>(jsonData);
            return Json(tradeModels);
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateInsertData()
        {
            var httpRequest = HttpContext.Request;
            McqDataModel mcqDataModel = new McqDataModel();
            mcqDataModel.McqUrl = httpRequest.Form["McqUrl"];
            mcqDataModel.ID = httpRequest.Form["McqID"];
            mcqDataModel.TopicID = httpRequest.Form["TopicID"];
            mcqDataModel.Remark = httpRequest.Form["Remark"];
            try
            {
                string request = "McqUrl=" + mcqDataModel.McqUrl + "&TopicID=" + mcqDataModel.TopicID + "&Remark=" + mcqDataModel.Remark+ "&McqID=" + mcqDataModel.ID;
                var responseString = await client.GetAsync("https://sbstudentmcq.000webhostapp.com/StudentMCQ/API/GetMcq.php?" + request);
                var responseContent = await responseString.Content.ReadAsStringAsync();
                var object_ = JObject.Parse(responseContent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return Json(mcqDataModel);
        }
    }
}
