using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiscountService.Discount.API.DTOs;

namespace DiscountService.Discount.API.DiscountServices.Interfaces
{
    public interface IMaGiamGiaService
    {
        Task<PagedResult<MaGiamGiaDto>> GetDiscountsAsync(DiscountPaginationRequest request);
        Task<MaGiamGiaDto?> GetDiscountByIdAsync(Guid maGG);
        Task<MaGiamGiaDto?> GetDiscountByCodeAsync(string maCode);
        Task<MaGiamGiaDto> CreateDiscountAsync(CreateMaGiamGiaRequest request);
        Task<MaGiamGiaDto?> UpdateDiscountAsync(Guid id, CreateMaGiamGiaRequest request);
        Task<bool> DecrementDiscountQuantityAsync(string maCode, int quantity = 1);
        Task<bool> DecrementDiscountQuantityByIdAsync(Guid maGG, int quantity = 1);
    }
}
