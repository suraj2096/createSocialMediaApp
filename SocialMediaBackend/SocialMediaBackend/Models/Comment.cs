using SocialMediaBackend.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaBackend.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        
        public int postid { get; set; }
        [ForeignKey("postid")]
        public PostDetial PostDetial { get; set; }
    }
}
