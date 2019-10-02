using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StockReport.Models
{
    public class Dividend
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("交易年分")]
        public int Year { get; set; }
        public int StockId { get; set; }
        [DisplayName("股利類別")]
        public string DividendType { get; set; }
        [DisplayName("股利數量")]
        public decimal Among { get; set; }
        public bool IsDelete { get; set; }

        [ForeignKey("StockId")]
        public Stock Stock { get; set; }
    }
}