using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyChat.Data;
using MyChat.Models;

namespace MyChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChannelController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Channel>>> GetChannels()
        {
            var channels = await _context.Channels.Include(c => c.Discussions).ThenInclude(d => d.Messages).ToListAsync();
            return Ok(channels);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Channel>> GetChannel(int id)
        {
            var channel = await _context.Channels.Include(c => c.Discussions).ThenInclude(d => d.Messages)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (channel == null)
            {
                return NotFound();
            }

            return Ok(channel);
        }

        [HttpPost]
        public async Task<ActionResult<Channel>> AddChannel([FromBody] Channel newChannel)
        {
            _context.Channels.Add(newChannel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetChannel), new { id = newChannel.Id }, newChannel);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Channel>> UpdateChannel(int id, [FromBody] Channel updatedChannel)
        {
            var existingChannel = await _context.Channels.FindAsync(id);

            if (existingChannel == null)
            {
                return NotFound();
            }

            existingChannel.Name = updatedChannel.Name;

            await _context.SaveChangesAsync();

            return Ok(existingChannel);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteChannel(int id)
        {
            var channelToRemove = await _context.Channels.FindAsync(id);

            if (channelToRemove == null)
            {
                return NotFound();
            }

            _context.Channels.Remove(channelToRemove);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("searchChannels")]
        public async Task<ActionResult<IEnumerable<Channel>>> SearchChannels(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest("Search term cannot be empty.");
            }

            var results = await _context.Channels
                .Where(c => EF.Functions.Like(c.Name, $"%{searchTerm}%"))
                .ToListAsync();

            return Ok(results);
        }
    }
}
