namespace Zemoga.Service.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using Zemoga.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<Zemoga.Service.Data.Data.CoreDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Zemoga.Service.Data.Data.CoreDataContext context)
        {
            context.Users.AddOrUpdate(x => x.Id,
                new User() { Id = 1, Name = "Jane Austen - Writer", Role = UserRole.Writer, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now },
                new User() { Id = 2, Name = "Charles Dickens - Editor", Role = UserRole.Editor, CreatedAt = DateTime.Now, ModifiedAt = DateTime.Now }
            );
        }
    }
}
