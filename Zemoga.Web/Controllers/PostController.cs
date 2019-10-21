using Microsoft.Ajax.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Zemoga.Models;
using Zemoga.Web.Service.Api;

namespace Zemoga.Web.Controllers
{
    public class PostController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var currentUser = Session["CurrentUser"];
            var postSvc = new PostService();
            ICollection<Post> listPosts = await postSvc.GetPostsByStatus(PostStatus.Published);

            if (currentUser != null)
            {
                if (((User)currentUser).Role == UserRole.Writer)
                {
                    var listPostsNew = await postSvc.GetPostsByStatus(PostStatus.New);
                    listPostsNew.ForEach(listPosts.Add);
                }
                else
                {
                    var listPostsApproval = await postSvc.GetPostsByStatus(PostStatus.PendingPublishApproval);
                    listPostsApproval.ForEach(listPosts.Add);
                }
            }

            ViewBag.CurrentUser = currentUser;
            return View(listPosts);
        }

        public ActionResult Create()
        {
            var currentUser = Session["CurrentUser"];

            if (currentUser != null && ((User)currentUser).Role == UserRole.Writer)
            {
                return View("Create");
            }

            return Redirect("/Post");
        }

        public async Task<ActionResult> Details(long id)
        {
            if (id == 0)
            {
                return Redirect("/Post");
            }

            var postSvc = new PostService();
            var post = await postSvc.GetPostById(id);
            var postStatusChanges = await postSvc.GetStatesById(id);
            var lastChange = postStatusChanges.OrderByDescending(x => x.Id).FirstOrDefault();
            ViewBag.ApprovedBy = lastChange.User.Name;
            ViewBag.ApprovedAt = lastChange.CreatedAt;

            if (post == null)
            {
                return Redirect("/Post");
            }

            return View("Details", post);
        }

        public async Task<ActionResult> Edit(long id)
        {
            var currentUser = Session["CurrentUser"];

            if (currentUser != null && ((User)currentUser).Role == UserRole.Writer)
            {
                if (id == 0)
                {
                    return Redirect("/Post");
                }

                var postSvc = new PostService();
                var post = await postSvc.GetPostById(id);

                if (post == null)
                {
                    return Redirect("/Post");
                }

                return View("Edit", post);
            }

            return Redirect("/Post");
        }

        public async Task<ActionResult> Delete(long id)
        {
            var currentUser = Session["CurrentUser"];

            if (currentUser != null && ((User)currentUser).Role == UserRole.Writer)
            {
                if (id == 0)
                {
                    return Redirect("/Post");
                }

                var postSvc = new PostService();
                var post = await postSvc.DeletePost(id);
            }

            return Redirect("/Post");
        }

        [HttpPost]
        public async Task<ActionResult> Create(Post post)
        {
            var currentUser = Session["CurrentUser"];

            if (currentUser != null && ((User)currentUser).Role == UserRole.Writer)
            {
                var postSvc = new PostService();
                var newPost = await postSvc.CreatePost(post);
                await postSvc.MovePost(newPost.Id, new PostStatusChange() { Status = PostStatus.New, User = (User)currentUser });
            }

            return Redirect("/Post");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Post post)
        {
            var postSvc = new PostService();
            await postSvc.EditPost(post);
            return Redirect("/Post");
        }

        [HttpPost]
        public async Task<ActionResult> Publish(long id)
        {
            var currentUser = Session["CurrentUser"];

            if (currentUser != null && ((User)currentUser).Role == UserRole.Writer)
            {
                if (id == 0)
                {
                    return Redirect("/Post");
                }

                var postSvc = new PostService();
                await postSvc.MovePost(id, new PostStatusChange() { Status = PostStatus.PendingPublishApproval, User = (User)currentUser });
            }

            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public async Task<ActionResult> Approve(long id)
        {
            var currentUser = Session["CurrentUser"];

            if (currentUser != null && ((User)currentUser).Role == UserRole.Editor)
            {
                if (id == 0)
                {
                    return Redirect("/Post");
                }

                var postSvc = new PostService();
                await postSvc.MovePost(id, new PostStatusChange() { Status = PostStatus.Published, User = (User)currentUser });
            }

            return Redirect("/Post");
        }

        [HttpPost]
        public async Task<ActionResult> Reject(long id)
        {
            var currentUser = Session["CurrentUser"];

            if (currentUser != null && ((User)currentUser).Role == UserRole.Editor)
            {
                if (id == 0)
                {
                    return Redirect("/Post");
                }

                var postSvc = new PostService();
                await postSvc.MovePost(id, new PostStatusChange() { Status = PostStatus.New, User = (User)currentUser });
            }

            return Redirect("/Post");
        }

    }
}