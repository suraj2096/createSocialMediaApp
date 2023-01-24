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
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool createComment(Comment comment)
        {
            _context.Comment.Add(comment);
            return save();
        }

        public IEnumerable<Comment> getAllComment(Expression<Func<Comment,bool>> filter=null)
        {
           return _context.Comment.Where(filter).ToList();
        }

        public bool save()
        {
            return _context.SaveChanges() == 1 ? true : false;
        }
    }
}
