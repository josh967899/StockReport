using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace StockReport.Models
{
    public class Rate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int RateCode { get; set; }
        public string Name { get; set; }       
        public decimal Value { get; set; }
        public string  Remark { get; set; }
    }
}