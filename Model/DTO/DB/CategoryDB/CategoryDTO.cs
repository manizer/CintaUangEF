using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Model.DTO.DB.CategoryDB
{
	[Table("mscategory")]
    public class CategoryDTO : BaseModel
    {
        [Key]
		[Column("id")]
        public int Id { get; set; }
		[Column("name")]
        public string Name { get; set; }
    }
}
