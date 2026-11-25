using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Sqlite;
using Entities;

namespace EfcRepositories;

public class AppContext : DbContext 
{ 
    
    public DbSet<Post> Posts => Set<Post>(); 
    public DbSet<User> Users => Set<User>(); 
    public DbSet<Comment> Comments => Set<Comment>(); 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
    { 
        optionsBuilder.UseSqlite(@"Data Source = C:\Users\OHN\Projects\Server\EfcRepositories\app.db"); 
    } 
    
}