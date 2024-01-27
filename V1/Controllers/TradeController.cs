using Microsoft.AspNetCore.Mvc;
using System.Net;
using V1.Models.Trade;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;
using System.Reflection.Emit;

namespace V1.Controllers
{
    public class TradeController : Controller
    {
        private static readonly HttpClient client = new HttpClient();

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> GetTradeAsync()
        {
            var responseString = await client.GetStringAsync("https://sbstudentmcq.000webhostapp.com/StudentMCQ/API/GetTrade.php");
            var  object_= JObject.Parse(responseString);
            var jsonData = JsonConvert.SerializeObject(object_["Trade"]);
            List<TradeDataModel> tradeModels = JsonConvert.DeserializeObject<List<TradeDataModel>>(jsonData);
            return Json(tradeModels);
        }
    }
}
