using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public class DataContext:DbContext 
    { 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=NIRVANA\SQLEXPRESS;Database=API;Trusted_Connection=true;encrypt=false");
        }


        public DbSet<Film> Film { get; set; }
    }
}
