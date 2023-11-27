using Microsoft.AspNetCore.Mvc;
using MyChat.Models;

namespace MyChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscussionController : ControllerBase
    {
        private readonly List<Channel> _channels = [];

        [HttpGet]
        public ActionResult<IEnumerable<Channel>> GetDiscussions()
        {
            return Ok(_channels);
        }

        [HttpGet("{id}")]
        public ActionResult<Channel> GetDiscussion(int id)
        {
            var discussion = _channels.FirstOrDefault(c => c.Id == id);

            if (discussion == null)
            {
                return NotFound();
            }

            return Ok(discussion);
        }

        [HttpPost]
        public ActionResult<Channel> AddDicussion([FromBody] Channel newDiscussion)
        {
            // Voit lisätä tarkistuksia uuden keskustelun lisäämiseen tarvittaessa
            newDiscussion.Id = _channels.Count + 1;
            _channels.Add(newDiscussion);

            return CreatedAtAction(nameof(GetDiscussion), new { id = newDiscussion.Id }, newDiscussion);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteDicussion(int id)
        {
            var discussionToRemove = _channels.FirstOrDefault(c => c.Id == id);

            if (discussionToRemove == null)
            {
                return NotFound();
            }

            _channels.Remove(discussionToRemove);

            return NoContent();
        }
    }
}
