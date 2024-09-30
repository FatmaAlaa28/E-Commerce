using Online_Store.Models;
using Microsoft.EntityFrameworkCore;
using static System.Formats.Asn1.AsnWriter;
using System.Runtime.Intrinsics.X86;
namespace Online_Store.Data
{
    public class ApplicationDbContext : DbContext
{
        public ApplicationDbContext()
        {

        }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    public DbSet<User> Users { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<ProductCart> ProductCarts { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Product> products { get; set; }
    public DbSet<Transaction> transactions { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-7TPMJ1D;Initial Catalog=Online--Store;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");



            base.OnConfiguring(optionsBuilder);
    }
}
}
