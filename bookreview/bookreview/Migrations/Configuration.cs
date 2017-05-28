namespace bookreview.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<bookreview.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "bookreview.Models.ApplicationDbContext";
        }

        protected override void Seed(bookreview.Models.ApplicationDbContext context)
        {
        }
    }
}
