using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SocialMediaBackend.Models;
using SocialMediaBackend.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SocialMediaBackend.Controllers
{
    [Route("api/post")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postrepository;
        private readonly ILikeUnlikeRepository _likeUnlikeRepository;
        private readonly IFollowRepository _followrespository;
        private readonly IWebHostEnvironment _webhostEnvironment;
        public PostController(IPostRepository postrepository,IWebHostEnvironment webhostEnvironment,ILikeUnlikeRepository likeunlikerepository,IFollowRepository followrepository)
        {
            _postrepository = postrepository;
            _webhostEnvironment = webhostEnvironment;
            _likeUnlikeRepository = likeunlikerepository;
            _followrespository = followrepository;
        }
        [HttpGet]
        public IActionResult getpost(int? id)
        {
            var claimidentity = (ClaimsIdentity)User.Identity;
            var claim = claimidentity.FindFirst(ClaimTypes.Name);
            if(id == null)
            {
            var resultData = _postrepository.getAllPost(orderBy: m => m.OrderByDescending(m => m.Id),includeMultipleTable:"applicationuser");
                foreach(var postcheck in resultData)
                {
                    var postcheckchange = _likeUnlikeRepository.getAll(m => m.postId == postcheck.Id 
                    && m.userId == postcheck.Userid && m.Like == true).FirstOrDefault();
                    if(postcheckchange != null)
                    {
                        postcheck.Like = true;
                    }
                    else
                    {
                        postcheck.Like = false;
                    }
                    var checkfollow = _followrespository.getFollower(m => m.UserID == claim.Value && m.followedId == postcheck.Userid).FirstOrDefault();
                    if (checkfollow != null)
                    {
                        postcheck.Follow = true;
                    }
                    else
                    {
                        postcheck.Follow = false;
                    }

                }
            return Ok(resultData);
            }
            if (claim.Value == null)
            {
                return BadRequest();
            }
            var getuserspecificpost = _postrepository.getAllPost(a => a.Userid == claim.Value);
            return Ok(getuserspecificpost);

            //var req = Request.Headers;
        }

        [HttpPost]
        public IActionResult createPost(IFormCollection data)
        {
            var claimidentity = (ClaimsIdentity)User.Identity;
            var claim = claimidentity.FindFirst(ClaimTypes.Name);
            var value = claim.Value;
            var jsondata = JsonConvert.DeserializeObject<PostDetial>(Request.Form["data"]);
            var files = data.Files;
            if (files.Count > 0)
            {
                var webrootPath = _webhostEnvironment.WebRootPath;
                var filename = Guid.NewGuid().ToString(); // here it will give unique string that we use in storing image
                var extension = Path.GetExtension(files[0].FileName);
                var uploads = Path.Combine(webrootPath, @"image\socialimage");
                using (var filestream = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                {
                    files[0].CopyTo(filestream);
                }
                jsondata.Image = @"\image\socialimage\" + filename + extension;
                
            }
            jsondata.Userid = value;
            if (jsondata == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            if (!_postrepository.createPost(jsondata))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(new { message = "post created successfully" });
        }

        [HttpDelete("{id:int}")]
        public IActionResult deletePost(int id)
        {
            if (id == 0) return BadRequest();
            var postdata = _postrepository.getAllPost(m => m.Id == id).FirstOrDefault();
            if (postdata == null)
            {
                return BadRequest("you will enter a bad request");
            }
            if (!_postrepository.deletePost(postdata))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(new { message = "Deleted successfully" });
        }

        // post related work here
        [HttpPost("LikePost")]
        public IActionResult LikeUnlike()
        {
            var postData = JsonConvert.DeserializeObject<PostDetial>(Request.Form["data"]);
            if (postData == null) return BadRequest("You send the wrong input");
           var getLikePost = _likeUnlikeRepository.getAll(m => m.postId == postData.Id && m.userId == postData.Userid).FirstOrDefault();
            if (getLikePost != null)
            {
                getLikePost.Like = postData.Like;
                if (!_likeUnlikeRepository.UpdateLikeUnlike(getLikePost))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                return Ok(new { status = 1, message = "like or unlike Successfully the Post" });
            }
            var likeData = new LikeUnlike()
            {
                postId = postData.Id,
                userId = postData.Userid,
                Like = true
            };
            if (!_likeUnlikeRepository.createLikeUnlike(likeData))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(new { status = 1, message = "Like Successfully the Post" });
        }
        [HttpPost("FollowPerson")]
        public IActionResult follow()
        {
            var postData = JsonConvert.DeserializeObject<PostDetial>(Request.Form["data"]);
            var claimidentity = (ClaimsIdentity)User.Identity;
            var claim = claimidentity.FindFirst(ClaimTypes.Name);
            //if (postData.Userid == claim.Value) return BadRequest("not follow yourself");
            var followuserexist = _followrespository.getFollower(u => u.UserID == claim.Value && u.followedId == postData.Userid).FirstOrDefault();
            if (followuserexist != null)
            {
                if (!_followrespository.RemoveFollower(followuserexist))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
                return Ok(new { success = 1, message = "Successfully Unfollow" });
            }
            var userfollow = new Followers()
            {
                UserID = claim.Value,
                followedId = postData.Userid
            };
            if (!_followrespository.createFollower(userfollow))
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok(new { status = 1, message = "successfully followed " });
           
        }

    }
}
