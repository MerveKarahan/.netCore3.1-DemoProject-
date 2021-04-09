using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
   public class ChangePasswordDto
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
