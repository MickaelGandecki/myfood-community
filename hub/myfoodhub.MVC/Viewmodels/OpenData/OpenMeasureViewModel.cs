using myfoodapp.Hub.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace myfoodapp.Hub.Models.OpenData
{
    public class OpenMeasureViewModel
    {
        public Int64 Id { get; set; }
        public DateTime captureDate { get; set; }
        public decimal value { get; set; }
        public string sensor { get; set; }

    }
}