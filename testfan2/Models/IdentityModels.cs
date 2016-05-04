using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace testfan2.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        //public int TeamID { get; set; }
        //public virtual FantasyTeam FantasyTeam { get; set; }
        [Display(Name = "Surname")]
        [StringLength(50, ErrorMessage = "Surname cannot be longer than 50 characters.")]
        [Required(ErrorMessage = "You must enter a surname")]
        public string CustomerSurname { get; set; }

        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Required(ErrorMessage = "You must enter a firstname")]
        public string CustomerFirstName { get; set; }
        public Nation SupportingNation { get; set; }
        public System.DateTime? BirthDate { get; set; }
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
        //public ApplicationDbContext()
        //    : base("DefaultConnection", throwIfV1Schema: false)
            public ApplicationDbContext()
            : base("FantasyFootballContext")
        {
        }
        public DbSet<Player> Players { get; set; }
        public DbSet<FantasyTeam> FantasyTeams { get; set; }

        public DbSet<PlayerRoundStat> PlayerRoundStats { get; set; }
       // public DbSet<Customer> Customers { get; set; }
        public DbSet<FantasyLeague> FantasyLeagues { get; set; }
        public DbSet<Fixture> Fixtures { get; set; }

        public DbSet<NationTeam> NationTeams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");

            modelBuilder.Entity<FantasyTeam>()
         .HasMany(t => t.Players).WithMany(p => p.FantasyTeams)
         .Map(t => t.MapLeftKey("TeamID")
             .MapRightKey("PlayerID")
             .ToTable("FantasyTeamPlayer"));


        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        
        
        
    }

}