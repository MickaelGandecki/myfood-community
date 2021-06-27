using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin;

namespace myfoodapp.Hub.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {

        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {

        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            this.Configuration.LazyLoadingEnabled = true;
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<myfoodapp.Hub.Models.Message> Messages { get; set; }

        public System.Data.Entity.DbSet<myfoodapp.Hub.Models.MessageType> MessageTypes { get; set; }

        public System.Data.Entity.DbSet<myfoodapp.Hub.Models.Measure> Measures { get; set; }

        public System.Data.Entity.DbSet<myfoodapp.Hub.Models.SensorType> SensorTypes { get; set; }

        public System.Data.Entity.DbSet<myfoodapp.Hub.Models.ProductionUnit> ProductionUnits { get; set; }

        public System.Data.Entity.DbSet<myfoodapp.Hub.Models.ProductionUnitType> ProductionUnitTypes { get; set; }

        public System.Data.Entity.DbSet<myfoodapp.Hub.Models.Option> Options { get; set; }

        public System.Data.Entity.DbSet<myfoodapp.Hub.Models.OptionList> OptionLists { get; set; }

        public System.Data.Entity.DbSet<myfoodapp.Hub.Models.ProductionUnitOwner> ProductionUnitOwners { get; set; }

        public System.Data.Entity.DbSet<myfoodapp.Hub.Models.HydroponicType> HydroponicTypes { get; set; }

        public System.Data.Entity.DbSet<myfoodapp.Hub.Models.Log> Logs { get; set; }

        public System.Data.Entity.DbSet<myfoodapp.Hub.Models.Event> Events { get; set; }

        public System.Data.Entity.DbSet<myfoodapp.Hub.Models.Recipes> Recipes { get; set; }

        public System.Data.Entity.DbSet<myfoodapp.Hub.Models.GardeningType> GardeningTypes { get; set; }

        public System.Data.Entity.DbSet<myfoodapp.Hub.Models.WateringLevel> WateringLevels { get; set; }

        public System.Data.Entity.DbSet<myfoodapp.Hub.Models.Month> Months { get; set; }

        public System.Data.Entity.DbSet<myfoodapp.Hub.Models.Season> Seasons { get; set; }

        public System.Data.Entity.DbSet<myfoodapp.Hub.Models.ProductionUnitStatus> ProductionUnitStatus { get; set; }

        public System.Data.Entity.DbSet<myfoodapp.Hub.Models.EventType> EventTypes { get; set; }

        public System.Data.Entity.DbSet<myfoodapp.Hub.Models.EventTypeItem> EventTypeItems { get; set; }

        public System.Data.Entity.DbSet<myfoodapp.Hub.Models.RecognitionRecord> RecognitionRecords { get; set; }

        public System.Data.Entity.DbSet<myfoodapp.Hub.Models.HealthLevel> HealthLevels { get; set; }

        public System.Data.Entity.DbSet<myfoodapp.Hub.Models.ProductionLevel> ProductionLevels { get; set; }

        public System.Data.Entity.DbSet<myfoodapp.Hub.Models.PreferedMoment> PreferedMoments { get; set; }

        public System.Data.Entity.DbSet<myfoodapp.Hub.Models.Language> Languages { get; set; }

    }
}