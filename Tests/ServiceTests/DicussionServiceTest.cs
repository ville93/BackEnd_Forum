using MyChat.Data;
using MyChat.Models;
using MyChat.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MyChat.Tests
{
    public class DiscussionServiceTests
    {
        [Fact]
        public void GetAll_ReturnsAllDiscussions()
        {
            // Arrange
            var expectedDiscussions = GetTestDiscussions();
            var mockDbSet = new Mock<DbSet<Discussion>>();

            mockDbSet.As<IQueryable<Discussion>>().Setup(m => m.Provider).Returns(expectedDiscussions.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Discussion>>().Setup(m => m.Expression).Returns(expectedDiscussions.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Discussion>>().Setup(m => m.ElementType).Returns(expectedDiscussions.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Discussion>>().Setup(m => m.GetEnumerator()).Returns(expectedDiscussions.GetEnumerator());

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.Setup(c => c.Discussions).Returns(mockDbSet.Object);
            var service = new DiscussionService(mockContext.Object);

            // Act
            var result = service.GetAll();

            // Assert
            Assert.Equal(expectedDiscussions.Count, result.Count);
            foreach (var expectedDiscussion in expectedDiscussions)
            {
                Assert.Contains(result, d => d.Id == expectedDiscussion.Id);
            }
        }

        [Fact]
        public void GetDiscussionById_ReturnsCorrectDiscussion()
        {
            // Arrange
            var expectedDiscussion = GetTestDiscussions().First();
            var discussions = GetTestDiscussions();
            var mockDbSet = new Mock<DbSet<Discussion>>();

            mockDbSet.As<IQueryable<Discussion>>().Setup(m => m.Provider).Returns(discussions.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Discussion>>().Setup(m => m.Expression).Returns(discussions.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Discussion>>().Setup(m => m.ElementType).Returns(discussions.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Discussion>>().Setup(m => m.GetEnumerator()).Returns(discussions.GetEnumerator());

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.Setup(c => c.Discussions).Returns(mockDbSet.Object);
            var service = new DiscussionService(mockContext.Object);

            // Act
            var result = service.GetDiscussionById(expectedDiscussion.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedDiscussion.Id, result.Id);
            Assert.Equal(expectedDiscussion.Title, result.Title);
        }


        [Fact]
        public void AddDiscussion_AddsNewDiscussion()
        {
            // Arrange
            var newDiscussion = new Discussion
            {
                Id = 100,
                Title = "Test Discussion",
                CreatedAt = DateTime.Now
            };

            var mockDbSet = new Mock<DbSet<Discussion>>();
            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.Setup(c => c.Discussions).Returns(mockDbSet.Object);
            var service = new DiscussionService(mockContext.Object);

            // Act
            var result = service.AddDiscussion(newDiscussion);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(newDiscussion.Id, result.Id);
            Assert.Equal(newDiscussion.Title, result.Title);
            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }

        [Fact]
        public void DeleteDiscussion_RemovesExistingDiscussion()
        {
            // Arrange
            var discussions = GetTestDiscussions();
            var discussionToRemove = discussions.First();
            var mockDbSet = new Mock<DbSet<Discussion>>();

            mockDbSet.Setup(m => m.Find(It.IsAny<object[]>()))
                     .Returns<object[]>(ids => discussions.FirstOrDefault(d => d.Id == (int)ids[0]));

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.Setup(c => c.Discussions).Returns(mockDbSet.Object);
            var service = new DiscussionService(mockContext.Object);

            // Act
            var result = service.DeleteDiscussion(discussionToRemove.Id);

            // Assert
            Assert.True(result);
            mockDbSet.Verify(d => d.Remove(It.IsAny<Discussion>()), Times.Once);
            mockContext.Verify(c => c.SaveChanges(), Times.Once);
        }


        [Fact]
        public void GetNewestDiscussions_ReturnsNewestDiscussions()
        {
            // Arrange
            var expectedDiscussions = GetTestDiscussions().OrderByDescending(d => d.CreatedAt).Take(10).ToList();
            var mockDbSet = new Mock<DbSet<Discussion>>();

            mockDbSet.As<IQueryable<Discussion>>().Setup(m => m.Provider).Returns(expectedDiscussions.AsQueryable().Provider);
            mockDbSet.As<IQueryable<Discussion>>().Setup(m => m.Expression).Returns(expectedDiscussions.AsQueryable().Expression);
            mockDbSet.As<IQueryable<Discussion>>().Setup(m => m.ElementType).Returns(expectedDiscussions.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<Discussion>>().Setup(m => m.GetEnumerator()).Returns(expectedDiscussions.GetEnumerator());

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.Setup(c => c.Discussions).Returns(mockDbSet.Object);

            var service = new DiscussionService(mockContext.Object);

            // Act
            var result = service.GetNewestDiscussions();

            // Assert
            Assert.Equal(expectedDiscussions.Count, result.Count);
            for (int i = 0; i < expectedDiscussions.Count; i++)
            {
                Assert.Equal(expectedDiscussions[i].Id, result[i].Id);
                Assert.Equal(expectedDiscussions[i].Title, result[i].Title);
            }
        }

        private List<Discussion> GetTestDiscussions()
        {
            return new List<Discussion>
            {
                new Discussion
                {
                    Id = 1,
                    Title = "President election",
                    Messages = new List<Message>
                    {
                        new Message
                        {
                            Id = 1,
                            Content = "First message",
                            Timestamp = DateTime.Parse("2024-03-01T13:34:48.815862"),
                            DiscussionId = 19
                        },
                        new Message
                        {
                            Id = 2,
                            Content = "Second message",
                            Timestamp = DateTime.Parse("2024-03-02T11:40:40.9847079"),
                            DiscussionId = 19
                        },
                        new Message
                        {
                            Id = 3,
                            Content = "Third message",
                            Timestamp = DateTime.Parse("2024-03-02T11:42:36.8440027"),
                            DiscussionId = 19
                        },
                    },
                    ChannelId = 1,
                    CreatedAt = DateTime.Parse("2024-03-01T13:34:48.8081486")
                },
                new Discussion
                {
                    Id = 2,
                    Title = "Favorite Video Games",
                    Messages = new List<Message>
                    {
                        new Message
                        {
                            Id = 4,
                            Content = "CS",
                            Timestamp = DateTime.Parse("0001-01-01T00:00:00"),
                            DiscussionId = 18
                        }
                    },
                    ChannelId = 2,
                    CreatedAt = DateTime.Parse("2024-03-01T13:24:13.2529379")
                },
                new Discussion
                {
                    Id = 3,
                    Title = "Favorite Books",
                    Messages = new List<Message>
                    {
                        new Message
                        {
                            Id = 5,
                            Content = "LOTR",
                            Timestamp = DateTime.Parse("0001-01-01T00:00:00"),
                            DiscussionId = 3
                        }
                    },
                    ChannelId = 5,
                    CreatedAt = DateTime.Parse("2024-02-24T11:49:42.2529141")
                },
            };
        }
    }
}
