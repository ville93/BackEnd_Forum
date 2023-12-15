namespace MyChat.Models
{
    public class Channel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Discussion> Discussions { get; set; } = new List<Discussion>();
    }
}
