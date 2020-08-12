using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Service.Models
{
    public class FilterModel
    {
        public bool ReturnFiltered
        {
            get { return !string.IsNullOrEmpty(FilterString); }
        }

        public string FilterString { get; set; }
    }
}
