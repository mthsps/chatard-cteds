using chatard.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;


namespace chatard.DataAccess
{
    public class Context : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["ChatardDatabase"].ConnectionString);
        }
        
        public Context(DbContextOptions<Context> options) : base(options) { 
            Database.EnsureCreated();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(GetUsers());
            modelBuilder.Entity<Message>().ToTable("Messages");
            base.OnModelCreating(modelBuilder);
            Console.WriteLine("Passei por aqui");
        }

        private User[] GetUsers()
        {
            return new User[]
            {
                new User()
                {
                    Id= 1,
                Username = "Hello",
                Password = "admin123",
                Email = "dhahkdksah@"
                }
            };
        }
    }
}
