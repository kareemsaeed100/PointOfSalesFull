using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PointOfSalesFull.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
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
    public class ApplicationRole : IdentityRole
    {
     
        public ApplicationRole()
          : base() { }

        public ApplicationRole(string name, string rolDescrption)
            : base(name)
        {
            RolDescrption = rolDescrption;
        }

        public virtual string RolDescrption
        {
            get; set;

        }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<AccStock> AccStocks { get; set; }
        public DbSet<AccTransDet> AccTransDet { get; set; }
        public DbSet<AccTransHed> AccTransHead { get; set; }
        public DbSet<AccVchrs> AccVchr { get; set; }
        public DbSet<SalesInvoice> SalesInvoicies { get; set; }
        public DbSet<Sales_Detailes> SalesDetailes { get; set; }
        public DbSet<PurchasesInvoice> PurchasesInvoices { get; set; }
        public DbSet<PurchasesDetailes> PurchasesDetailes { get; set; }
        public DbSet<SalesReturn> SalesReturn { get; set; }
        public DbSet<SalesReturnDetailes> SalesReturnDetailes { get; set; }
        public DbSet<PurchasesReturn> PurchasesReturn { get; set; }
        public DbSet<PurchasesReturnDetailes> PurchasesReturnDetailes { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Product> Products { get; set; }
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Branches> Branchies { get; set; }
        public DbSet<Defination> Defination { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<Units> Units { get; set; }
        public DbSet<Nationality> Nationalities { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<PayMode> PayModes { get; set; }
        public DbSet<Saller> Salleres { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}