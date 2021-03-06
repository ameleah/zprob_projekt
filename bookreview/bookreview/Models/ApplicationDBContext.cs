﻿using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using bookreview.Models.BaseModels;
namespace bookreview.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Rate> Rates { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();

        }

    }
}