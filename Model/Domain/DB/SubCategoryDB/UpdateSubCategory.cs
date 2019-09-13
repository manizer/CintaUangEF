using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Domain.DB.SubCategoryDB
{
    public class UpdateSubCategory
    {
        public int SubCategoryId { get; set; }
        public int CategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public int AuditedUserId { get; set; }
    }
}
