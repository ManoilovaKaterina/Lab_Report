using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class Serv
    {
        public int ServId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int BasePrice { get; set; }
    }
}