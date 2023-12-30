using MyChat.Controllers;
using MyChat.Models;

namespace MyChat.Services
{
    public class ChannelService
    {
        private readonly List<Channel> _channels =
        [
            new Channel 
            { 
                Id = 1, Name = "General", 
                Discussions  =
                {
                    new Discussion
                    {
                        Id = 1,
                        Title = "Breaking news",
                        ChannelId = 1,
                        CreatedAt = DateTime.Now.AddHours(-1),
                        Messages = new List<Message>
                        {
                            new Message { Id = 1, Content = "Hello!", Timestamp = DateTime.Now.AddMinutes(-30) },
                            new Message { Id = 2, Content = "How are you?", Timestamp = DateTime.Now.AddMinutes(-20) }
                        }
                    },
                    new Discussion
                    {
                        Id = 2,
                        Title = "What is happening?",
                        ChannelId = 2,
                        CreatedAt = DateTime.Now.AddHours(-2),
                        Messages = new List<Message>
                        {
                            new Message { Id = 3, Content = "Discussing important things.", Timestamp = DateTime.Now.AddMinutes(-40) },
                            new Message { Id = 4, Content = "Any thoughts?", Timestamp = DateTime.Now.AddMinutes(-15) }
                        }
                    },
                }
            },
            new Channel
            {
                Id = 1,
                Name = "Politics",
                Discussions =
                {
                    new Discussion
                    {
                        Id = 1,
                        Title = "Laws",
                        ChannelId = 1,
                        CreatedAt = DateTime.Now.AddHours(-1),
                        Messages = new List<Message>
                        {
                            new Message { Id = 1, Content = "Hello!", Timestamp = DateTime.Now.AddMinutes(-30) },
                            new Message { Id = 2, Content = "How are you?", Timestamp = DateTime.Now.AddMinutes(-20) }
                        }
                    },
                    new Discussion
                    {
                        Id = 2,
                        Title = "NATO discussion",
                        ChannelId = 2,
                        CreatedAt = DateTime.Now.AddHours(-2),
                        Messages = new List<Message>
                        {
                            new Message { Id = 3, Content = "Discussing important things.", Timestamp = DateTime.Now.AddMinutes(-40) },
                            new Message { Id = 4, Content = "Any thoughts?", Timestamp = DateTime.Now.AddMinutes(-15) }
                        }
                    },
                }
            },
        ];

        public List<Channel> GetSerchedChannels(string searchTerm)
        {
            return _channels
                .Where(c => c.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
