using SocialMediaBackend.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaBackend.Models
{
    public class Followers
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public string followedId { get; set; }
        [ForeignKey("followedId")]
        public ApplicationUser applicationuser { get; set; }

    }
}
