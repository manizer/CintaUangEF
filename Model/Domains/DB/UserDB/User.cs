using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Domain.DB.UserDB
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
    }
}
