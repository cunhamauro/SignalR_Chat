namespace SignalR_Chat.ViewModels.MessageViewModels
{
    public class ChatViewModel
    {
        public ChatViewModel()
        {
            Messages = new List<UserMessagesListViewModel>();
        }

        public string CurrentUserId { get; set; }

        public string ReceiverId { get; set; }

        public string ReceiverUserName { get; set; }

        public IEnumerable<UserMessagesListViewModel> Messages { get; set; }
    }
}
