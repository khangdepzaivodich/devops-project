using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatService.ChatAPI.Models;

namespace ChatService.ChatAPI.Services.Interfaces
{
    public interface IChatMongoService
    {
        Task<List<PhienTroChuyen>> GetDanhSachPhienAsync();
        Task<List<PhienTroChuyen>> GetDanhSachPhienByUserIdAsync(Guid userId);
        Task TaoPhienAsync(PhienTroChuyen phien);
        Task CapNhatThongTinPhienAsync(Guid idPhien, string lastMessage);
        Task ResetUnreadAsync(Guid idPhien);
        Task<bool> CapNhatTrangThaiPhienAsync(Guid idPhien, string trangThaiMoi, string? trangThaiCu = null);
        Task UpgradePhienAsync(Guid idPhien, Guid userId, string hoTen, string? avatar = null);
        Task CapNhatThongTinStaffPhienAsync(Guid idPhien, string staffId, string staffName, string? staffAvatar = null);
        Task<PhienTroChuyen?> GetPhienByIdAsync(Guid idPhien);
        Task<List<HoiThoai>> GetTinNhanTheoPhienAsync(Guid idPhien);
        Task GuiTinNhanAsync(HoiThoai tinNhan);
        Task<List<PhienTroChuyen>> GetIdleSessionsAsync(TimeSpan threshold);
        Task<List<PhienTroChuyen>> GetIdleGuestSessionsAsync(TimeSpan threshold);
        Task<bool> XoaPhienChatAsync(Guid idPhien);
    }
}
