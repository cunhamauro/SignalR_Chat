using Microsoft.AspNetCore.Mvc;
using SignalR_Chat.Interfaces;

namespace SignalR_Chat.Controllers
{
    public class MessagesController : Controller
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _messageService.GetUsers();

            return View(users);
        }

        public async Task<IActionResult> Chat(string selectedUserId)
        {
            var chatViewModel = await _messageService.GetMessages(selectedUserId);

            return View(chatViewModel);
        }
    }
}
