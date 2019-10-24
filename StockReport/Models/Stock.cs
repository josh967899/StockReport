using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StockReport.Models
{
    public class Stock
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("股票代碼")]
        public string StockCode { get; set; }
        [DisplayName("股票名稱")]
        public string  StockName { get; set; }
        [DisplayName("上市/上櫃")]
        public string Category { get; set; }
        [DisplayName("現價")]
        public decimal CurrentPrice { get; set; }
        public bool IsDelete { get; set; }
        //public string owner { get; set; }

    }
}