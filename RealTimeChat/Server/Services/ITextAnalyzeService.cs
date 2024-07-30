using Azure.AI.TextAnalytics;

namespace RealTimeChat.Server.Services
{
    public interface ITextAnalyzeService
    {
        Task<Sentiment?> GetAnalyzeSentimentResult(string document,string language="en");
    }
}