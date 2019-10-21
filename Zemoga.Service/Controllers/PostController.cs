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
        [HttpGet]
        [Route("api/Post/{id}")]
        [ResponseType(typeof(Post))]
        public IHttpActionResult GetById(long id)
        {
            using (var db = new CoreDataContext())
            {
                var post = new Post();
                try
                {
                    post = db.Posts
                        .Where(x => x.Id == id)
                        .FirstOrDefault();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

                return Ok(post);
            }
        }

        [HttpGet]
        [Route("api/Post/status/{status}")]
        [ResponseType(typeof(ICollection<Post>))]
        public IHttpActionResult Get(long status)
        {
            using (var db = new CoreDataContext())
            {
                var posts = new List<Post>();
                try
                {
                    posts = db.Posts
                        .Where(x => x.Status == (PostStatus)status)
                        .ToList();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

                return Ok(posts);
            }
        }

        [HttpPost]
        [Route("api/Post")]
        [ResponseType(typeof(Post))]
        public IHttpActionResult Create([FromBody]Post newPost)
        {
            using (var db = new CoreDataContext())
            {
                try
                {
                    newPost.Status = PostStatus.New;
                    db.Posts.Add(newPost);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

                return Ok(newPost);
            }
        }

        [HttpPut]
        [Route("api/Post")]
        [ResponseType(typeof(BoolResponse))]
        public IHttpActionResult Update(Post update)
        {
            using (var db = new CoreDataContext())
            {
                try
                {
                    var post = db.Posts
                        .Where(x => x.Id == update.Id)
                        .FirstOrDefault();

                    if (post != null)
                    {
                        post.Title = update.Title;
                        post.Content = update.Content;
                        post.AuthorName = update.AuthorName;
                        post.ModifiedAt = DateTime.Now;
                        db.SaveChanges();
                    }

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

                return Ok(new BoolResponse(true));
            }
        }

        [HttpDelete]
        [Route("api/Post/{postid}")]
        [ResponseType(typeof(BoolResponse))]
        public IHttpActionResult Delete(long postid)
        {
            using (var db = new CoreDataContext())
            {
                try
                {
                    var postStatusChange = db.PostStatusChanges
                           .Where(x => x.Post.Id == postid)
                           .ToList();
                    db.PostStatusChanges.RemoveRange(postStatusChange);
                    var post = db.Posts
                        .Where(x => x.Id == postid)
                        .FirstOrDefault();
                    db.Posts.Remove(post);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

                return Ok(new BoolResponse(true));
            }
        }

        [HttpPost]
        [Route("api/Post/move/{postid}")]
        [ResponseType(typeof(BoolResponse))]
        public IHttpActionResult Move(long postid, [FromBody]PostStatusChange postStatusChange)
        {
            using (var db = new CoreDataContext())
            {
                try
                {
                    var post = db.Posts
                       .Where(x => x.Id == postid)
                       .FirstOrDefault();
                    if (post != null)
                    {
                        post.ModifiedAt = DateTime.Now;
                        post.Status = (PostStatus)postStatusChange.Status;

                        var user = db.Users
                           .Where(x => x.Id == postStatusChange.User.Id)
                           .FirstOrDefault();
                        postStatusChange.Post = post;
                        postStatusChange.User = user;
                        db.PostStatusChanges.Add(postStatusChange);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

                return Ok(new BoolResponse(true));
            }
        }

        [HttpGet]
        [Route("api/Post/{id}/states")]
        [ResponseType(typeof(ICollection<PostStatusChange>))]
        public IHttpActionResult GetStatesById(long id)
        {
            using (var db = new CoreDataContext())
            {
                var postStatusChange = new List<PostStatusChange>();
                try
                {
                    postStatusChange = db.PostStatusChanges
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
}
