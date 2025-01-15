using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SignalR_Chat.Models
{
    public class AppUser : IdentityUser
    {
        [InverseProperty(nameof(Message.Sender))]
        public virtual ICollection<Message> SentMessages {  get; set; }

        [InverseProperty(nameof(Message.Receiver))]
        public virtual ICollection<Message> ReceivedMessages {  get; set; }
    }
}
