using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RevenueCollection.Models
{
    public class ChildSubMenu
    {
        public int SubParentId { get; set; }
        public string ChildSubMenuName { get; set; }
        public string ChildSubMenuUrl { get; set; }
    }
}