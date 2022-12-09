using chatard.Models;
using MaterialDesignThemes.Wpf;
using System;
using System.Configuration;
using System.Data.Entity;

namespace chatard.DataAccess
{
    public class Context : DbContext
    {
        public Context() : base("name=ChatardDatabase") 
        {
            Database.SetInitializer<Context>(new DropCreateDatabaseIfModelChanges<Context>());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserContacts> UserContacts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        
    }

    
    
}
