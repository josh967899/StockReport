using StockReport.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace StockReport.ViewModels
{
    public class ClientPages
    {
        public User UserName { get; set; }
        public List<ProfitStatus> profits { get; set; }
        public int SumHode { get; set; }
        public decimal undone { get; set; }
        public decimal done { get; set; }
        public decimal SumInvest { get; set; }
        public decimal SumhodePrice { get; set; }
    }
    public class ProfitStatus
    {
        public Stock stock { get; set; }
        [DisplayName("持有數量")]
        public int hode { get; set; }
        [DisplayName("現金股利")]
        public int cashDividend { get; set; }
        [DisplayName("投資金額")]
        public decimal investAmount { get; set; }
        [DisplayName("收入")]
        public decimal income { get; set; }
        [DisplayName("持有金額")]
        public decimal hodePrice { get; set; }
        [DisplayName("損益(%)")]
        public decimal profit { get; set; }
        [DisplayName("損益")]
        public decimal balance { get; set; }
    }
}

 
