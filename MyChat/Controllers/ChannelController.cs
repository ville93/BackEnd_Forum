using Microsoft.AspNetCore.Mvc;
using MyChat.Models;
using MyChat.Services;
using System.Collections.Generic;
using System.Linq;

namespace MyChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelController : ControllerBase
    {
        private readonly List<Channel> _channels =
        [
            new Channel
            {
                Id = 1,
                Name = "General",
                Discussions =
                {
                    new Discussion
                    {
                        Id = 1,
                        Title = "Breaking news",
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
                        Title = "What is happening?",
                        ChannelId = 2,
                        CreatedAt = DateTime.Now.AddHours(-2),
                        Messages = new List<Message>
                        {
                            new Message { Id = 3, Content = "Discussing important things.", Timestamp = DateTime.Now.AddMinutes(-40) },
                            new Message { Id = 4, Content = "Any thoughts?", Timestamp = DateTime.Now.AddMinutes(-15) }
                        }
                    },
                }
            },
            new Channel
            {
                Id = 1,
                Name = "Politics",
                Discussions =
                {
                    new Discussion
                    {
                        Id = 1,
                        Title = "Laws",
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
                        Title = "NATO discussion",
                        ChannelId = 2,
                        CreatedAt = DateTime.Now.AddHours(-2),
                        Messages = new List<Message>
                        {
                            new Message { Id = 3, Content = "Discussing important things.", Timestamp = DateTime.Now.AddMinutes(-40) },
                            new Message { Id = 4, Content = "Any thoughts?", Timestamp = DateTime.Now.AddMinutes(-15) }
                        }
                    },
                }
            },
        ];

        private readonly ChannelService _channelService;
        public ChannelController(ChannelService channelService)
        {
            _channelService = channelService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Channel>> GetChannels()
        {
            return Ok(_channels);
        }

        [HttpGet("{id}")]
        public ActionResult<Channel> GetChannel(int id)
        {
            var channel = _channels.FirstOrDefault(c => c.Id == id);

            if (channel == null)
            {
                return NotFound();
            }

            return Ok(channel);
        }

        [HttpPost]
        public ActionResult<Channel> AddChannel([FromBody] Channel newChannel)
        {
            // Voit lisätä tarkistuksia uuden kanavan lisäämiseen tarvittaessa
            newChannel.Id = _channels.Count + 1;
            _channels.Add(newChannel);

            return CreatedAtAction(nameof(GetChannel), new { id = newChannel.Id }, newChannel);
        }

        [HttpPut("{id}")]
        public ActionResult<Channel> UpdateChannel(int id, [FromBody] Channel updatedChannel)
        {
            var existingChannel = _channels.FirstOrDefault(c => c.Id == id);

            if (existingChannel == null)
            {
                return NotFound();
            }

            // Voit lisätä tarkistuksia päivittämiseen tarvittaessa
            existingChannel.Name = updatedChannel.Name;

            return Ok(existingChannel);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteChannel(int id)
        {
            var channelToRemove = _channels.FirstOrDefault(c => c.Id == id);

            if (channelToRemove == null)
            {
                return NotFound();
            }

            _channels.Remove(channelToRemove);

            return NoContent();
        }

        [HttpGet("searchChannels")]
        public ActionResult<IEnumerable<Channel>> SearchChannels(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest("Search term cannot be empty.");
            }

            var results = _channelService.GetSerchedChannels(searchTerm);
            return Ok(results);
        }
    }
}
