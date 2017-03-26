using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalR.Example
{
    [HubName("QuoteTickerMini")]
    public class QuoteTickerHub : Hub
    {
        private readonly QuoteTicker _quoteTicker;

        public QuoteTickerHub() : this(QuoteTicker.Instance) { }

        public QuoteTickerHub(QuoteTicker quoteTicker)
        {
            _quoteTicker = quoteTicker;
        }

        public Quote GetCurrentQuote()
        {
            return _quoteTicker.GetCurrentQuote();
        }

        public int GetQuotesNumber()
        {
            return _quoteTicker.GetQuotesNumber();
        }

        public void AddQuote(string owner, string text)
        {
            _quoteTicker.AddQuote(owner, text);
        }
    }
}
