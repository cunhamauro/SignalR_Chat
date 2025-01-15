using Microsoft.EntityFrameworkCore;
using SignalR_Chat.Data;
using SignalR_Chat.Interfaces;
using SignalR_Chat.Models;
using SignalR_Chat.ViewModels.MessageViewModels;

namespace SignalR_Chat.Services
{
    public class MessageService : IMessageService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public MessageService(ApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<ChatViewModel> GetMessages(string selectedUserId)
        {
            var currentUserId = _currentUserService.UserId;

            var selectedUser = await _context.Users.FirstOrDefaultAsync(i => i.Id == selectedUserId);

            var selectedUserName = string.Empty;

            if (selectedUser != null)
            {
                selectedUserName = selectedUser.UserName;
            }

            var chatViewModel = new ChatViewModel()
            {
                CurrentUserId = currentUserId,
                ReceiverId = selectedUserId,
                ReceiverUserName = selectedUserName,
            };

            var messages = await _context.Messages.Where(i => (i.SenderId == currentUserId || i.SenderId == selectedUserId) && (i.ReceiverId == currentUserId || i.ReceiverId == selectedUserId)).Select(i => new UserMessagesListViewModel
            {
                Id = i.Id,
                Text = i.Text,
                Date = i.Date.ToShortDateString(),
                Time = i.Date.ToShortTimeString(),
                IsCurrentUserSentMessage = i.SenderId == currentUserId,
            }).ToListAsync();

            chatViewModel.Messages = messages;

            return chatViewModel;
        }

        public async Task<IEnumerable<MessagesUserListViewModel>> GetUsers()
        {
            var currentUserId = _currentUserService.UserId;

            var users = await _context.Users.Where(i => i.Id != currentUserId).Select(i => new MessagesUserListViewModel()
            {
                Id = i.Id,
                UserName = i.UserName,
                LastMessage = _context.Messages.Where(m => (m.SenderId == currentUserId || m.SenderId == i.Id) && (m.ReceiverId == currentUserId || m.ReceiverId == i.Id)).OrderByDescending(m => m.Id).Select(m => m.Text).FirstOrDefault(),
            }).ToListAsync();

            return users;
        }
    }
}
