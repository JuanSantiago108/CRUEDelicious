#pragma warning disable CS8618

using Microsoft.EntityFrameworkCore;
namespace CRUEDelicious.Models;


public class MyContext : DbContext 
{ 
    public MyContext(DbContextOptions options) : base(options) { }
    // the "Post" table name will come from the DbSet property name
    public DbSet<Dish> Dishes { get; set; } 
}