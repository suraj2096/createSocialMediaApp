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
    public class FollowRepostory : IFollowRepository
    {
        private readonly ApplicationDbContext _context;
        public FollowRepostory(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool createFollower(Followers follow)
        {
            _context.Follow.Add(follow);
            return save();
        }

        public IEnumerable<Followers> getFollower(Expression<Func<Followers, bool>> filter = null, string includeMultipleTable = null)
        {
            return _context.Follow.Where(filter);
        }

        public bool RemoveFollower(Followers follow)
        {
            _context.Follow.Remove(follow);
            return save();
        }
        public bool save()
        {
            return _context.SaveChanges() == 1 ? true : false;
        }
    }
}
