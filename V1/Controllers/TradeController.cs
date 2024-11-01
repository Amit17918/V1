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
using V1.Utils;
using System.Data.SqlClient;
using System;
using System.Data;

namespace V1.Controllers
{
    public class TradeController : Controller
    {
        private static readonly HttpClient client = new HttpClient();
        SqlConnrctor connrctor = new SqlConnrctor();

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> GetTradeAsync()
        {
            
            List<TradeDataModel> tradeModels = new List<TradeDataModel>();
            try
            {
                SqlConnection con = connrctor.Connection();
                con.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM " + TableName.T_TRADE, con);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                var dataTable = new DataTable();
                dataTable.Load(reader);
                if (dataTable.Rows.Count > 0)
                {
                    var serializedMyObjects = JsonConvert.SerializeObject(dataTable);
                    tradeModels = (List<TradeDataModel>)JsonConvert.DeserializeObject(serializedMyObjects, typeof(List<TradeDataModel>));
                }
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connrctor.Close();
            }
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
                SqlConnection con = connrctor.Connection();
                con.Open();
                if (tradeDataModel.ID == null || tradeDataModel.ID == "")
                {
                    string query = "INSERT INTO "+TableName.T_TRADE+" ("+Constants.C_TRADENAME+ ", "+Constants.C_TITLE+") VALUES(@TradeName, @Title)";
                    SqlCommand cmd = new SqlCommand(query, con);

                    //Pass values to Parameters
                    cmd.Parameters.AddWithValue("@TradeName", tradeDataModel.TradeName);
                    cmd.Parameters.AddWithValue("@Title", tradeDataModel.TradeName);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    string query = "UPDATE "+TableName.T_TRADE+" SET "+Constants.C_TRADENAME+ " = @TradeName, "+Constants.C_TITLE+ " = @Title WHERE " + Constants.C_ID + " = @id";
                    SqlCommand cmd = new SqlCommand(query, con);

                    //Pass values to Parameters
                    cmd.Parameters.AddWithValue("@TradeName", tradeDataModel.TradeName);
                    cmd.Parameters.AddWithValue("@Title", tradeDataModel.TradeName);
                    cmd.Parameters.AddWithValue("@id", tradeDataModel.ID);
                    cmd.ExecuteNonQuery();
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                connrctor.Close();
            }
            return Json(tradeDataModel);
        }
    }
}
