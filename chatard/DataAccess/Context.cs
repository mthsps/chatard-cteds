using Microsoft.EntityFrameworkCore;
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
        }


    }
}
