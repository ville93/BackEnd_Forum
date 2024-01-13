using System.ComponentModel.DataAnnotations.Schema;

namespace MyChat.Models
{
    public class Discussion
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Message> Messages { get; set; }
        public int ChannelId { get; set; }
        public DateTime CreatedAt { get; set; }

        [NotMapped]
        public int AnswersCount { get; set; }
    }
}
