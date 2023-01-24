using SocialMediaBackend.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaBackend.Models
{
    public class PostDetial
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public DateTime Publish { get; set; }
        public string Userid { get; set; }
        [ForeignKey("Userid")]
        public ApplicationUser applicationuser { get; set; }
        [NotMapped]
        public Boolean Like { get; set; }
        [NotMapped]
        public Boolean Follow { get; set; }
    }
}
