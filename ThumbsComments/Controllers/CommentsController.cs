using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThumbsComments.Interfaces;
using ThumbsComments.Models;

namespace ThumbsComments.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ILogger<CommentsController> _logger;
        private readonly IThumbsCommentsRepository _thumbsCommentsRepository;

        public CommentsController(ILogger<CommentsController> logger, 
                                  IThumbsCommentsRepository thubsCommentsRepository)
        {
            _thumbsCommentsRepository = thubsCommentsRepository;
            _logger = logger;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<IEnumerable<Comment>> GetComments()
        {
            try
            {
                return await _thumbsCommentsRepository.GetMany();
            }
            catch(Exception ex)
            {
                _logger.LogCritical(500, ex.Message, ex);

                return new List<Comment>() ;
            }           
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var comment = await _thumbsCommentsRepository.Get(t => t.Id == id);

                if (comment == null)
                {
                    return NotFound();
                }

                return Ok(comment);
            }
            catch(Exception ex)
            {
                _logger.LogCritical(500, ex.Message, ex);

                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lineOfBusiness"></param>
        /// <returns>Task IActionResult</returns>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="404">Not Found</response>
        /// <response code="500">Internal Server Error</response>
        [HttpGet(), Route("GetCommentByLineOfBusiness")]
        public async Task<IActionResult> GetCommentByLineOfBusiness(string lineOfBusiness)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var comment = await _thumbsCommentsRepository.Get(t => t.LineOfBusiness == lineOfBusiness);

                if (comment == null)
                {
                    return NotFound();
                }

                return Ok(comment);
            }
            catch(Exception ex)
            {
                _logger.LogCritical(500, ex.Message, ex);

                return StatusCode(500, ex.Message);
            }            
        }


        // PUT: api/Comments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment([FromRoute] Guid id, [FromBody] Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comment.Id)
            {
                return BadRequest();
            }

            try
            {
                await _thumbsCommentsRepository.Put(comment);

                return NoContent();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await _thumbsCommentsRepository.Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogCritical(500, ex.Message, ex);

                    return StatusCode(500, ex.Message);
                }
            }            
        }

        // POST: api/Comments
        [HttpPost]
        public async Task<IActionResult> PostComment([FromBody] Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                comment.Id = Guid.NewGuid();

                await _thumbsCommentsRepository.Post(comment);

                return CreatedAtAction("GetComment", new { id = comment.Id }, comment);
            }
            catch(Exception ex)
            {
                _logger.LogCritical(500, ex.Message, ex);

                return StatusCode(500, ex.Message);
            }           
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var comment = await _thumbsCommentsRepository.Get(t => t.Id == id);

                if (comment == null)
                {
                    return NotFound();
                }

                await _thumbsCommentsRepository.Delete(comment);

                return Ok(comment);
            }
            catch(Exception ex)
            {
                _logger.LogCritical(500, ex.Message, ex);

                return StatusCode(500, ex.Message);
            }
        }
    }
}