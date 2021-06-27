using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace myfoodapp.Hub.Models
{
    public class PieChartViewModel
    {
        public string Category { get; set; }
        public decimal Value { get; set; }
        public string Color { get; set; }
    }
}