using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Enteprise_programming_evita.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("PropertyConnection", throwIfV1Schema: false)


        {
            // Every time we start the system, the old database will be dropped (deleted) and a new one is created
            // Database.SetInitializer<ApplicationDbContext>(new DropCreateDatabaseAlways<ApplicationDbContext>());

         //   Database.SetInitializer<ApplicationDbContext>(new ApplicationDbContextInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Enteprise_programming_evita.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<Enteprise_programming_evita.Models.ItemType> ItemTypes { get; set; }

        public System.Data.Entity.DbSet<Enteprise_programming_evita.Models.Item> Items { get; set; }
    }

    //public class ApplicationDbContextInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    //{
    //    protected override void Seed(ApplicationDbContext context)
    //    {
    //        base.Seed(context);
    //    }
    //}
}
