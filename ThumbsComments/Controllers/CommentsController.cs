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
        private readonly IThumbsCommentsRepository _thubsCommentsRepository;

        public CommentsController(ILogger<CommentsController> logger, 
                                  IThumbsCommentsRepository thubsCommentsRepository)
        {
            _thubsCommentsRepository = thubsCommentsRepository;
            _logger = logger;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<IEnumerable<Comment>> GetComments()
        {
            return await _thubsCommentsRepository.GetMany();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _thubsCommentsRepository.Get(t => t.Id == id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        [HttpGet(), Route("GetCommentByLineOfBusiness")]
        public async Task<IActionResult> GetCommentByLineOfBusiness(string lineOfBusiness)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _thubsCommentsRepository.Get(t => t.LineOfBusiness == lineOfBusiness);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
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
                await _thubsCommentsRepository.Put(comment);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await _thubsCommentsRepository.Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogCritical(500, ex.Message, ex);
                    return StatusCode(500, ex.Message);
                }
            }

            return NoContent();
        }

        // POST: api/Comments
        [HttpPost]
        public async Task<IActionResult> PostComment([FromBody] Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            comment.Id = Guid.NewGuid();

            await _thubsCommentsRepository.Post(comment);

       
            return CreatedAtAction("GetComment", new { id = comment.Id }, comment);
         
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _thubsCommentsRepository.Get(t => t.Id == id);

            if (comment == null)
            {
                return NotFound();
            }

            await _thubsCommentsRepository.Delete(comment);

            return Ok(comment);
        }
    }
}