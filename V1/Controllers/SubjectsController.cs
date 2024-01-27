using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using V1.Models.Subjects;
using V1.Models.Trade;
using System.Net.Http.Json;
using System.Net.Http;
using V1.Models.Years;

namespace V1.Controllers
{
    public class SubjectsController : Controller
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly HttpResponseMessage httpResponseMessage;

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> GetSubjects()
        {
            var responseString = await client.PostAsync("https://sbstudentmcq.000webhostapp.com/StudentMCQ/API/GetSubject.php?filter",null);
            var responseContent = await responseString.Content.ReadAsStringAsync();
            var object_ = JObject.Parse(responseContent);
            var jsonData = JsonConvert.SerializeObject(object_["Subjects"]);
            List<SubjectsDataModel> tradeModels = JsonConvert.DeserializeObject<List<SubjectsDataModel>>(jsonData);
            return Json(tradeModels);
        }
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetDropDownYearData()
        {
            var responseString = await client.PostAsync("https://sbstudentmcq.000webhostapp.com/StudentMCQ/API/GetYear.php?filter", null);
            var responseContent = await responseString.Content.ReadAsStringAsync();
            var object_ = JObject.Parse(responseContent);
            var jsonData = JsonConvert.SerializeObject(object_["Year"]);
            List<YearsDataModel> tradeModels = JsonConvert.DeserializeObject<List<YearsDataModel>>(jsonData);
            return Json(tradeModels);
        }
    }
}
