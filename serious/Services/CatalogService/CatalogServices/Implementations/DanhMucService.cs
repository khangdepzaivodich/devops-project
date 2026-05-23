using CatalogService.Data;
using CatalogService.DTOs;
using CatalogService.Models;
using CatalogService.Helpers;
using Microsoft.EntityFrameworkCore;
namespace CatalogService.CatalogServices.Implementations
{
    public class DanhMucService : CatalogService.CatalogServices.Interfaces.IDanhMucService
    {
        private readonly AppDbContext _context;

        public DanhMucService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DanhMucDTO>> GetAllAsync()
        {
            return await _context.DanhMucs
                .Select(x => new DanhMucDTO
                {
                    MaDM = x.MaDM,
                    MaLDM = x.MaLDM,
                    TenDM = x.TenDM,
                    Slug = x.Slug
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<DanhMucDTO>> GetByLoaiDanhMucIdAsync(Guid maLDM)
        {
            return await _context.DanhMucs
                .Where(x => x.MaLDM == maLDM)
                .Select(x => new DanhMucDTO
                {
                    MaDM = x.MaDM,
                    MaLDM = x.MaLDM,
                    TenDM = x.TenDM,
                    Slug = x.Slug
                })
                .ToListAsync();
        }

        public async Task<DanhMucDTO?> GetByIdAsync(Guid id)
        {
            var entity = await _context.DanhMucs.FindAsync(id);
            if (entity == null) return null;

            return new DanhMucDTO
            {
                MaDM = entity.MaDM,
                MaLDM = entity.MaLDM,
                TenDM = entity.TenDM,
                Slug = entity.Slug
            };
        }

        public async Task<DanhMucDTO?> GetBySlugAsync(string slug)
        {
            return await _context.DanhMucs
                .Select(x => new DanhMucDTO
                {
                    MaDM = x.MaDM,
                    MaLDM = x.MaLDM,
                    TenDM = x.TenDM,
                    Slug = x.Slug
                })
                .FirstOrDefaultAsync(x => x.Slug == slug);
        }

        public async Task<DanhMucDTO> CreateAsync(DanhMucCreateUpdateDTO dto)
        {
            var entity = new DanhMuc
            {
                MaDM = Guid.NewGuid(),
                MaLDM = dto.MaLDM,
                TenDM = dto.TenDM,
                Slug = SlugHelper.GenerateSlug(dto.TenDM)
            };

            _context.DanhMucs.Add(entity);
            await _context.SaveChangesAsync();

            return new DanhMucDTO
            {
                MaDM = entity.MaDM,
                MaLDM = entity.MaLDM,
                TenDM = entity.TenDM,
                Slug = entity.Slug
            };
        }

        public async Task<bool> UpdateAsync(Guid id, DanhMucCreateUpdateDTO dto)
        {
            var entity = await _context.DanhMucs.FindAsync(id);
            if (entity == null) return false;

            entity.MaLDM = dto.MaLDM;
            entity.TenDM = dto.TenDM;
            entity.Slug = SlugHelper.GenerateSlug(dto.TenDM);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var entity = await _context.DanhMucs.FindAsync(id);
            if (entity == null) return false;

            _context.DanhMucs.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
