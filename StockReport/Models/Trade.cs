using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StockReport.Models
{
    public class Trade
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("交易日期")]
        public DateTime TradeDate { get; set; }
        public int StockId { get; set; }
        [DisplayName("交易數量")]
        public int Quantity { get; set; }
        [DisplayName("買/賣")]
        public string TradeType { get; set; }
        [DisplayName("交易價")]
        public decimal Price { get; set; }
        [DisplayName("手續費")]
        public int Fee { get; set; }
        [DisplayName("稅金")]
        public int Tax { get; set; }
        [DisplayName("總金額")]
        public int TotalAmount { get; set; }
        public string UserCode { get; set; }
        public bool IsDelete { get; set; }

        [ForeignKey("StockId")]
        public Stock Stock { get; set; }
    }
}