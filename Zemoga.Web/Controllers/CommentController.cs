using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using Zemoga.Models;
using Zemoga.Web.Service.Api;

namespace Zemoga.Web.Controllers
{
    public class CommentController : Controller
    {
        public async Task<ActionResult> Index(long postId)
        {
            ViewBag.CurrentPost = postId;
            var commentSvc = new CommentService();
            ICollection<Comment> listComments = await commentSvc.GetCommentsByPost(postId);
            return View(listComments);
        }

        public ActionResult Create(long postId)
        {
            ViewBag.CurrentPost = postId;
            return View("Create");
        }

        [HttpPost]
        public async Task<ActionResult> Create(long postId, Comment comment)
        {
            var commentSvc = new CommentService();
            var newComment = await commentSvc.CreateComment(postId, comment);
            return Redirect("Index?postId=" + postId);
        }

    }
}