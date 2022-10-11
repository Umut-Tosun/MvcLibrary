using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcLibrary.Models.Entity;

namespace MvcLibrary.Models.Class
{
    public class Class1
    {
        public IEnumerable<Tbl_Book> books { get; set; }
        public IEnumerable<Tbl_About> about { get; set; }
    }
}