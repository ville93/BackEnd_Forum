﻿namespace MyChat.Models
{
    public class Discussion
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Message> Messages { get; set; }
        public int ChannelId { get; set; }
    }
}