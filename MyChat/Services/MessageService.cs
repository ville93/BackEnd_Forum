using Microsoft.EntityFrameworkCore;
using MyChat.Data;
using MyChat.Models;

namespace MyChat.Services
{
    public class MessageService
    {
        private readonly ApplicationDbContext _context;
        private readonly DiscussionService _discussionService;

        public MessageService(ApplicationDbContext context, DiscussionService discussionService)
        {
            _context = context;
            _discussionService = discussionService;
        }

        public Message GetMessageById(int id)
        {
            return _context.Messages.FirstOrDefault(m => m.Id == id);
        }

        public Message AddMessage(Message newMessage, int discussionId)
        {
            if (newMessage == null)
            {
                throw new ArgumentNullException(nameof(newMessage));
            }

            var discussion = _discussionService.GetDiscussionById(discussionId);
            if (discussion == null)
            {
                throw new ArgumentException("Invalid discussionId");
            }

            newMessage.Timestamp = DateTime.Now;
            newMessage.DiscussionId = discussionId;
            _context.Messages.Add(newMessage);
            _context.SaveChanges();

            return newMessage;
        }
    }
}
