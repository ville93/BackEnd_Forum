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
            return _context.Discussions
                .Where(d => d.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
