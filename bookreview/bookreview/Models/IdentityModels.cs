using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using bookreview.Models.BaseModels;
using System.Collections.Generic;

namespace bookreview.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string DisplayedUserName { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        
        public static bool HasRated(Book book)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var rates = context.Rates.Include(r => r.User).Include(r => r.Book);
            foreach (Rate r in rates)
            {
                if (!r.EntityType)
                {
                    continue;
                }
                if (r.Book_Id == book.Id && r.User_Id == System.Web.HttpContext.Current.User.Identity.GetUserId())
                {
                    return true;
                }
            }
            return false;
        }
        public static bool HasRated(Author author)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var rates = context.Rates.Include(r => r.User).Include(r => r.Author);
            foreach (Rate r in rates)
            {
                if(r.EntityType)
                {
                    continue;
                }
                if (r.Author_Id == author.Id && r.User_Id == System.Web.HttpContext.Current.User.Identity.GetUserId())
                {
                    return true;
                }
            }
            return false;
        }

    }
}