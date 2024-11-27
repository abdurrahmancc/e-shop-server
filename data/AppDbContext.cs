using e_shop_server.Models;
using Microsoft.EntityFrameworkCore;

namespace e_shop_server.data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options): base(options){}
        public DbSet<ProductModel>Products {get; set;}
    }
}
