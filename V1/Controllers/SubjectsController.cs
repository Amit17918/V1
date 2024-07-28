using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using V1.Models.Subjects;
using V1.Models.Trade;
using System.Net.Http.Json;
using System.Net.Http;
using V1.Models.Years;
using System.Text.Json;
using V1.Models;

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
        public async Task<IActionResult> GetDropDownYearData(string TradeID)
        {
            string request = "and Trade.ID='"+ TradeID + "'";
            List<YearsDataModel> tradeModels = new List<YearsDataModel>();
            try
            {
                var responseString = await client.PostAsync("https://sbstudentmcq.000webhostapp.com/StudentMCQ/API/GetYear.php?filter=" + request, null);
                var responseContent = await responseString.Content.ReadAsStringAsync();
                var object_ = JObject.Parse(responseContent);
                var jsonData = JsonConvert.SerializeObject(object_["Year"]);
                tradeModels = JsonConvert.DeserializeObject<List<YearsDataModel>>(jsonData);
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return Json(tradeModels);
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateInsertData()
        {
            var httpRequest = HttpContext.Request;
            SubjectsDataModel subjectsDataModel = new SubjectsDataModel();
            subjectsDataModel.SubjectName = httpRequest.Form["SubjectName"];
            subjectsDataModel.ID = httpRequest.Form["ID"];
            subjectsDataModel.YearID = httpRequest.Form["YearID"];
            try
            {
                string request = "SubjectName=" + subjectsDataModel.SubjectName + "&YearID=" + subjectsDataModel.YearID + "&SubjectID=" + subjectsDataModel.ID;
                var responseString = await client.GetAsync("https://sbstudentmcq.000webhostapp.com/StudentMCQ/API/GetSubject.php?" + request);
                var responseContent = await responseString.Content.ReadAsStringAsync();
                var object_ = JObject.Parse(responseContent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return Json(subjectsDataModel);
        }
    }
}
