using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using V1.Models.Topic;
using V1.Models.Years;
using V1.Models.Trade;
using System.Data.SqlClient;
using System.Data;
using V1.Utils;

namespace V1.Controllers
{
    public class YearController : Controller
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly HttpResponseMessage httpResponseMessage;
        SqlConnrctor connrctor = new SqlConnrctor();


        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> GetYear()
        {
            List<YearsDataModel> YearsDataModel = new List<YearsDataModel>();
            try
            {
                SqlConnection con = connrctor.Connection();
                con.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM "+TableName.T_YEAR, con);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                var dataTable = new DataTable();
                dataTable.Load(reader);
                if (dataTable.Rows.Count > 0)
                {
                    var serializedMyObjects = JsonConvert.SerializeObject(dataTable);
                    YearsDataModel = (List<YearsDataModel>)JsonConvert.DeserializeObject(serializedMyObjects, typeof(List<YearsDataModel>));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connrctor.Close();
            }
            return Json(YearsDataModel);
        }
        
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetDropDownTradeData()
        {
            List<TradeDataModel> tradeModels = new List<TradeDataModel>();
            try
            {
                SqlConnection con = connrctor.Connection();
                con.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM "+TableName.T_TRADE, con);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                var dataTable = new DataTable();
                dataTable.Load(reader);
                if (dataTable.Rows.Count > 0)
                {
                    var serializedMyObjects = JsonConvert.SerializeObject(dataTable);
                    tradeModels = (List<TradeDataModel>)JsonConvert.DeserializeObject(serializedMyObjects, typeof(List<TradeDataModel>));
                }
            }
            catch (Exception ex)
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
            YearsDataModel yearsDataModel = new YearsDataModel();
            yearsDataModel.YearName = httpRequest.Form["YearName"];
            yearsDataModel.ID = httpRequest.Form["ID"];
            yearsDataModel.TradeID = httpRequest.Form["TradeID"];
            try
            {
                SqlConnection con = connrctor.Connection();
                con.Open();
                if (yearsDataModel.ID == null || yearsDataModel.ID == "")
                {
                    string query = "INSERT INTO " + TableName.T_YEAR + " (" + Constants.C_YEARNAME + ", " + Constants.C_TRADEID + ") VALUES(@YearName, @TradeId)";
                    SqlCommand cmd = new SqlCommand(query, con);

                    //Pass values to Parameters
                    cmd.Parameters.AddWithValue("@YearName", yearsDataModel.YearName);
                    cmd.Parameters.AddWithValue("@TradeId", yearsDataModel.TradeID);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    string query = "UPDATE " + TableName.T_YEAR + " SET " + Constants.C_YEARNAME + " = @YearName, " + Constants.C_TRADEID + " = @TradeId WHERE " + Constants.C_ID + " = @id";
                    SqlCommand cmd = new SqlCommand(query, con);

                    //Pass values to Parameters
                    cmd.Parameters.AddWithValue("@YearName", yearsDataModel.YearName);
                    cmd.Parameters.AddWithValue("@TradeId", yearsDataModel.TradeID);
                    cmd.Parameters.AddWithValue("@id", yearsDataModel.ID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                connrctor.Close();
            }
            return Json(yearsDataModel);
        }
    }
}
