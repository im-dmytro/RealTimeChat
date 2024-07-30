using Azure.AI.TextAnalytics;

namespace RealTimeChat.Server.Services
{
    public class TextAnalyzeService : ITextAnalyzeService
    {
        TextAnalyticsClient textAnalyticsClient;
        public TextAnalyzeService(TextAnalyticsClient textAnalyticsClient)
        {
            this.textAnalyticsClient = textAnalyticsClient;
        }
        public async Task<Sentiment?> GetAnalyzeSentimentResult(string document, string language = "en")
        {
            var result = await textAnalyticsClient.AnalyzeSentimentAsync(document, language);
            var sentiment = result?.Value?.Sentiment;
            if (sentiment == null)
            {
                return null;
            }

            switch (sentiment)
            {
                case TextSentiment.Positive:
                    return Sentiment.Positive;
                case TextSentiment.Negative:
                    return Sentiment.Negative;
                default:
                    return Sentiment.Neutral;
            }
        }

    }
}
