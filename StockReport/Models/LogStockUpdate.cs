using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StockReport.Models
{
    public class LogStockUpdate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Log { get; set; }
    }
}