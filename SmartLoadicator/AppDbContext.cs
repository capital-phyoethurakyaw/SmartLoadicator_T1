using Microsoft.EntityFrameworkCore;
using SmartLoadicator;
using SmartLoadicator.Views;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Promotion> Promotions { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
       var ServerPath = (System.IO.Directory.GetCurrentDirectory().ToString().Replace("bin\\Debug", "DBLite").Contains("DBLite") ? System.IO.Directory.GetCurrentDirectory().ToString().Replace("bin\\Debug", "DBLite") : System.IO.Directory.GetCurrentDirectory().ToString().Replace("bin\\Debug", "DBLite") + "\\DBLite") + "\\" + "smc.sqlite";

        optionsBuilder.UseSqlite("Data Source=" + ServerPath);
    }
}
