using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaBackend.Models;
using SocialMediaBackend.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SocialMediaBackend.Controllers
{
    [Route("api/comment")]
    [ApiController]
    //[Authorize]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        public CommentController(ICommentRepository commentrepository)
        {
            _commentRepository = commentrepository;
        }
        [HttpGet("{id:int}")]
        public IActionResult getAllComment(int id)
        {
            if (id == 0) return BadRequest();
            var data = _commentRepository.getAllComment(u=>u.postid == id);
            return Ok(data);
        }
        [HttpPost]
        public IActionResult createComment([FromBody] Comment comment)
        {
            if (comment == null) return BadRequest();
            /*var claimidentity = (ClaimsIdentity)User.Identity;
            var claim = claimidentity.FindFirst(ClaimTypes.Name);
            var value = claim.Value;*/
            /*if (value == null) return NotFound();*/
            if (!_commentRepository.createComment(comment))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }
    }
}
