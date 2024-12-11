using Bulky.Models;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }

        // Thêm dữ liệu vào bảng 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                    new Category { Id = 1, Name = "1", DisplayOrder = 1 },
                    new Category { Id = 2, Name = "1", DisplayOrder = 2 },
                    new Category { Id = 3, Name = "1", DisplayOrder = 3 }
                );
        }
    }
}
