using chatard.Models;
using MaterialDesignThemes.Wpf;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Windows.Media.Imaging;

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
            // Self referencing many to many relationship
            modelBuilder.Entity<UserContacts>()
                .HasRequired(x => x.User)
                .WithMany(x => x.Contacts)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);


            base.OnModelCreating(modelBuilder);
        }
        
    }

    
    
}
