using finalProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(finalProject.Startup))]
namespace finalProject
{
    public partial class Startup
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRoles();
            CreateUsers();
        }
        public void CreateUsers()
        {
            var userManger = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = new ApplicationUser();
            user.Email = "admin1@gmail.com";
            user.UserName = "admin1";
            var check = userManger.Create(user, "++18Toptmem");
            if (check.Succeeded)
            {
                userManger.AddToRole(user.Id, "admins");
            }

        }
        public void CreateRoles()
        {
            var roleManger = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            IdentityRole role;
            if (!roleManger.RoleExists("admins"))
            {
                role = new IdentityRole();
                role.Name = "admins";
                roleManger.Create(role);
            }
            if (!roleManger.RoleExists("Teachers"))
            {
                role = new IdentityRole();
                role.Name = "Teachers";
                roleManger.Create(role);
            }
            if (!roleManger.RoleExists("Parents"))
            {
                role = new IdentityRole();
                role.Name = "Parents";
                roleManger.Create(role);
            }
            if (!roleManger.RoleExists("Students"))
            {
                role = new IdentityRole();
                role.Name = "Students";
                roleManger.Create(role);
            }

        }
    }
}
