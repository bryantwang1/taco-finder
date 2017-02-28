using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TacoFinder.Models
{
    public class TacoSpot
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
    }
}
