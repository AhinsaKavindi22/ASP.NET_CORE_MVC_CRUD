using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    // inherits from the EF core's DB context class (main bridge that connects our project with DB)
    public class WebApplication2Context : DbContext
    {
        // enable needed configurations for our context
        public WebApplication2Context(DbContextOptions<WebApplication2Context> options) : base(options) { } 
     
        // enable us to access this model from database
        public DbSet<Item> Items { get; set; }
    }
}
