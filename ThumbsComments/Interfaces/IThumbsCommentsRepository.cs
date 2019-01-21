using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ThumbsComments.Models;

namespace ThumbsComments.Interfaces
{
    /// <summary>
    /// Thumbs Comments Repository
    /// </summary>
    interface IThumbsCommentsRepository
    {
        /// <summary>
        /// Required Get function
        /// </summary>
        /// <param name="where">Where function</param>
        /// <returns>Task of Comment</returns>
        Task<Comment> Get(Expression<Func<Comment, bool>> where);

        /// <summary>
        /// Required GetMany function
        /// </summary>
        /// <returns>Task IEnumerable Comments</returns>
        Task<IEnumerable<Comment>> GetMany();

        /// <summary>
        /// Required GetMany function
        /// </summary>
        /// <param name="where">Where function</param>
        /// <returns>Task IEnumerable Comment</returns>
        Task<IEnumerable<Comment>> GetMany(Expression<Func<Comment, bool>> where);  

        /// <summary>
        /// Requred Post function
        /// </summary>
        /// <param name="comment">Comment</param>
        /// <returns>Task</returns>
        Task Post(Comment comment);

        /// <summary>
        /// Required Put function
        /// </summary>
        /// <param name="comment">Comment</param>
        /// <returns>Task</returns>
        Task Put(Comment comment);

        /// <summary>
        /// Required Delete function
        /// </summary>
        /// <param name="comment">Product Group</param>
        /// <returns>Task</returns>
        Task Delete(Comment comment);

        /// <summary>
        /// Required Exists function
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Task bool</returns>
        Task<bool> Exists(Guid id);
    }
}
