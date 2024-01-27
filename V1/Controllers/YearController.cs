using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using V1.Models.Topic;
using V1.Models.Years;
using V1.Models.Trade;

namespace V1.Controllers
{
    public class YearController : Controller
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly HttpResponseMessage httpResponseMessage;

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> GetYear()
        {
            var responseString = await client.PostAsync("https://sbstudentmcq.000webhostapp.com/StudentMCQ/API/GetYear.php?filter", null);
            var responseContent = await responseString.Content.ReadAsStringAsync();
            var object_ = JObject.Parse(responseContent);
            var jsonData = JsonConvert.SerializeObject(object_["Year"]);
            List<YearsDataModel> tradeModels = JsonConvert.DeserializeObject<List<YearsDataModel>>(jsonData);
            return Json(tradeModels);
        }
        
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetDropDownTradeData()
        {
            var responseString = await client.GetStringAsync("https://sbstudentmcq.000webhostapp.com/StudentMCQ/API/GetTrade.php");
            var object_ = JObject.Parse(responseString);
            var jsonData = JsonConvert.SerializeObject(object_["Trade"]);
            List<TradeDataModel> tradeModels = JsonConvert.DeserializeObject<List<TradeDataModel>>(jsonData);
            return Json(tradeModels);
        }
    }
}
