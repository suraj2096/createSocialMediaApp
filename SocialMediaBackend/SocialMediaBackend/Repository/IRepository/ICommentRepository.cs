using SocialMediaBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SocialMediaBackend.Repository.IRepository
{
    public interface ICommentRepository
    {
        public IEnumerable<Comment> getAllComment(Expression<Func<Comment, bool>> filter=null);
        public bool createComment(Comment comment);
        public bool save();
    }
}
