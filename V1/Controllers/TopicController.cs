using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using V1.Models.Mcq;
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
    }
}
