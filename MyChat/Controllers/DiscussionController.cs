using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using MyChat.Models;
using MyChat.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;

namespace MyChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscussionController : ControllerBase
    {
        private readonly List<Discussion> _discussions = new List<Discussion>();
        private readonly DiscussionService _discussionService;

        public DiscussionController(DiscussionService discussionService)
        {
            _discussionService = discussionService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Discussion>> GetDiscussions()
        {
            var discussions = _discussionService.GetAll();
            return Ok(discussions);
        }

        [HttpGet("{id}")]
        public ActionResult<Discussion> GetDiscussion(int id)
        {
            var discussion = _discussionService.GetDiscussionById(id);
            if (discussion == null)
            {
                return NotFound();
            }

            return Ok(discussion);
        }

        [HttpPost]
        public ActionResult<Discussion> AddDiscussion([FromBody] Discussion newDiscussion)
        {
            newDiscussion.Id = _discussions.Count + 1;
            _discussions.Add(newDiscussion);

            return CreatedAtAction(nameof(GetDiscussion), new { id = newDiscussion.Id }, newDiscussion);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteDiscussion(int id)
        {
            var discussionToRemove = _discussions.FirstOrDefault(d => d.Id == id);

            if (discussionToRemove == null)
            {
                return NotFound();
            }

            _discussions.Remove(discussionToRemove);

            return NoContent();
        }

        [HttpGet("newest")]
        public ActionResult<IEnumerable<Discussion>> GetNewestDiscussions()
        {
            var newestDiscussions = _discussionService.GetNewestDiscussions();
            return Ok(newestDiscussions);
        }

        [HttpGet("popular")]
        public ActionResult<IEnumerable<Discussion>> GetPopularDiscussions()
        {
            var popularDiscussions = _discussionService.GetPopularDiscussions();
            return Ok(popularDiscussions);
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<Discussion>> SearchDiscussions(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return BadRequest("Search term cannot be empty.");
            }

            var results = _discussionService.GetSearchedDiscussion(searchTerm).AsEnumerable();
            return Ok(results);
        }

    }
}
