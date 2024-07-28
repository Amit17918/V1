using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using V1.Models.Mcq;
using V1.Models.Subjects;
using V1.Models.Topic;

namespace V1.Controllers
{
    public class TopicController : Controller
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly HttpResponseMessage httpResponseMessage;

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> GetTopic()
        {
            var responseString = await client.PostAsync("https://sbstudentmcq.000webhostapp.com/StudentMCQ/API/GetTopic.php?filter", null);
            var responseContent = await responseString.Content.ReadAsStringAsync();
            var object_ = JObject.Parse(responseContent);
            var jsonData = JsonConvert.SerializeObject(object_["Topics"]);
            List<TopicDataModel> tradeModels = JsonConvert.DeserializeObject<List<TopicDataModel>>(jsonData);
            return Json(tradeModels);
        }

        [HttpGet]
        public async Task<IActionResult> GetDropDownSubjectsData(string YearID)
        {
            string request = "and Year.ID='" + YearID + "'";
            var responseString = await client.PostAsync("https://sbstudentmcq.000webhostapp.com/StudentMCQ/API/GetSubject.php?filter="+ request, null);
            var responseContent = await responseString.Content.ReadAsStringAsync();
            var object_ = JObject.Parse(responseContent);
            var jsonData = JsonConvert.SerializeObject(object_["Subjects"]);
            List<SubjectsDataModel> tradeModels = JsonConvert.DeserializeObject<List<SubjectsDataModel>>(jsonData);
            return Json(tradeModels);
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateInsertData()
        {
            var httpRequest = HttpContext.Request;
            TopicDataModel topicDataModel = new TopicDataModel();
            topicDataModel.TopicName = httpRequest.Form["TopicName"];
            topicDataModel.ID = httpRequest.Form["ID"];
            topicDataModel.SubjectID = httpRequest.Form["SubjectID"];
            try
            {
                string request = "TopicName=" + topicDataModel.TopicName + "&SubjectID=" + topicDataModel.SubjectID + "&TopicID=" + topicDataModel.ID;
                var responseString = await client.GetAsync("https://sbstudentmcq.000webhostapp.com/StudentMCQ/API/GetTopic.php?" + request);
                var responseContent = await responseString.Content.ReadAsStringAsync();
                var object_ = JObject.Parse(responseContent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return Json(topicDataModel);
        }
    }
}
