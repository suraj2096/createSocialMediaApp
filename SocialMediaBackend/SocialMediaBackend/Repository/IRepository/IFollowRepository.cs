using SocialMediaBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SocialMediaBackend.Repository.IRepository
{
    public interface IFollowRepository
    {
        public IEnumerable<Followers> getFollower(Expression<Func<Followers, bool>> filter = null, string includeMultipleTable = null);
        public bool createFollower(Followers follow);
        public bool RemoveFollower(Followers follow);
    }
}
