using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using DiscountService.Discount.API.Data;
using DiscountService.Discount.API.DTOs;
using DiscountService.Discount.API.Models;
using DiscountService.Discount.API.DiscountServices.Interfaces;

namespace DiscountService.Discount.API.DiscountServices.Implementations
{
    public class MaGiamGiaService : IMaGiamGiaService
    {
        private readonly IMongoCollection<MaGiamGia> _discountsCollection;

        public MaGiamGiaService(IOptions<DiscountDbSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _discountsCollection = mongoDatabase.GetCollection<MaGiamGia>(dbSettings.Value.CollectionName);
        }

        public async Task<PagedResult<MaGiamGiaDto>> GetDiscountsAsync(DiscountPaginationRequest request)
        {
            try
            {
                var filter = Builders<MaGiamGia>.Filter.Empty;
                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    filter = Builders<MaGiamGia>.Filter.Regex(x => x.MaCode, new MongoDB.Bson.BsonRegularExpression(request.Keyword, "i"));
                }

                var totalCount = await _discountsCollection.CountDocumentsAsync(filter);
                var discounts = await _discountsCollection.Find(filter)
                    .SortByDescending(x => x.HanSuDung)
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Limit(request.PageSize)
                    .ToListAsync();

                return new PagedResult<MaGiamGiaDto>
                {
                    Items = discounts.Select(MapToDto),
                    TotalCount = (int)totalCount,
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize
                };
            }
            catch
            {
                return new PagedResult<MaGiamGiaDto>
                {
                    Items = Array.Empty<MaGiamGiaDto>(),
                    TotalCount = 0,
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize
                };
            }
        }

        public async Task<MaGiamGiaDto?> GetDiscountByIdAsync(Guid maGG)
        {
            try
            {
                var discount = await _discountsCollection.Find(x => x.MaGG == maGG).FirstOrDefaultAsync();
                return discount != null ? MapToDto(discount) : null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<MaGiamGiaDto?> GetDiscountByCodeAsync(string maCode)
        {
            try
            {
                var discount = await _discountsCollection.Find(x => x.MaCode == maCode).FirstOrDefaultAsync();
                return discount != null ? MapToDto(discount) : null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<MaGiamGiaDto> CreateDiscountAsync(CreateMaGiamGiaRequest request)
        {
            // Kiểm tra trùng mã
            var existing = await _discountsCollection.Find(x => x.MaCode == request.MaCode).FirstOrDefaultAsync();
            if (existing != null)
            {
                throw new InvalidOperationException("Mã giảm giá này đã tồn tại.");
            }

            var normalizedScope = NormalizeScope(request);
            var discount = new MaGiamGia
            {
                MaGG = Guid.NewGuid(),
                MaCode = request.MaCode,
                Loai = request.Loai,
                SoTien = request.SoTien,
                DonHangToiThieu = request.DonHangToiThieu,
                GiaTriGiamToiDa = request.GiaTriGiamToiDa,
                SoLuong = request.SoLuong,
                HanSuDung = request.HanSuDung,
                ApDungCho = normalizedScope.ApDungCho,
                MaLDM = normalizedScope.MaLDM,
                MaDM = normalizedScope.MaDM,
                MaSP = normalizedScope.MaSP,
                MaSPs = normalizedScope.MaSPs
            };

            await _discountsCollection.InsertOneAsync(discount);
            return MapToDto(discount);
        }

        public async Task<MaGiamGiaDto?> UpdateDiscountAsync(Guid id, CreateMaGiamGiaRequest request)
        {
            // Kiểm tra trùng mã (trừ mã của chính nó)
            var existing = await _discountsCollection.Find(x => x.MaCode == request.MaCode && x.MaGG != id).FirstOrDefaultAsync();
            if (existing != null)
            {
                throw new InvalidOperationException("Mã giảm giá này đã tồn tại.");
            }

            var normalizedScope = NormalizeScope(request);
            var update = Builders<MaGiamGia>.Update
                .Set(x => x.MaCode, request.MaCode)
                .Set(x => x.Loai, request.Loai)
                .Set(x => x.SoTien, request.SoTien)
                .Set(x => x.DonHangToiThieu, request.DonHangToiThieu)
                .Set(x => x.GiaTriGiamToiDa, request.GiaTriGiamToiDa)
                .Set(x => x.SoLuong, request.SoLuong)
                .Set(x => x.HanSuDung, request.HanSuDung)
                .Set(x => x.ApDungCho, normalizedScope.ApDungCho)
                .Set(x => x.MaLDM, normalizedScope.MaLDM)
                .Set(x => x.MaDM, normalizedScope.MaDM)
                .Set(x => x.MaSP, normalizedScope.MaSP)
                .Set(x => x.MaSPs, normalizedScope.MaSPs);

            var options = new FindOneAndUpdateOptions<MaGiamGia>
            {
                ReturnDocument = ReturnDocument.After
            };

            var result = await _discountsCollection.FindOneAndUpdateAsync(x => x.MaGG == id, update, options);
            return result != null ? MapToDto(result) : null;
        }

        public async Task<bool> DecrementDiscountQuantityAsync(string maCode, int quantity = 1)
        {
            try
            {
                if (quantity <= 0) return true;
                var update = Builders<MaGiamGia>.Update.Inc(x => x.SoLuong, -quantity);
                var result = await _discountsCollection.UpdateOneAsync(
                    x => x.MaCode == maCode && x.SoLuong >= quantity && x.HanSuDung >= DateTime.UtcNow,
                    update
                );

                return result.ModifiedCount > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DecrementDiscountQuantityByIdAsync(Guid maGG, int quantity = 1)
        {
            try
            {
                if (quantity <= 0) return true;
                var update = Builders<MaGiamGia>.Update.Inc(x => x.SoLuong, -quantity);
                var result = await _discountsCollection.UpdateOneAsync(
                    x => x.MaGG == maGG && x.SoLuong >= quantity && x.HanSuDung >= DateTime.UtcNow,
                    update
                );

                return result.ModifiedCount > 0;
            }
            catch
            {
                return false;
            }
        }

        private static MaGiamGiaDto MapToDto(MaGiamGia model)
        {
            return new MaGiamGiaDto
            {
                MaGG = model.MaGG,
                MaCode = model.MaCode,
                Loai = model.Loai,
                SoTien = model.SoTien,
                DonHangToiThieu = model.DonHangToiThieu,
                GiaTriGiamToiDa = model.GiaTriGiamToiDa,
                SoLuong = model.SoLuong,
                HanSuDung = model.HanSuDung,
                ApDungCho = model.ApDungCho,
                MaLDM = model.MaLDM,
                MaDM = model.MaDM,
                MaSP = model.MaSP,
                MaSPs = model.MaSPs ?? new List<Guid>()
            };
        }

        private static CreateMaGiamGiaRequest NormalizeScope(CreateMaGiamGiaRequest request)
        {
            var normalized = new CreateMaGiamGiaRequest
            {
                MaCode = request.MaCode,
                Loai = request.Loai,
                SoTien = request.SoTien,
                DonHangToiThieu = request.DonHangToiThieu,
                GiaTriGiamToiDa = request.GiaTriGiamToiDa,
                SoLuong = request.SoLuong,
                HanSuDung = request.HanSuDung,
                ApDungCho = request.ApDungCho,
                MaLDM = request.MaLDM,
                MaDM = request.MaDM,
                MaSP = request.MaSP,
                MaSPs = request.MaSPs ?? new List<Guid>()
            };

            if (normalized.ApDungCho == "SanPham" && normalized.MaSPs.Count > 0)
            {
                normalized.MaSP = null;
            }
            else if (normalized.ApDungCho == "SanPham" && normalized.MaSPs.Count == 0 && normalized.MaSP.HasValue)
            {
                normalized.MaSPs = new List<Guid> { normalized.MaSP.Value };
                normalized.MaSP = null;
            }

            return normalized;
        }
    }
}
