using Microsoft.AspNetCore.SignalR;
using SignalR_Chat.Data;
using SignalR_Chat.Interfaces;
using SignalR_Chat.Models;

namespace SignalR_Chat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public ChatHub(ApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task SendMessage(string receiverId, string message)
        {
            var dateNow = DateTime.Now;

            var date = dateNow.ToShortDateString();
            var time = dateNow.ToShortTimeString();

            string senderId = _currentUserService.UserId;

            var messageToAdd = new Message()
            {
                Text = message,
                SenderId = senderId,
                Date = dateNow,
                ReceiverId = receiverId,
            };

            await _context.AddAsync(messageToAdd);
            await _context.SaveChangesAsync();

            List<string> users = new List<string>()
            {
                receiverId,senderId,
            };

            await Clients.Users(users).SendAsync("ReceiveMessage", message, date, time, senderId);
        }
    }
}
