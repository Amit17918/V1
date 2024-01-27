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
    }
}
