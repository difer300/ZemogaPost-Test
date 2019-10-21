using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Zemoga.Models;
using Zemoga.Service.Data.Data;

namespace Zemoga.Service.Controllers
{
    public class CommentController : ApiController
    {
        private readonly ICoreDataContext _db;

        public CommentController(ICoreDataContext dbContext)
        {
            _db = dbContext;
        }

        public CommentController()
        {
            _db = new CoreDataContext();
        }

        [HttpGet]
        [Route("api/Comment/{postid}")]
        [ResponseType(typeof(ICollection<Comment>))]
        public IHttpActionResult Get(long postid)
        {
            var comments = new List<Comment>();
            try
            {
                comments = _db.Comments
                    .Where(x => x.Post.Id == postid)
                    .ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(comments);
        }

        [HttpPost]
        [Route("api/Comment/{postid}")]
        [ResponseType(typeof(Post))]
        public IHttpActionResult Create(long postid, [FromBody]Comment newComment)
        {
            try
            {
                var post = _db.Posts
                    .Where(x => x.Id == postid)
                    .FirstOrDefault();
                newComment.Post = post;
                _db.Comments.Add(newComment);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(newComment);
        }

        [HttpPut]
        [Route("api/Comment")]
        public IHttpActionResult Update(Comment update)
        {
            try
            {
                var comment = _db.Comments
                    .Where(x => x.Id == update.Id)
                    .FirstOrDefault();

                if (comment != null)
                {
                    comment.Text = update.Text;
                    comment.AuthorName = update.AuthorName;
                    comment.ModifiedAt = DateTime.Now;
                    _db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(true);
        }

        [HttpDelete]
        [Route("api/Comment/{commentid}")]
        [ResponseType(typeof(bool))]
        public IHttpActionResult Delete(long commentid)
        {
            try
            {
                var comment = _db.Comments
                    .Where(x => x.Id == commentid)
                    .FirstOrDefault();
                _db.Comments.Remove(comment);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(true);
        }

    }
}
