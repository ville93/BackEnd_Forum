using Microsoft.AspNetCore.Mvc;
using MyChat.Models;
using MyChat.Services;

namespace MyChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscussionController : ControllerBase
    {
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
            try
            {
                var addedDiscussion = _discussionService.AddDiscussion(newDiscussion);
                return CreatedAtAction(nameof(GetDiscussion), new { id = addedDiscussion.Id }, addedDiscussion);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the discussion.");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteDiscussion(int id)
        {
            var success = _discussionService.DeleteDiscussion(id);
            if (!success)
            {
                return NotFound();
            }

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

            var results = _discussionService.GetSearchedDiscussion(searchTerm);
            return Ok(results);
        }
    }
}
