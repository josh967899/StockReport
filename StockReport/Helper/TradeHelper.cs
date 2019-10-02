using StockReport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockReport.Helper
{
    public class TradeHelper 
    {
        public static Trade getTotalAmount(Trade trade,decimal feeRate,decimal taxRate)
        {
            
            int price = Convert.ToInt32(trade.Quantity * trade.Price);
            var fee = price * feeRate;
            trade.Tax = trade.TradeType == "B" ? 0: Convert.ToInt16(Math.Floor(price * taxRate));
            trade.Fee = fee < 20 ? 20 : Convert.ToInt16(Math.Floor(fee));
            trade.TotalAmount = trade.TradeType == "B" ? price + trade.Fee : price - trade.Fee - trade.Tax;

            return trade;
            
        }
    }
}