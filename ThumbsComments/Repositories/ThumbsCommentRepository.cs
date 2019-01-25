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
    /// <summary>
    /// Thumbs Comment repository, handles all database queries
    /// </summary>
    public class ThumbsCommentRepository : IThumbsCommentsRepository
    {
        private readonly Context _context;

        /// <summary>
        /// Constructor for Product groups Repository
        /// </summary>
        /// <param name="context">Context dependency injected</param>    
        public ThumbsCommentRepository(Context context)
        {
            _context = context;
        }

        /// <summary>
        /// Get individual thumb comment
        /// </summary>
        /// <param name="where">where</param>
        /// <returns>Task Comment</returns>
        public async Task<Comment> Get(Expression<Func<Comment, bool>> where)
        {
            return await _context.Comments
                                 .Where(where)
                                 .FirstOrDefaultAsync();
                                        
        }

        /// <summary>
        /// Returns many comments
        /// </summary>
        /// <returns>Task IEnumerable Comment</returns>
        public async Task<IEnumerable<Comment>> GetMany()
        {
            return await _context.Comments
                                 .ToListAsync();
        }

        /// <summary>
        /// Get many comments
        /// </summary>
        /// <param name="where">where</param>
        /// <returns>Task IEnumerable Comment</returns>
        public async Task<IEnumerable<Comment>> GetMany(Expression<Func<Comment, bool>> where)
        {
            return await _context.Comments
                                 .Where(where)
                                 .ToListAsync();
        }

        /// <summary>
        /// Create comment
        /// </summary>
        /// <param name="comment">comment</param>
        /// <returns>Task</returns>
        public async Task Post(Comment comment)
        {
            _context.Add(comment);
            await _context.SaveChangesAsync();
            return;
        }

        /// <summary>
        /// Update comment
        /// </summary>
        /// <param name="comment">comment</param>
        /// <returns>Task</returns>
        public async Task Put(Comment comment)
        {
            _context.Update(comment);
            await _context.SaveChangesAsync();
            return;
        }

        /// <summary>
        /// Delete comment
        /// </summary>
        /// <param name="comment">comment</param>
        /// <returns>Task</returns>
        public async Task Delete(Comment comment)
        {
            _context.Comments
                    .Remove(comment);

            await _context.SaveChangesAsync();

            return;
        }

        /// <summary>
        /// Does comment exist
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Task bool</returns>
        public async Task<bool> Exists(Guid id)
        {
            return await _context.Comments
                                 .AnyAsync();
        }
    }
}
