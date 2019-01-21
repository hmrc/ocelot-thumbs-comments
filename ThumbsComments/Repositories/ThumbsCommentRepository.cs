using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ThumbsComments.Interfaces;
using ThumbsComments.Models;

namespace ThumbsComments.Repositories
{
    public class ThumbsCommentRepository : IThumbsCommentsRepository
    {
        private readonly Context _context;

        public ThumbsCommentRepository(Context context)
        {
            _context = context;
        }

        public async Task<Comment> Get(Expression<Func<Comment, bool>> where)
        {
            return await _context.Comments
                                 .Where(where)
                                 .FirstOrDefaultAsync();
                                        
        }

        public async Task<IEnumerable<Comment>> GetMany()
        {
            return await _context.Comments
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetMany(Expression<Func<Comment, bool>> where)
        {
            return await _context.Comments
                                 .Where(where)
                                 .ToListAsync();
        }

        public async Task Post(Comment comment)
        {
            _context.Add(comment);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task Put(Comment comment)
        {
            _context.Update(comment);
            await _context.SaveChangesAsync();
            return;
        }

        public async Task Delete(Comment comment)
        {
            _context.Comments
                    .Remove(comment);

            await _context.SaveChangesAsync();

            return;
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Comments
                                 .AnyAsync();
        }
    }
}
