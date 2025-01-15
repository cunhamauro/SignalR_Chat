using SignalR_Chat.ViewModels.MessageViewModels;

namespace SignalR_Chat.Interfaces
{
    public interface IMessageService
    {
        Task<IEnumerable<MessagesUserListViewModel>> GetUsers();

        Task<ChatViewModel> GetMessages(string selectedUserId);
    }
}
