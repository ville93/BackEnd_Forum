using MyChat.Models;

namespace MyChat.Services
{
    public class ChannelService
    {
        private readonly List<Channel> _channels =
        [
            new Channel { Id = 1, Name = "General" },
            new Channel { Id = 2, Name = "Random" },
        ];

        public List<Channel> GetSerchedChannels(string searchTerm)
        {
            return _channels
                .Where(c => c.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
