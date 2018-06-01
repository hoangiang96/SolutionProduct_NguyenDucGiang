using System;

namespace Product.Model.Abstract
{
    public interface IAuditable
    {
        DateTime? CreatedDate { set; get; }

        string CreatedBy { set; get; }

        DateTime? UpdatedDate { set; get; } 

        string UpdatedBy { set; get; }

        string MetaKeyword { get; set; }

        string MetaDescription { get; set; }

        bool Status { set; get; }
    }
}