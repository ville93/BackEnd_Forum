using MyChat.Models;

namespace MyChat.Services
{
    public class DiscussionService
    {
        private readonly List<Discussion> _discussions;
        public DiscussionService()
        {
            _discussions = new List<Discussion>
            {
                new Discussion
                {
                    Id = 1,
                    Title = "Discussion 1",
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
                    Title = "Discussion 2",
                    ChannelId = 2,
                    CreatedAt = DateTime.Now.AddHours(-2),
                    Messages = new List<Message>
                    {
                        new Message { Id = 3, Content = "Discussing important things.", Timestamp = DateTime.Now.AddMinutes(-40) },
                        new Message { Id = 4, Content = "Any thoughts?", Timestamp = DateTime.Now.AddMinutes(-15) }
                    }
                },
            };
        }


        public List<Discussion> GetNewestDiscussions()
        {
            return _discussions.OrderByDescending(d => d.CreatedAt).ToList();
        }

        public List<Discussion> GetPopularDiscussions()
        {
            return _discussions.OrderByDescending(d => d.Messages.Count).ToList();
        }

        public List<Discussion> GetSearchedDiscussion(string searchTerm)
        {
            return _discussions
                .Where(d => d.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
