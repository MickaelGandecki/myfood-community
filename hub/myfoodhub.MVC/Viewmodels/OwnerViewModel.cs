using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace myfoodapp.Hub.Models
{
    public class OwnerViewModel
    {
        public int Id { get; set; }
        public int pioneerCitizenNumber { get; set; }
        public string pioneerCitizenName { get; set; }
    }

}