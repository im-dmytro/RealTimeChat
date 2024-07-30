using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeChat.Shared
{
    public class ChatMessage
    {
        public long Id { get; set; }
        public string FromUserName { get; set; } = null!;
        public string Message { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public string? SentimentResult { get; set; }
    }
}
