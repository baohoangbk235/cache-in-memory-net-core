using Microsoft.EntityFrameworkCore;
using RedisExample.Model;


namespace RedisExample.Data
{
    public class DbContextClass: DbContext
    {
        public DbContextClass(DbContextOptions<DbContextClass> options): base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
