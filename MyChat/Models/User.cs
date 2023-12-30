using Microsoft.AspNetCore.Identity;

namespace MyChat.Models
{
    public class User : IdentityUser
    {
        public int Id { get; set; }
        public string UniqueId { get; set; }
    }
}
