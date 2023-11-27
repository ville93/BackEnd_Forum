using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyChat.Models;

namespace MyChat.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> ApplicationUsers { get; set; }
        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Channel> Channels { get; set; }
    }
}
