using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
 
public class UserContext : DbContext, IUserRepository
{
    private DbSet<User> Users { get; set; }

        public UserContext(DbContextOptions<UserContext> options)
        : base(options)
    {
            Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("Users");
    }

    public void SaveResult(string login, int level)
    {

    }

    public IEnumerable<User> GetStatictics()
    {
        return Users.ToList();
    }
}