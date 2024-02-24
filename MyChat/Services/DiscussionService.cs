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

        public Discussion AddDiscussion(Discussion newDiscussion)
        {
            if (newDiscussion == null)
            {
                throw new ArgumentNullException(nameof(newDiscussion));
            }

            newDiscussion.CreatedAt = DateTime.Now;
            _context.Discussions.Add(newDiscussion);
            _context.SaveChanges();

            if (newDiscussion.Messages != null && newDiscussion.Messages.Any())
            {
                foreach (var message in newDiscussion.Messages)
                {
                    message.Timestamp = DateTime.Now;
                    message.DiscussionId = newDiscussion.Id;
                    _context.Messages.Add(message);
                }
                _context.SaveChanges();
            }

            return newDiscussion;
        }

        public bool DeleteDiscussion(int id)
        {
            var discussionToRemove = _context.Discussions.FirstOrDefault(d => d.Id == id);

            if (discussionToRemove == null)
            {
                return false;
            }

            _context.Discussions.Remove(discussionToRemove);
            _context.SaveChanges();

            return true;
        }

        public List<Discussion> GetNewestDiscussions()
        {
            return _context.Discussions
                .OrderByDescending(d => d.CreatedAt)
                .Take(10)
                .Include(d => d.Messages)
                .ToList();
        }

        public List<Discussion> GetPopularDiscussions()
        {
            return _context.Discussions
                .OrderByDescending(d => d.Messages.Count)
                .Take(10)
                .Include(d => d.Messages)
                .ToList();
        }

        public List<Discussion> GetSearchedDiscussion(string searchTerm)
        {
            return _context.Discussions
                .Include(d => d.Messages)
                .Where(d => EF.Functions.Like(d.Title, $"%{searchTerm}%"))
                .ToList();
        }
    }
}
