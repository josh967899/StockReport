using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StockReport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace StockReport.Helper
{
    public class StockHelper
    {
        private const string URL = "https://mis.twse.com.tw/stock/api/getStockInfo.jsp?";
        private const string msgKey = "msgArray";
        private static string StockParameter(Stock stock)
        {
            return stock.Category + "_" + stock.StockCode + ".tw";
        }
        private static JObject getResponse(string urlParameters)
        {
            HttpClient client = new HttpClient();
            try
            {
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(URL + urlParameters).Result;
                if (response.IsSuccessStatusCode)
                {
                    var dataObjects = response.Content.ReadAsStringAsync().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                    JObject json = JObject.Parse(dataObjects);
                    return json;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                client.Dispose();
            }
            return null;
        }

        public static void GerCurrentPrice(Stock stock)
        {
            string urlParameters = "json=1&delay=0&ex_ch=";
            urlParameters += StockParameter(stock);
            var json = getResponse(urlParameters);
            if (json != null)
            {
                if (json.ContainsKey(msgKey))
                {
                    foreach (var val in json[msgKey])
                    {
                        stock.CurrentPrice = Convert.ToDecimal(val["z"]);
                    }
                }
            }

        }

        public static void UpdateCurrentPrice()
        {
            try
            {
                DateTime date = DateTime.Now;
                using (var db = new ApplicationDbContext())
                {
                    var beforeLog = db.LogStockUpdates.OrderByDescending(x => x.UpdateDate).FirstOrDefault();

                    if (beforeLog != null && beforeLog.UpdateDate.ToShortDateString() == date.ToShortDateString()&& beforeLog.UpdateDate.Hour>14)
                        return;

                    var stocks = db.Stocks.Where(a=>!a.IsDelete&& !string.IsNullOrEmpty(a.Category)).ToList();
                    string urlParameters = "json=1&delay=0&ex_ch=";
                    for (int i = 0; i < stocks.Count; i++)
                    {
                        if (i == 0)
                            urlParameters += StockParameter(stocks[i]);
                        else
                            urlParameters += "|" + StockParameter(stocks[i]);
                    }
                    var json = getResponse(urlParameters);
                    if (json != null)
                    {

                        if (json.ContainsKey(msgKey))
                        {

                            foreach (var val in json[msgKey])
                            {
                                string stockCode = val["c"].ToString();
                                var stock = db.Stocks.Where(s => s.StockCode == stockCode).FirstOrDefault();
                                if (stock == null) continue;
                                stock.CurrentPrice = Convert.ToDecimal(val["z"]);
                            }
                        }

                        var log = new LogStockUpdate();
                        log.UpdateDate = date;
                        log.Log = JsonConvert.SerializeObject(json);
                        db.LogStockUpdates.Add(log);
                        db.SaveChanges();

                    }
                    else
                    {
                        //Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                    }

                    //Make any other calls using HttpClient here.

                    //Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.


                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}