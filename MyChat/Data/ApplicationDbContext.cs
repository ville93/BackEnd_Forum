using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyChat.Models;

namespace MyChat.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> ApplicationUsers { get; set; }
        public DbSet<Discussion> Discussions { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Channel> Channels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Channel>().HasData(
                new Channel
                {
                    Id = 1,
                    Name = "Business",
                },
                new Channel
                {
                    Id = 2,
                    Name = "Celebrity",
                },
                new Channel
                {
                    Id = 3,
                    Name = "Gaming",
                },
                new Channel
                {
                    Id = 4,
                    Name = "General",
                },
                new Channel
                {
                    Id = 5,
                    Name = "Politics",
                },
                new Channel
                {
                    Id = 6,
                    Name = "Football Fever",
                }
            );

            builder.Entity<Discussion>().HasData(
                new Discussion
                {
                    Id = 1,
                    Title = "Entrepreneurship Tips",
                    ChannelId = 1,
                    CreatedAt = DateTime.Now.AddHours(-1),
                    
                },
                new Discussion
                {
                    Id = 2,
                    Title = "Marketing Strategies",
                    ChannelId = 1,
                    CreatedAt = DateTime.Now.AddHours(-2),
                },
                new Discussion
                {
                    Id = 3,
                    Title = "Recent Celebrity News",
                    ChannelId = 2,
                    CreatedAt = DateTime.Now.AddHours(-1),
                },
                new Discussion
                {
                    Id = 4,
                    Title = "Favorite Celebrity Moments",
                    ChannelId = 2,
                    CreatedAt = DateTime.Now.AddHours(-2),
                },
                new Discussion
                {
                    Id = 5,
                    Title = "Favorite Video Games",
                    ChannelId = 3,
                    CreatedAt = DateTime.Now.AddHours(-1),
                },
                new Discussion
                {
                    Id = 6,
                    Title = "Upcoming Game Releases",
                    ChannelId = 3,
                    CreatedAt = DateTime.Now.AddHours(-2),
                },
                new Discussion
                {
                    Id = 7,
                    Title = "Favorite Books",
                    ChannelId = 4,
                    CreatedAt = DateTime.Now.AddHours(-1),
                },
                new Discussion
                {
                    Id = 8,
                    Title = "What is happening?",
                    ChannelId = 4,
                    CreatedAt = DateTime.Now.AddHours(-2),
                },
                new Discussion
                {
                    Id = 9,
                    Title = "Election Season",
                    ChannelId = 5,
                    CreatedAt = DateTime.Now.AddHours(-1),
                },
                new Discussion
                {
                    Id = 10,
                    Title = "Foreign Relations",
                    ChannelId = 5,
                    CreatedAt = DateTime.Now.AddHours(-2),
                },
                new Discussion
                {
                    Id = 11,
                    Title = "Who's excited for the upcoming football season?",
                    ChannelId = 6,
                    CreatedAt = DateTime.Now.AddHours(-1),
                },
                new Discussion
                {
                    Id = 12,
                    Title = "Basketball Talk",
                    ChannelId = 6,
                    CreatedAt = DateTime.Now.AddHours(-2),
                }
            );

            builder.Entity<Message>().HasData(
                new List<Message>
                {
                    new Message
                    {
                        Id = 1,
                        Content = "What are some valuable tips for starting a successful business?",
                        Timestamp = DateTime.Now.AddMinutes(-30),
                        DiscussionId = 1,
                    },
                    new Message
                    {
                        Id = 2,
                        Content = "In my experience, focusing on customer needs is crucial!",
                        Timestamp = DateTime.Now.AddMinutes(-20),
                        DiscussionId = 1,
                    },
                    new Message
                    {
                        Id = 3,
                        Content = "Let's discuss effective marketing strategies.",
                        Timestamp = DateTime.Now.AddMinutes(-40),
                        DiscussionId = 2,
                    },
                    new Message
                    {
                        Id = 4,
                        Content = "I've found social media marketing to be quite impactful.",
                        Timestamp = DateTime.Now.AddMinutes(-15),
                        DiscussionId = 2,
                    },
                    new Message
                    {
                        Id = 5,
                        Content = "What's the latest celebrity gossip or news you've heard?",
                        Timestamp = DateTime.Now.AddMinutes(-30),
                        DiscussionId = 3,
                    },
                    new Message
                    {
                        Id = 6,
                        Content = "I heard there's a new blockbuster movie in the making!",
                        Timestamp = DateTime.Now.AddMinutes(-20),
                        DiscussionId = 3,
                    },
                    new Message
                    {
                        Id = 7,
                        Content = "What are some of your all-time favorite celebrity moments?",
                        Timestamp = DateTime.Now.AddMinutes(-40),
                        DiscussionId = 4,
                    },
                    new Message
                    {
                        Id = 8,
                        Content = "I loved that awards ceremony last year. Memorable performances!",
                        Timestamp = DateTime.Now.AddMinutes(-15),
                        DiscussionId = 4,
                    },
                    new Message
                    {
                        Id = 9,
                        Content = "What are your all-time favorite video games?",
                        Timestamp = DateTime.Now.AddMinutes(-30),
                        DiscussionId = 5,
                    },
                    new Message
                    {
                        Id = 10,
                        Content = "I can't get enough of The Witcher 3. What about you?",
                        Timestamp = DateTime.Now.AddMinutes(-20),
                        DiscussionId = 5,
                    }
                    ,new Message
                    {
                        Id = 11,
                        Content = "Any exciting upcoming game releases you're looking forward to?",
                        Timestamp = DateTime.Now.AddMinutes(-40),
                        DiscussionId = 6,
                    },
                    new Message
                    {
                        Id = 12,
                        Content = "I can't wait for the next installment of the Assassin's Creed series!",
                        Timestamp = DateTime.Now.AddMinutes(-15),
                        DiscussionId = 6,
                    },
                    new Message
                    {
                        Id = 13,
                        Content = "Share your favorite books and why you love them.",
                        Timestamp = DateTime.Now.AddMinutes(-30),
                        DiscussionId = 7,
                    },
                    new Message
                    {
                        Id = 14,
                        Content = "I recently read 'The Great Gatsby' – such a classic!",
                        Timestamp = DateTime.Now.AddMinutes(-20),
                        DiscussionId = 7,
                    },
                    new Message
                    {
                        Id = 15,
                        Content = "Discussing important things.",
                        Timestamp = DateTime.Now.AddMinutes(-40),
                        DiscussionId = 8,
                    },
                    new Message
                    {
                        Id = 16,
                        Content = "Any thoughts?",
                        Timestamp = DateTime.Now.AddMinutes(-15),
                        DiscussionId = 8,
                    },
                    new Message
                    {
                        Id = 17,
                        Content = "With the election season approaching, what are your predictions?",
                        Timestamp = DateTime.Now.AddMinutes(-30),
                        DiscussionId = 9,
                    },
                    new Message
                    {
                        Id = 18,
                        Content = "I believe the debates will play a crucial role this time.",
                        Timestamp = DateTime.Now.AddMinutes(-20),
                        DiscussionId = 9,
                    },
                    new Message
                    {
                        Id = 19,
                        Content = "Let's discuss the current state of foreign relations.",
                        Timestamp = DateTime.Now.AddMinutes(-40),
                        DiscussionId = 10,
                    },
                    new Message
                    {
                        Id = 20,
                        Content = "The recent summit had some interesting outcomes.",
                        Timestamp = DateTime.Now.AddMinutes(-15),
                        DiscussionId = 10,
                    },
                    new Message
                    {
                        Id = 21,
                        Content = "I can't wait to see my favorite team in action!",
                        Timestamp = DateTime.Now.AddMinutes(-30),
                        DiscussionId = 10,
                    },
                    new Message
                    {
                        Id = 22,
                        Content = "Predictions for the championship winner?",
                        Timestamp = DateTime.Now.AddMinutes(-20),
                        DiscussionId = 11,
                    },
                    new Message
                    {
                        Id = 23,
                        Content = "Let's discuss the latest basketball games!",
                        Timestamp = DateTime.Now.AddMinutes(-40),
                        DiscussionId = 12,
                    },
                    new Message
                    {
                        Id = 24,
                        Content = "Who's your favorite basketball team?",
                        Timestamp = DateTime.Now.AddMinutes(-15),
                        DiscussionId = 12,
                    }
                }
            );
        }
    }
}
