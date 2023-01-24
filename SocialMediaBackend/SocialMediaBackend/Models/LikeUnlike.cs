using SocialMediaBackend.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaBackend.Models
{
    public class LikeUnlike
    {
        public int Id { get; set; }
        public int postId { get; set; }
        [ForeignKey("postId")]
        public PostDetial post { get; set; }
        public string userId { get; set; }
        [ForeignKey("userId")]
        public ApplicationUser user { get; set; }
        public Boolean Like { get; set; }

    }
}
