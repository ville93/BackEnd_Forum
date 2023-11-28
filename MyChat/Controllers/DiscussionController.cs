using Microsoft.AspNetCore.Mvc;
using MyChat.Models;
using MyChat.Services;
using System.Collections.Generic;
using System.Linq;

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
            return Ok(_discussions);
        }

        [HttpGet("{id}")]
        public ActionResult<Discussion> GetDiscussion(int id)
        {
            var discussion = _discussions.FirstOrDefault(d => d.Id == id);

            if (discussion == null)
            {
                return NotFound();
            }

            return Ok(discussion);
        }

        [HttpPost]
        public ActionResult<Discussion> AddDiscussion([FromBody] Discussion newDiscussion)
        {
            // Voit lisätä tarkistuksia uuden keskustelun lisäämiseen tarvittaessa
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
    }
}
