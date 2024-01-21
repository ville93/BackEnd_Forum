using Microsoft.AspNetCore.Mvc;
using MyChat.Models;

namespace MyChat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly List<Message> _messages = new List<Message>();

        [HttpGet]
        public ActionResult<IEnumerable<Message>> GetMessages()
        {
            return Ok(_messages);
        }

        [HttpGet("{id}")]
        public ActionResult<Message> GetMessage(int id)
        {
            var message = _messages.FirstOrDefault(m => m.Id == id);

            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }

        [HttpPost]
        public ActionResult<Message> AddMessage([FromBody] Message newMessage)
        {
            newMessage.Id = _messages.Count + 1;
            newMessage.Timestamp = DateTime.Now;
            _messages.Add(newMessage);
            return CreatedAtAction(nameof(GetMessage), new { id = newMessage.Id }, newMessage);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteMessage(int id)
        {
            var messageToRemove = _messages.FirstOrDefault(m => m.Id == id);

            if (messageToRemove == null)
            {
                return NotFound();
            }

            _messages.Remove(messageToRemove);

            return NoContent();
        }
    }
}
