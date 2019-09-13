using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Model.DTO.DB.UserDB
{
    public class UserDTO
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
    }
}
