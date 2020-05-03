using Enteprise_programming_evita.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(Enteprise_programming_evita.Startup))]
namespace Enteprise_programming_evita
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesAndDefaultUsers();
        }

        private void createRolesAndDefaultUsers()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                using (RoleManager<IdentityRole> roleManager =
                    new RoleManager<IdentityRole>(
                        new RoleStore<IdentityRole>(context)))
                {
                    if (!roleManager.RoleExists("Admin"))
                    {
                        IdentityRole role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                        role.Name = "Admin";
                        roleManager.Create(role);
                    }
                    
                }

                using (UserManager<ApplicationUser> UserManager =
                    new UserManager<ApplicationUser>(
                        new UserStore<ApplicationUser>(context)))
                {
                    if (UserManager.FindByName("admin@yourEmailHost.com") == null)
                    {
                        ApplicationUser user = new ApplicationUser();
                        user.UserName = "administrator@yourEmailHost.com";
                        user.Email = "administrator@yourEmailHost.com";
                        string userPWD = "P@ssw0rd_1234";

                        IdentityResult chkUser = UserManager.Create(user, userPWD);
                        if (chkUser.Succeeded)
                        {
                            IdentityResult chkRole = UserManager.AddToRole(user.Id, "Admin");

                            if (!chkRole.Succeeded)
                            {
                                Console.Error.WriteLine("An error has occured in Startup! admin user was not assigned to Admin role successfully.");
                                Console.WriteLine("An error has occured in Startup! admin user was not assigned to Admin role successfully.");
                            }
                        }
                        else
                        {
                            Console.Error.WriteLine("An error has occured in Startup! admin user does not exist, but was not created successfully.");
                            Console.WriteLine("An error has occured in Startup! admin user does not exist, but was not created successfully.");
                        }
                    }
                }
            }
        }
    }
}
