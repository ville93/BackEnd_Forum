using Microsoft.EntityFrameworkCore;
using MyChat.Data;
using MyChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyChat.Services
{
    public class DiscussionService
    {
        private readonly ApplicationDbContext _context;

        public DiscussionService(ApplicationDbContext context)
        {
            _context = context;
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
            return _context.Discussions
                .OrderByDescending(d => d.Messages.Count)
                .ToList();
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
