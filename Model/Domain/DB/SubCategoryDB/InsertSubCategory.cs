using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Domain.DB.SubCategoryDB
{
    public class InsertSubCategory
    {
        public int CategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public int AuditedUserId { get; set; }
    }
}
