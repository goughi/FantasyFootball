using Microsoft.Owin;
using Owin;
using testfan2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

using System.Linq;


[assembly: OwinStartupAttribute(typeof(testfan2.Startup))]
namespace testfan2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }


        // In this method we will create default User roles and Admin user for login  
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();


            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User   
            if (!roleManager.RoleExists("Administrator"))
            {

                // first we create Admin rool  
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Administrator";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                 

                //var user = new ApplicationUser();
                //user.UserName = "goughi";
                //user.Email = "goughi@hotmail.com";
                
                //string userPWD = "1981TUESday";

                //var chkUser = UserManager.Create(user, userPWD);

                ////Add default User to Role Admin  
                //if (chkUser.Succeeded)
                //{
                //    var result1 = UserManager.AddToRole(user.Id, "Administrator");

                //}
            }

            // creating paid user role   
            if (!roleManager.RoleExists("PaidUser"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "PaidUser";
                roleManager.Create(role);

            }

            if (!roleManager.RoleExists("FreeUser"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "FreeUser";
                roleManager.Create(role);

            }
        }
    }
}
