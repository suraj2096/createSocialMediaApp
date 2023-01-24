using SocialMediaBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SocialMediaBackend.Repository.IRepository
{
    public interface IPostRepository
    {
        public IEnumerable<PostDetial> getAllPost(Expression<Func<PostDetial,bool>> filter=null,Func<IQueryable<PostDetial>,IOrderedQueryable<PostDetial>> orderBy=null,string includeMultipleTable=null);
        public bool createPost(PostDetial postdetail);
        public bool deletePost(PostDetial postDetial);
    }
}
