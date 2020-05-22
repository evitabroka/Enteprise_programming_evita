using Enteprise_programming_evita.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(Enteprise_programming_evita.Startup))]
namespace Enteprise_programming_evita
{
    public partial class Startup
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesAndDefaultUsers();
            Addquality();
            CeatCategories();
            FillItrmTypes();
            FillItems();

        }

        public void Addquality()
        {
            if (db.Qualities == null)
            {

                db.SaveChanges();
            }

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
                    if (!roleManager.RoleExists("RegisteredUser"))
                    {
                        IdentityRole role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                        role.Name = "RegisteredUser";
                        roleManager.Create(role);
                    }



                }
                var userlist = new List<ApplicationUser>();
                userlist.Add(new ApplicationUser() { Email = "Test1@gmail.com", UserName = "Test1@gmail.com" });
                userlist.Add(new ApplicationUser() { Email = "Test2@gmail.com", UserName = "Test2@gmail.com" });
                userlist.Add(new ApplicationUser() { Email = "Test3@gmail.com", UserName = "Test3@gmail.com" });
                userlist.Add(new ApplicationUser() { Email = "Test4@gmail.com", UserName = "Test4@gmail.com" });
                userlist.Add(new ApplicationUser() { Email = "Test5@gmail.com", UserName = "Test5@gmail.com" });

                using (UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context)))
                {
                    foreach (var user in userlist)
                    {
                        IdentityResult chkUser = userManager.Create(user, "userpasword");
                        if (chkUser.Succeeded)
                        {
                            IdentityResult chkRole = userManager.AddToRole(user.Id, "RegisteredUser");
                        }
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

            private void CeatCategories()
            {


                if (!db.Categories.Any())
                {


                    var CategoryList = new List<Category>();

                CategoryList.Add(new Category() { Id = 1, Name = "Drums" });
                CategoryList.Add(new Category() { Id = 1, Name = "Piano" });
                CategoryList.Add(new Category() { Id = 1, Name = "Guitar" });
                CategoryList.Add(new Category() { Id = 1, Name = "Clanernet" });
                CategoryList.Add(new Category() { Id = 1, Name = "Violin" });





                foreach (var category in CategoryList)
                    {
                        db.Categories.Add(category);
                    }
                    db.SaveChanges();
                }
            }
            private void FillItrmTypes()
            {
                if (!db.ItemTypes.Any())
                {
                    var CategoryList = db.Categories.ToList();
                    var ItyemTypeList = new List<ItemType>();
                    for (int c = 0; c < CategoryList.Count; c++)
                    {



                        for (int it = 0; it < 6; it++)
                        {
                            String tname = "ItemType" + c.ToString() + it.ToString();
                            int cat = CategoryList[c].Id;
                            String degimag = @"https://www.dropbox.com/s/9tht25fymzryai9/default842e74219e2c4da5a868c6b82ff0daac.jpg?raw=1";

                            ItyemTypeList.Add(new ItemType() { Name = tname, CategoryId = cat, Image = degimag });
                        }
                    }
                    foreach (var itype in ItyemTypeList)
                    {
                        db.ItemTypes.Add(itype);
                    }
                    db.SaveChanges();
                }
            }
            private void FillItems()
            {
                if (!db.Items.Any())
                {

                    Random random = new Random();
                    var itemlist = new List<Item>();
                    var userlist = new List<ApplicationUser>();
                userlist.Add(new ApplicationUser() { Email = "Test1@gmail.com", UserName = "Test1@gmail.com" });
                userlist.Add(new ApplicationUser() { Email = "Test2@gmail.com", UserName = "Test2@gmail.com" });
                userlist.Add(new ApplicationUser() { Email = "Test3@gmail.com", UserName = "Test3@gmail.com" });
                userlist.Add(new ApplicationUser() { Email = "Test4@gmail.com", UserName = "Test4@gmail.com" });
                userlist.Add(new ApplicationUser() { Email = "Test5@gmail.com", UserName = "Test5@gmail.com" });
                    using (ApplicationDbContext context = new ApplicationDbContext())
                    {
                        using (UserManager<ApplicationUser> userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context)))
                        {
                            var itemTypel = db.ItemTypes.ToList();
                            var qualitylist = db.Qualities.ToList();
                            foreach (var itemTy in itemTypel)
                            {
                                for (int ii = 0; ii < 5; ii++)
                                {
                                    int rndUser = random.Next(0, 4);
                                    int rndQuaality = random.Next(0, 3);
                                    int price = random.Next(1000);
                                    int quantity = random.Next(1000);
                                int qualityiid = qualitylist[rndQuaality].QualityId;
                                    String userlistid = userManager.FindByEmail(userlist[rndUser].Email).Id;
                                if (itemlist.Any(item => item.Price == price && item.QualityId == qualityiid && item.ItemTypeId == itemTy.Id && item.Quantity == quantity))
                                {
                                    ii--;
                                }
                                else {
                                    itemlist.Add(new Item() { ItemTypeId = itemTy.Id, OwnerId = userlistid, Price = price, Quantity = quantity, QualityId = qualityiid, AddingDate = DateTime.Now })
                                        ;

                                }
                            }

                       
                            }
                        }
                    }

                    foreach (var itemm in itemlist)
                    {
                        db.Items.Add(itemm);
                    }
                    db.SaveChanges();
                }

            }
        }
    } 
