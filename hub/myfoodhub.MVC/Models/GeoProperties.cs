using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myfoodapp.Hub.Models
{
    public class GeoProperties
    {
        public long scalerank { get; set; }
        public string name { get; set; }
        public string comment { get; set; }
        public string name_alt { get; set; }
        public double lat_y { get; set; }
        public double long_x { get; set; }
        public string region { get; set; }
        public string subregion { get; set; }
        public string featureclass { get; set; }
    }
}