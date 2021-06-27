using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myfoodapp.Hub.Models
{
    public class Map
    {
        // Map properties 
        public string Name { get; set; }
        public double CenterLatitude { get; set; }
        public double CenterLongitude { get; set; }
        public int Zoom { get; set; }

        // Tile layer properties
        public string TileUrlTemplate { get; set; }
        public string[] TileSubdomains { get; set; }
        public string TileAttribution { get; set; }

        // Markers collection
        public IEnumerable<Marker> Markers { get; set; }
    }

    public class ClusterMap
    {
        // Map properties 
        public string Name { get; set; }
        public double CenterLatitude { get; set; }
        public double CenterLongitude { get; set; }
        public int Zoom { get; set; }

        // Tile layer properties
        public string TileUrlTemplate { get; set; }
        public string[] TileSubdomains { get; set; }
        public string TileAttribution { get; set; }

        // Markers collection
        public GeoClusterMapData ClusterData { get; set; }
    }

    public class Marker
    {
        public Marker(double latitude, double longitude, string sname)
        {
            latlng = new double[] { latitude, longitude };
            name = sname;
        }

        public double[] latlng { get; set; }
        public string name { get; set; }

        public string shape { get; set; }
    }
}