using Microsoft.EntityFrameworkCore;
using MyChat.Data;
using MyChat.Models;

namespace MyChat.Services
{
    public class DiscussionService
    {
        private readonly ApplicationDbContext _context;

        public DiscussionService(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Discussion> GetAll()
        {
            return _context.Discussions.ToList();
        }

        public Discussion GetDiscussionById(int id)
        {
            return _context.Discussions
                .Include(d => d.Messages)
                .FirstOrDefault(d => d.Id == id);
        }

        public List<Discussion> GetNewestDiscussions()
        {
            var discussionsWithMessages = _context.Discussions
                .OrderByDescending(d => d.CreatedAt)
                .Take(10)
                .Include(d => d.Messages)  
                .Select(d => new Discussion
                {
                    Id = d.Id,
                    Title = d.Title,
                    ChannelId = d.ChannelId,
                    CreatedAt = d.CreatedAt,
                    Messages = d.Messages.OrderByDescending(m => m.Timestamp).Take(1).ToList(),
                    AnswersCount = d.Messages.Count
                })
                .ToList();

            return discussionsWithMessages;
        }

        public List<Discussion> GetPopularDiscussions()
        {
            var discussionsWithMessages = _context.Discussions
                .OrderByDescending(d => d.Messages.Count) 
                .Take(10)
                .Include(d => d.Messages)
                .Select(d => new Discussion
                {
                    Id = d.Id,
                    Title = d.Title,
                    ChannelId = d.ChannelId,
                    CreatedAt = d.CreatedAt,
                    Messages = d.Messages.OrderByDescending(m => m.Timestamp).Take(1).ToList(),
                    AnswersCount = d.Messages.Count
                })
                .ToList();

            return discussionsWithMessages;
        }


        public List<Discussion> GetSearchedDiscussion(string searchTerm)
        {
            var discussionsWithMessages = _context.Discussions
                .Include(d => d.Messages)  
                .Where(d => EF.Functions.Like(d.Title, $"%{searchTerm}%"))
                .ToList();

            foreach (var discussion in discussionsWithMessages)
            {
                discussion.AnswersCount = discussion.Messages.Count;
            }

            return discussionsWithMessages;
        }
    }
}
