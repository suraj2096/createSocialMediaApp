using SocialMediaBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SocialMediaBackend.Repository.IRepository
{
   public interface ILikeUnlikeRepository
    {
        public IEnumerable<LikeUnlike> getAll(Expression<Func<LikeUnlike, bool>> filter = null,string includeMultipleTable = null);
        public bool createLikeUnlike(LikeUnlike likeunlike);
        public bool UpdateLikeUnlike(LikeUnlike likeunlike);
    }
}
