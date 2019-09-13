using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CintaUang.ViewModels.AuthViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public string UserPassword { get; set; }
    }
}
