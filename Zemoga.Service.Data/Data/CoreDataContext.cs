using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zemoga.Models;

namespace Zemoga.Service.Data.Data
{
    public class CoreDataContext : DbContext, ICoreDataContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostStatusChange> PostStatusChanges { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }

        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public CoreDataContext()
            : base(BuildConnectionString())
        {
        }

        private static string BuildConnectionString()
        {
            return ConfigurationManager.AppSettings["CoreDataContextConnectingString"];
        }

        public override int SaveChanges()
        {
            try
            {
                Logger.Debug("CoreDataContext.SaveChanges begin");
                var baseRtn = base.SaveChanges();
                Logger.Debug("CoreDataContext.SaveChanges end");
                return baseRtn;
            }
            catch (Exception e)
            {
                Logger.Error("Error Saving CoreDataContext", e);
                throw;
            }
        }
    }
}
