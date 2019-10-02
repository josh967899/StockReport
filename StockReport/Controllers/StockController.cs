using Newtonsoft.Json.Linq;
using StockReport.Helper;
using StockReport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace StockReport.Controllers
{
    public class StockController : Controller
    {
        // GET: Stock
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult StockCreate(int id=0)
        {
            using (var db = new ApplicationDbContext())
            {
                var data = db.Stocks.Where(a => a.Id == id).FirstOrDefault();
                if (data == null) data = new Stock();
            ViewBag.Category = new List<SelectListItem>()
    {
        new SelectListItem {Text="上市", Value="tse" },
        new SelectListItem {Text="上櫃", Value="otc"},

    };
                return View(data);
            }
        }
        public ActionResult StockEdit(int id)
        {
             return RedirectToAction("StockCreate", "Stock", new { @id = id });
        }
        [HttpPost]
        public ActionResult Create(Stock stock)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var data = db.Stocks.Where(a => a.Id == stock.Id).FirstOrDefault();
                    StockHelper.GerCurrentPrice(stock);
                    if(stock.Id==0|| data==null)
                        db.Stocks.Add(stock);
                    else
                    {
                        data.StockCode = stock.StockCode;
                        data.StockName = stock.StockName;
                        data.Category = stock.Category;
                        data.CurrentPrice = stock.CurrentPrice;
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

        public ActionResult StockList()
        {
            try
            {
                StockHelper.UpdateCurrentPrice();
                using (var db = new ApplicationDbContext())
                {
                    var list = db.Stocks.Where(a => !a.IsDelete).ToList();
                   
                    return View(list);
                }
            }
            catch (Exception e)
            {
                return RedirectToAction("index", "Home");
            }
        }
        public ActionResult DividendList()
        {
            using (var db = new ApplicationDbContext())
            {
                var list = db.Dividends.Where(a=>!a.IsDelete).ToList();
                var stocks = db.Stocks.Where(a => !a.IsDelete).ToList();
                ViewBag.Stocks = stocks;
                return View(list);
            }
        }

        public ActionResult DividendCreate(int id=0)
        {
            using (var db = new ApplicationDbContext())
            {
                var d = db.Dividends.Where(a => a.Id == id).FirstOrDefault();
                if (d == null) d = new Dividend();
                ViewBag.Stocks = db.Stocks.ToList();
                ViewBag.DividendType = new List<SelectListItem>()
             {
        new SelectListItem {Text="現金股利", Value="Cash" },
        new SelectListItem {Text="股票股利", Value="Stock"},

             };
                return View(d);
            }
        }
        [HttpPost]
        public ActionResult CreateDividend(Dividend dividend)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                   var d = db.Dividends.Where(a => a.Id == dividend.Id).FirstOrDefault();
                    if (d == null)
                        db.Dividends.Add(dividend);
                    else { 
                        d.StockId = dividend.StockId;
                        d.Year = dividend.Year;
                        d.DividendType = dividend.DividendType;
                        d.Among = dividend.Among;
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

        public ActionResult DividendEdit(int id)
        {
            return RedirectToAction("DividendCreate", "Stock", new { @id = id });
        }

        [HttpPost]
        public ActionResult DeleteStock(int id)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var data = db.Stocks.Where(a => a.Id == id).FirstOrDefault();
                    if (data != null)
                    {
                        data.IsDelete = true;
                        db.SaveChanges();
                        return Json("done");
                    }
                    return Json("fail: cannot find id");
                }
            }
            catch (Exception ex)
            {
                return Json("fail: " + ex.Message);
            }
        }

        [HttpPost]
        public ActionResult DeleteDividend(int id)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var data = db.Dividends.Where(a => a.Id == id).FirstOrDefault();
                    if (data != null)
                    {
                        data.IsDelete = true;
                        db.SaveChanges();
                        return Json("done");
                    }
                    return Json("fail: cannot find id");
                }
            }
            catch (Exception ex)
            {
                return Json("fail: " + ex.Message);
            }
        }

    }
}