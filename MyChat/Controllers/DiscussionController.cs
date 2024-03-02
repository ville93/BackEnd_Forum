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
        private readonly MessageService _messageService;

        public DiscussionController(DiscussionService discussionService, MessageService messageService)
        {
            _discussionService = discussionService;
            _messageService = messageService;
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

        [HttpGet("message/{id}")]
        public ActionResult<Message> GetMessage(int id)
        {
            var message = _messageService.GetMessageById(id);
            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }

        [HttpPost("{discussionId}")]
        public ActionResult<Message> AddMessageToDiscussion(int discussionId, [FromBody] Message newMessage)
        {
            try
            {
                var addedMessage = _messageService.AddMessage(newMessage, discussionId);
                return CreatedAtAction(nameof(GetMessage), new { id = addedMessage.Id }, addedMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the message.");
            }
        }
    }
}
