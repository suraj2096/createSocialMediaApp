using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaBackend.Models.View_Model
{
    public class SignUpViewModel
    {
        public string userName { get; set; }
        public string userEmail { get; set; }
        public string Password { get; set; }
        public string? provider { get; set; }
        public string? providerKey { get; set; }
    }
}
