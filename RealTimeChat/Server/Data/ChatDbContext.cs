using Microsoft.EntityFrameworkCore;
using RealTimeChat.Shared;


namespace RealTimeChat.Server.Data
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions options) : base(options)
        {
                    }
        public DbSet<ChatMessage> ChatMessages { get; set; }
    }
}
