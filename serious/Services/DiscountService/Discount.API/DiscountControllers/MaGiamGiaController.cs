using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DiscountService.Discount.API.DTOs;
using DiscountService.Discount.API.DiscountServices.Interfaces;

namespace DiscountService.Discount.API.DiscountControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaGiamGiaController : ControllerBase
    {
        private readonly IMaGiamGiaService _discountService;

        public MaGiamGiaController(IMaGiamGiaService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDiscounts([FromQuery] DiscountPaginationRequest request)
        {
            var result = await _discountService.GetDiscountsAsync(request);
            return Ok(result);
        }

        [HttpGet("{maGG:guid}")]
        public async Task<IActionResult> GetDiscountById(Guid maGG)
        {
            var discount = await _discountService.GetDiscountByIdAsync(maGG);
            if (discount == null) return NotFound();
            return Ok(discount);
        }

        [HttpGet("code/{maCode}")]
        public async Task<IActionResult> GetDiscountByCode(string maCode)
        {
            var discount = await _discountService.GetDiscountByCodeAsync(maCode);
            if (discount == null) return NotFound();
            return Ok(discount);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscount([FromBody] CreateMaGiamGiaRequest request)
        {
            try
            {
                var result = await _discountService.CreateDiscountAsync(request);
                return CreatedAtAction(nameof(GetDiscountById), new { maGG = result.MaGG }, result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{maGG:guid}")]
        public async Task<IActionResult> UpdateDiscount(Guid maGG, [FromBody] CreateMaGiamGiaRequest request)
        {
            try
            {
                var result = await _discountService.UpdateDiscountAsync(maGG, request);
                if (result == null) return NotFound();
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPatch("use/{maCode}")]
        public async Task<IActionResult> UseDiscount(string maCode, [FromQuery] int quantity = 1)
        {
            var success = await _discountService.DecrementDiscountQuantityAsync(maCode, quantity);
            if (!success) return BadRequest(new { message = "Invalid code, expired, or out of quantity." });
            return NoContent();
        }

        [HttpPost("{maGG:guid}/use")]
        public async Task<IActionResult> UseDiscountById(Guid maGG, [FromQuery] int quantity = 1)
        {
            var success = await _discountService.DecrementDiscountQuantityByIdAsync(maGG, quantity);
            if (!success) return BadRequest(new { message = "Invalid ID, expired, or out of quantity." });
            return NoContent();
        }
    }
}
