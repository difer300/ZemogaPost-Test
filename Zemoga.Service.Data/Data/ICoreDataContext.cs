using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zemoga.Models;

namespace Zemoga.Service.Data.Data
{
    public interface ICoreDataContext : IDisposable
    {
        DbSet<Post> Posts { get; set; }
        DbSet<PostStatusChange> PostStatusChanges { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<User> Users { get; set; }

        int SaveChanges();

    }
}
