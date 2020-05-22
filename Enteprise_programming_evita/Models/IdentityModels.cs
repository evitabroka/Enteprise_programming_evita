using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
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

           Database.SetInitializer<ApplicationDbContext>(new ApplicationDbContextInitializer());

           
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Enteprise_programming_evita.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<Enteprise_programming_evita.Models.ItemType> ItemTypes { get; set; }

        public System.Data.Entity.DbSet<Enteprise_programming_evita.Models.Item> Items { get; set; }

        public System.Data.Entity.DbSet<Enteprise_programming_evita.Models.Quality> Qualities { get; set; }
    }

    public class ApplicationDbContextInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            //
            var QualityList = new List<Quality>();

            QualityList.Add(new Quality() { QualityName = "Good" });
            QualityList.Add(new Quality() { QualityName = "Bad" });
            QualityList.Add(new Quality() { QualityName = "Poor" });
            QualityList.Add(new Quality() { QualityName = "Excelent" });

            foreach (var quality in QualityList)
            context.Qualities.Add(quality);












            base.Seed(context);
        }
    }
}
