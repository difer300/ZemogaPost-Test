using System.Collections.Generic;
using System.Threading.Tasks;
using Zemoga.Models;

namespace Zemoga.Web.Service.Api
{
    public class CommentService : Service<User>
    {
        public string Url
        {
            get
            {
                return "/api/comment";
            }
        }

        public async Task<ICollection<Comment>> GetCommentsByPost(long postId)
        {
            string url = string.Format("{0}/{1}", this.Url, postId);
            return await this.MakeRequest<ICollection<Comment>>(HttpRequestMethod.GET, url);
        }

        public async Task<Post> CreateComment(long postId, Comment comment)
        {
            string url = string.Format("{0}/{1}", this.Url, postId);
            return await this.MakeRequest<Post>(HttpRequestMethod.POST, url, body: comment);
        }

    }
}
