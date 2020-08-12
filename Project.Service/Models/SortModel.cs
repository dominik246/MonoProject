using Microsoft.EntityFrameworkCore;

using Project.Service.DataAccess;
using Project.Service.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.Service.Models
{
    public class SortModel
    {
        public bool ReturnSorted { get; set; } = true;

        public string SortBy { get; set; } = "Name";
    }
}
