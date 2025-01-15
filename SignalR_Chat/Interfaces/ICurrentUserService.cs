using SignalR_Chat.Models;

namespace SignalR_Chat.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }

        Task<AppUser> GetUser();
    }
}
