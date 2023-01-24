using Microsoft.EntityFrameworkCore;
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
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<PostDetial> dbset;
        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
            dbset = _context.Set<PostDetial>();
        }
        public bool createPost(PostDetial postdetail)
        {
            dbset.Add(postdetail);
            return save();
        }

        public bool deletePost(PostDetial postDetial)
        {
            dbset.Remove(postDetial);
            return save();
        }

        public IEnumerable<PostDetial> getAllPost(Expression<Func<PostDetial, bool>> filter = null,Func<IQueryable<PostDetial>, IOrderedQueryable<PostDetial>> orderBy = null,string includeMultipleTable=null)
        {
            IQueryable<PostDetial> query = dbset;
            if (filter != null)
            {
                query = query.Where<PostDetial>(filter);
            }
            if (includeMultipleTable != null)
            {
                query.Include(includeMultipleTable).ToList();
            }
            if (orderBy != null)
            {
                return orderBy(query).ThenByDescending(m => m.Id).ToList();
            }
            return query.ToList();
        }
        public bool save()
        {
            return _context.SaveChanges() == 1 ? true : false;
        }
    }
}
