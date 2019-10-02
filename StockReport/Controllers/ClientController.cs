using StockReport.Helper;
using StockReport.Models;
using StockReport.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockReport.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "User")]
        public ActionResult ClientDetail()
        {
            var username = User.Identity.Name;
            var clientPage = new ClientPages();
            try
            {
                StockHelper.UpdateCurrentPrice();
                using (var db = new ApplicationDbContext())
                {

                    clientPage.UserName = db.User.Where(a => a.UserEmail == username).FirstOrDefault();
                    var data = db.Trades.Where(a=>!a.IsDelete).ToList().Join(db.Stocks, a => a.StockId, c => c.Id, (a, c) => new Trade {
                        StockId=a.StockId,
                        Quantity=a.Quantity,
                        TotalAmount=a.TotalAmount,
                        TradeType=a.TradeType,
                        Stock =c
                    }).ToList();
                    clientPage.profits = ProfitHelper.getProfits(data);
                    var dividend = db.Dividends.Where(a => !a.IsDelete).ToList();
                    ProfitHelper.HandleProfit(clientPage.profits, dividend);
                    clientPage.SumHode = clientPage.profits.Sum(a => a.hode);
                    clientPage.SumInvest = clientPage.profits.Where(a=>a.hode!=0).Sum(a => a.investAmount);
                    clientPage.done = clientPage.profits.Where(a => a.hode == 0).Sum(a => a.balance);
                    clientPage.undone = clientPage.profits.Where(a => a.hode != 0).Sum(a => a.balance);
                    clientPage.SumhodePrice = clientPage.profits.Where(a => a.hode != 0).Sum(a => a.hodePrice);

                    var stocks = db.Stocks.Where(a => !a.IsDelete).ToList();
                    ViewBag.Stocks = stocks;

                    return View(clientPage);
                }
            }catch(Exception ex)
            {
                return RedirectToAction("HandleError", "Error", new { @msg = ex.Message });
            }       
        }

    }
}