using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Model.DTO.DB.UserDB
{
	[Table("msuser")]
    public class UserDTO
    {
        [Key]
		[Column("userid")]
        public int UserId { get; set; }
		[Column("username")]
        public string UserName { get; set; }
		[Column("email")]
        public string UserEmail { get; set; }
		[Column("password")]
        public string UserPassword { get; set; }
    }
}
