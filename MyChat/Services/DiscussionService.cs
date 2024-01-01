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
            .OrderByDescending(d => d.CreatedAt).Take(10)
            .Include(d => d.Messages.OrderBy(m => m.Id).Take(1))
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
