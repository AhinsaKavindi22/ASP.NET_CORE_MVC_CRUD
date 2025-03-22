using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    // inherits from the EF core's DB context class (main bridge that connects our project with DB)
    public class WebApplication2Context : DbContext
    {
        // enable needed configurations for our context. 
        public WebApplication2Context(DbContextOptions<WebApplication2Context> options) : base(options) { }

        // give initial data to the item and SN in db. For that override OnModelCreating method.
        // this method takes the model builder class as the parameter and configure model to specify either data or relationships 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasData(
               new Item {Id=6 ,Name="oven", Price=40, SerialNumberId=10});

            modelBuilder.Entity<SerialNumber>().HasData(
              new SerialNumber { Id = 10, Name = "Oven1413", ItemId=6 });

            modelBuilder.Entity<Category>().HasData(
              new Category { Id = 1, Name = "Electronics" },
              new Category { Id = 2, Name = "Books", }
            );

            // specify both of the foreign keys 
            modelBuilder.Entity<ItemClient>().HasKey(ic => new
            {
                ic.ItemId,
                ic.ClientId
            });

            // specify the relationship each of item and itemClient models
            modelBuilder.Entity<ItemClient>().HasOne(i => i.Item).WithMany(ic => ic.ItemClients).HasForeignKey(i => i.ItemId);

            // specify the relationship each of itemClient and client models
            modelBuilder.Entity<ItemClient>().HasOne(c => c.Client).WithMany(ic => ic.ItemClients).HasForeignKey(c => c.ClientId);

            base.OnModelCreating(modelBuilder);
        }

        // enable us to access this model from database
        public DbSet<Item> Items { get; set; }

        // enable us to access this model from database
        public DbSet<SerialNumber> SerialNumbers { get; set; }

        // enable us to access this model from database
        public DbSet<Category>? Categories { get; set; }

        public DbSet<ItemClient>? ItemClients { get; set; }


    }
}
