namespace RealTimeChat.Server.Services
{
    public class SentimentResult
    {
        public Sentiment Value { get; set; }
    }

    public enum Sentiment
    {
        Positive = 0, Negative = 1, Neutral = 2
    }
}
