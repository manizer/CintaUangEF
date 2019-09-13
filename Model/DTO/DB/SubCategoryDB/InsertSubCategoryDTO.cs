using System;
using System.Collections.Generic;
using System.Text;

namespace Model.DTO.DB.SubCategoryDB
{
    public class InsertSubCategoryDTO
    {
        public int CategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public int AuditedUserId { get; set; }
    }
}
