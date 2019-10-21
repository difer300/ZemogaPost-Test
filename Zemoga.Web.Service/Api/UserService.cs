using System.Collections.Generic;
using System.Threading.Tasks;
using Zemoga.Models;

namespace Zemoga.Web.Service.Api
{
    public class UserService : Service<User>
    {
        public string Url
        {
            get
            {
                return "/api/user";
            }
        }

        public async Task<ICollection<User>> GetUsers()
        {
            string url = string.Format("{0}", this.Url);
            return await this.MakeRequest<ICollection<User>>(HttpRequestMethod.GET, url);
        }

        public async Task<User> GetUser(long id)
        {
            string url = string.Format("{0}/{1}", this.Url, id);
            return await this.MakeRequest<User>(HttpRequestMethod.GET, url);
        }

    }
}
