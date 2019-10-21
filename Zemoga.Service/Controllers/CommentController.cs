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
        [HttpGet]
        [Route("api/Comment/{postid}")]
        [ResponseType(typeof(ICollection<Comment>))]
        public IHttpActionResult Get(long postid)
        {
            using (var db = new CoreDataContext())
            {
                var comments = new List<Comment>();
                try
                {
                    comments = db.Comments
                        .Where(x => x.Post.Id == postid)
                        .ToList();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

                return Ok(comments);
            }
        }

        [HttpPost]
        [Route("api/Comment/{postid}")]
        [ResponseType(typeof(Post))]
        public IHttpActionResult Create(long postid, [FromBody]Comment newComment)
        {
            using (var db = new CoreDataContext())
            {
                try
                {
                    var post = db.Posts
                        .Where(x => x.Id == postid)
                        .FirstOrDefault();
                    newComment.Post = post;
                    db.Comments.Add(newComment);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

                return Ok(newComment);
            }
        }

        [HttpPut]
        [Route("api/Comment")]
        public IHttpActionResult Update(Comment update)
        {
            using (var db = new CoreDataContext())
            {
                try
                {
                    var comment = db.Comments
                        .Where(x => x.Id == update.Id)
                        .FirstOrDefault();

                    if (comment != null)
                    {
                        comment.Text = update.Text;
                        comment.AuthorName = update.AuthorName;
                        comment.ModifiedAt = DateTime.Now;
                        db.SaveChanges();
                    }

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

                return Ok(true);
            }
        }

        [HttpDelete]
        [Route("api/Comment/{commentid}")]
        [ResponseType(typeof(bool))]
        public IHttpActionResult Delete(long commentid)
        {
            using (var db = new CoreDataContext())
            {
                try
                {
                    var comment = db.Comments
                        .Where(x => x.Id == commentid)
                        .FirstOrDefault();
                    db.Comments.Remove(comment);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

                return Ok(true);
            }
        }

    }
}
