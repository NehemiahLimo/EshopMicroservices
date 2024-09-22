using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountContext: DbContext
    {
        public DbSet<Coupon> Coupons { get; set; } = default!;

        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon { Id = 1, ProductName = "Mandazi", ProductDescription = "Ni ya mahamri buana", Amount = 20 },
                new Coupon { Id = 2, ProductName = "Chapati", ProductDescription = "Chapo chapo", Amount = 40 }
                );
        }
    }

}
