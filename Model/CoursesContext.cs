using Microsoft.EntityFrameworkCore;

namespace CoursesApi.Models
{
    public class CoursesContext : DbContext
    {
        public CoursesContext(DbContextOptions<CoursesContext> options)
            : base(options)
        {
        }

        public DbSet<Courses> Courses { get; set; }
    }
}