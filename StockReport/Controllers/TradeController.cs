using StockReport.Helper;
using StockReport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockReport.Controllers
{
    public class TradeController : Controller
    {
        // GET: Trade
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "User")]
        public ActionResult TradeCreate(int id=0)
        {
            using (var db = new ApplicationDbContext())
            {
                var trade = db.Trades.Where(a => a.Id == id).FirstOrDefault();
                if (trade == null) {
                    trade = new Trade();
                    trade.TradeDate = DateTime.Now;
                }
                var stocks = db.Stocks.ToList();
                ViewBag.Stocks = stocks;
                ViewBag.Trades = new List<SelectListItem>()
    {
        new SelectListItem {Text="Buy", Value="B" },
        new SelectListItem {Text="Sale", Value="S"},

    };
                return View(trade);
            }
        }
        public ActionResult TradeEidt(int id)
        {
            return RedirectToAction("TradeCreate", "Trade", new { @id = id });
        }
        [HttpPost]
        [Authorize(Roles = "User")]
        public ActionResult Create(Trade trade)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var t = db.Trades.Where(a => a.Id == trade.Id).FirstOrDefault();
                    var rate = db.Rates.Where(a => a.RateCode == 1).FirstOrDefault();
                    var feeRate = rate != null ?rate.Value:0.001425M;
                    rate = db.Rates.Where(a => a.RateCode == 2).FirstOrDefault();
                    var taxRate = rate != null ? rate.Value : 0.003M;

                    TradeHelper.getTotalAmount(trade, feeRate, taxRate);
                    trade.UserCode= User.Identity.Name;
                    if (t == null) 
                    db.Trades.Add(trade);
                    else
                    {
                        t.Price = trade.Price;
                        t.Quantity = trade.Quantity;
                        t.Fee = trade.Fee;
                        t.Stock = trade.Stock;
                        t.StockId = trade.StockId;
                        t.Tax = trade.Tax;
                        t.TradeDate = trade.TradeDate;
                        t.TradeType = trade.TradeType;
                        t.UserCode = trade.UserCode;
                        t.TotalAmount = trade.TotalAmount;
                    }
                    db.SaveChanges();

                    return Json("done");
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }

        }
        public ActionResult TradeList()
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var trades = db.Trades.Where(a => !a.IsDelete).OrderByDescending(a=>a.TradeDate).ToList();
                    var stocks = db.Stocks.Where(a => !a.IsDelete).ToList();
                    ViewBag.Stocks = stocks;
                    return View(trades);


                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("HandleError", "Error", new { @msg = ex.Message });
            }
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            try { 
            using (var db = new  ApplicationDbContext())
            {
                    var data = db.Trades.Where(a => a.Id == id).FirstOrDefault();
                    if(data != null){
                        data.IsDelete = true;
                        db.SaveChanges();
                        return Json("done");
                    }
                    return Json("fail: cannot find id");
                }
            }catch(Exception ex)
            {
                return Json("fail: "+ex.Message);
            }
        }
    }
}