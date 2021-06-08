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
        if (Users.Any(u => u.Login == login))
        {
            var u = Users.First(u => u.Login == login);
            double f = (double)u.Count / (u.Count + 1);
            u.Average = u.Average * f + level / (u.Count + 1);
            u.Count++;
            u.Best = Math.Max(u.Best,level);
        } else
        {
            var u = new User() {Login = login, Best = level, Average = level, Count = 1};
            Users.Add(u);
        }
        SaveChanges();
    }

    public IEnumerable<User> GetStatictics()
    {
        return Users.ToList();
    }
}