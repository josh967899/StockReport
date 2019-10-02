using StockReport.Models;
using StockReport.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockReport.Helper
{
    public class ProfitHelper
    {

        public static List<ProfitStatus> getProfits(List<Trade> trades)
        {
            //List<ProfitStatus> profits = new Dictionary<int, ProfitStatus>();
            try
            {        
            var result = trades.GroupBy(x => x.StockId).Select(g => new ProfitStatus
            {
                stock = g.First().Stock,
                hode = g.Where(a => a.TradeType == "B").Sum(p => p.Quantity) - g.Where(a => a.TradeType == "S").Sum(p => p.Quantity),
                investAmount = g.Where(a => a.TradeType == "B").Sum(p => p.TotalAmount),
                income = g.Where(a => a.TradeType == "S").Sum(p => p.TotalAmount),
                

            }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }
        }

        public static void HandleProfit(List<ProfitStatus> profits, List<Dividend> dividends)
        {
            foreach (var p in profits)
            {
                var stockDividend = (int)dividends.Where(x => x.StockId == p.stock.Id && x.DividendType == "Stock").Sum(a=>a.Among);
                var cashkDividend = (int)dividends.Where(x => x.StockId == p.stock.Id && x.DividendType == "Cash").Sum(a => a.Among);
                p.cashDividend = cashkDividend;
                p.hode += stockDividend;
                p.hodePrice = p.hode * p.stock.CurrentPrice;
                p.balance = p.hodePrice + p.income+ p.cashDividend - p.investAmount;
                p.profit = p.balance / p.investAmount * 100;

            }
        }

    }
}