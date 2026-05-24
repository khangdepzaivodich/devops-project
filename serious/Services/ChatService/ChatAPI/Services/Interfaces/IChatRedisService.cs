using System;
using System.Threading.Tasks;

namespace ChatService.ChatAPI.Services.Interfaces
{
    public interface IChatRedisService
    {
        Task SetUserOnlineAsync(string userId);
        Task RegisterStaffOnlineAsync(string staffId);
        Task<DateTime?> GetStaffLastSeenAsync(string staffId);
        Task<string?> AssignLeastBusyStaffAsync();
        Task AddToWaitingQueueAsync(string sessionId);
        Task<string?> GetNextInWaitingQueueAsync();
        Task DecreaseStaffWorkloadAsync(string staffId);
        Task IncreaseStaffWorkloadAsync(string staffId);
        Task<bool> IsUserOnlineAsync(string userId);
        Task MapSessionToUserAsync(string sessionId, string userId, string hoTen);
        Task<(string? userId, string? hoTen)> GetSessionMappingAsync(string sessionId);
        Task SetStaffNameAsync(string staffId, string staffName);
        Task<string?> GetStaffNameAsync(string staffId);
        Task SetStaffAvatarAsync(string staffId, string avatar);
        Task<string?> GetStaffAvatarAsync(string staffId);
        Task<long> GetNextGuestNumberWithDateAsync(string dateKey);
    }
}
