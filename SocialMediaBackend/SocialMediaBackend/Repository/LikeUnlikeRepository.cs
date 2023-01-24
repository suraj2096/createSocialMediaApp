using SocialMediaBackend.Identity;
using SocialMediaBackend.Models;
using SocialMediaBackend.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SocialMediaBackend.Repository
{
    public class LikeUnlikeRepository : ILikeUnlikeRepository
    {
        private readonly ApplicationDbContext _context;
        public LikeUnlikeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool createLikeUnlike(LikeUnlike likeunlike)
        {
            _context.Like.Add(likeunlike);
            return save();
        }

        public IEnumerable<LikeUnlike> getAll(Expression<Func<LikeUnlike, bool>> filter = null, string includeMultipleTable = null)
        {
            return _context.Like.Where(filter);
        }

        public bool UpdateLikeUnlike(LikeUnlike likeunlike)
        {
            _context.Like.Update(likeunlike);
            return save();
        }
        public bool save()
        {
            return _context.SaveChanges() == 1 ? true : false;
        }
    }
}
