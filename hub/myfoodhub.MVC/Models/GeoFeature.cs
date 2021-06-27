using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myfoodapp.Hub.Models
{
    public class GeoFeature
    {
        public string type { get; set; }
        public GeoProperties properties { get; set; }
        public Geometry geometry { get; set; }
    }
}