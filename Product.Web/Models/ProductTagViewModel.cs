using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Product.Web.Models
{
    public class ProductTagViewModel
    {
        public int ProductID { get; set; }

        public string TagID { get; set; }

        public virtual itemViewModel Product { get; set; }

        public virtual TagViewModel Tag { get; set; }
    }
}