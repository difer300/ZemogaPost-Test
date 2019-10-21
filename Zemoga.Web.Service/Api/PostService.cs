using System.Collections.Generic;
using System.Threading.Tasks;
using Zemoga.Models;

namespace Zemoga.Web.Service.Api
{
    public class PostService : Service<Post>
    {
        public string Url
        {
            get
            {
                return "/api/post";
            }
        }

        public async Task<ICollection<Post>> GetPostsByStatus(PostStatus status)
        {
            string url = string.Format("{0}/status/{1}", this.Url, (int)status);
            return await this.MakeRequest<ICollection<Post>>(HttpRequestMethod.GET, url);
        }

        public async Task<Post> GetPostById(long id)
        {
            string url = string.Format("{0}/{1}", this.Url, id);
            return await this.MakeRequest<Post>(HttpRequestMethod.GET, url);
        }

        public async Task<Post> CreatePost(Post post)
        {
            string url = string.Format("{0}", this.Url);
            return await this.MakeRequest<Post>(HttpRequestMethod.POST, url, body: post);
        }

        public async Task<BoolResponse> EditPost(Post post)
        {
            string url = string.Format("{0}", this.Url);
            return await this.MakeRequest<BoolResponse>(HttpRequestMethod.PUT, url, body: post);
        }

        public async Task<BoolResponse> DeletePost(long postId)
        {
            string url = string.Format("{0}/{1}", this.Url, postId);
            return await this.MakeRequest<BoolResponse>(HttpRequestMethod.DELETE, url);
        }

        public async Task<BoolResponse> MovePost(long postId, PostStatusChange postStatusChange)
        {
            string url = string.Format("{0}/move/{1}", this.Url, postId);
            return await this.MakeRequest<BoolResponse>(HttpRequestMethod.POST, url, body: postStatusChange);
        }

        public async Task<ICollection<PostStatusChange>> GetStatesById(long id)
        {
            string url = string.Format("{0}/{1}/states", this.Url, id);
            return await this.MakeRequest<ICollection<PostStatusChange>>(HttpRequestMethod.GET, url);
        }

    }
}
