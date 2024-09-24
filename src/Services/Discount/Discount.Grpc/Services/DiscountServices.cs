using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountServices(DiscountContext dbContext, ILogger<DiscountServices> logger): DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
            if (coupon is null)
                coupon = new Coupon { ProductName = "No Discount", Amount = 0, ProductDescription = "No Discount Desc" };

            logger.LogInformation("Discount is retrieved for ProductName: {productName}, Amount :{amount}", coupon.ProductName, coupon.Amount);
            var couponModel = coupon.Adapt<CouponModel>();

            return couponModel;
        }


        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));

            dbContext.Coupons.Add(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is succesfully create. ProductName: {productName}", coupon.ProductName);


            var couponmodel = coupon.Adapt<CouponModel>();
            return couponmodel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();
            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));

            logger.LogInformation("Discount is successfuly updated. {productName}", coupon.ProductName);

            dbContext.Coupons.Update(coupon);
            await dbContext.SaveChangesAsync();

            var couponModel = coupon.Adapt<CouponModel>();

            return couponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x=>x.ProductName== request.ProductName);
            if(coupon is null)
                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with product: {request.ProductName} was not found"));

            dbContext.Coupons.Remove(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is succesfully deleted. Product Name: {productName}", request.ProductName);


            return new DeleteDiscountResponse { Success=true};


        }

    }
}
