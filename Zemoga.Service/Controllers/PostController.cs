using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Zemoga.Models;
using Zemoga.Service.Data.Data;

namespace Zemoga.Service.Controllers
{
    public class PostController : ApiController
    {

        private readonly ICoreDataContext _db;

        public PostController(ICoreDataContext dbContext)
        {
            _db = dbContext;
        }

        public PostController()
        {
            _db = new CoreDataContext();
        }

        [HttpGet]
        [Route("api/Post/{id}")]
        [ResponseType(typeof(Post))]
        public IHttpActionResult GetById(long id)
        {
            var post = new Post();
            try
            {
                post = _db.Posts
                    .Where(x => x.Id == id)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(post);
        }

        [HttpGet]
        [Route("api/Post/status/{status}")]
        [ResponseType(typeof(ICollection<Post>))]
        public IHttpActionResult Get(long status)
        {
            var posts = new List<Post>();
            try
            {
                posts = _db.Posts
                    .Where(x => x.Status == (PostStatus)status)
                    .ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(posts);
        }

        [HttpPost]
        [Route("api/Post")]
        [ResponseType(typeof(Post))]
        public IHttpActionResult Create([FromBody]Post newPost)
        {
            try
            {
                newPost.Status = PostStatus.New;
                _db.Posts.Add(newPost);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(newPost);
        }

        [HttpPut]
        [Route("api/Post")]
        [ResponseType(typeof(BoolResponse))]
        public IHttpActionResult Update(Post update)
        {
            try
            {
                var post = _db.Posts
                    .Where(x => x.Id == update.Id)
                    .FirstOrDefault();

                if (post != null)
                {
                    post.Title = update.Title;
                    post.Content = update.Content;
                    post.AuthorName = update.AuthorName;
                    post.ModifiedAt = DateTime.Now;
                    _db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new BoolResponse(true));
        }

        [HttpDelete]
        [Route("api/Post/{postid}")]
        [ResponseType(typeof(BoolResponse))]
        public IHttpActionResult Delete(long postid)
        {
            try
            {
                var postStatusChange = _db.PostStatusChanges
                       .Where(x => x.Post.Id == postid)
                       .ToList();
                _db.PostStatusChanges.RemoveRange(postStatusChange);
                var post = _db.Posts
                    .Where(x => x.Id == postid)
                    .FirstOrDefault();
                _db.Posts.Remove(post);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new BoolResponse(true));
        }

        [HttpPost]
        [Route("api/Post/move/{postid}")]
        [ResponseType(typeof(BoolResponse))]
        public IHttpActionResult Move(long postid, [FromBody]PostStatusChange postStatusChange)
        {
            try
            {
                var post = _db.Posts
                   .Where(x => x.Id == postid)
                   .FirstOrDefault();
                if (post != null)
                {
                    post.ModifiedAt = DateTime.Now;
                    post.Status = (PostStatus)postStatusChange.Status;

                    var user = _db.Users
                       .Where(x => x.Id == postStatusChange.User.Id)
                       .FirstOrDefault();
                    postStatusChange.Post = post;
                    postStatusChange.User = user;
                    _db.PostStatusChanges.Add(postStatusChange);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new BoolResponse(true));
        }

        [HttpGet]
        [Route("api/Post/{id}/states")]
        [ResponseType(typeof(ICollection<PostStatusChange>))]
        public IHttpActionResult GetStatesById(long id)
        {
            var postStatusChange = new List<PostStatusChange>();
            try
            {
                postStatusChange = _db.PostStatusChanges
                    .Include("User")
                    .Where(x => x.Post.Id == id)
                    .ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(postStatusChange);
        }

    }
}
