using Microsoft.AspNetCore.Mvc;
using System.Net;
using V1.Models.Trade;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;
using System.Reflection.Emit;
using System.Text.Json.Nodes;

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

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateInsertData()
        {
            var httpRequest = HttpContext.Request;
            TradeDataModel tradeDataModel = new TradeDataModel();
            tradeDataModel.TradeName = httpRequest.Form["tradeName"];
            tradeDataModel.ID = httpRequest.Form["ID"];
            try
            {
                string request = "TradeName=" + tradeDataModel.TradeName + "&TradeID=" + tradeDataModel.ID + "&TradeIcon=";
                var responseString = await client.PostAsync("https://sbstudentmcq.000webhostapp.com/StudentMCQ/API/GetTrade.php?" + request, null);
                var responseContent = await responseString.Content.ReadAsStringAsync();
                var object_ = JObject.Parse(responseContent);
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return Json(tradeDataModel);
        }
    }
}
