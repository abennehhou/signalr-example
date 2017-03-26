using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalR.Example
{
    public class QuoteTicker
    {
        private readonly static Lazy<QuoteTicker> _instance = new Lazy<QuoteTicker>(() => new QuoteTicker(GlobalHost.ConnectionManager.GetHubContext<QuoteTickerHub>().Clients));

        private readonly List<Quote> _quotes = new List<Quote>();
        private readonly Quote _currentQuote = new Quote();

        private readonly object _updateCurrentQuoteLock = new object();

        private readonly TimeSpan _updateInterval = TimeSpan.FromSeconds(5);
        private readonly Random _random = new Random();

        private readonly Timer _timer;
        private volatile bool _updatingQuotes = false;

        private QuoteTicker(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
            _quotes = InitializeQuotes();
            _currentQuote = _quotes[0];
            _timer = new Timer(UpdateCurrentQuote, null, _updateInterval, _updateInterval);
        }

        public static QuoteTicker Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        private IHubConnectionContext<dynamic> Clients
        {
            get;
            set;
        }

        public Quote GetCurrentQuote()
        {
            return _currentQuote;
        }

        private void UpdateCurrentQuote(object state)
        {
            lock (_updateCurrentQuoteLock)
            {
                if (!_updatingQuotes)
                {
                    _updatingQuotes = true;
                    int r = _random.Next(_quotes.Count);
                    var currentQuote = _quotes[r];

                    BroadcastCurrentQuote(currentQuote);
                }
            }

            _updatingQuotes = false;
        }


        private void BroadcastCurrentQuote(Quote quote)
        {
            Clients.All.updateCurrentQuote(quote);
        }

        private List<Quote> InitializeQuotes()
        {
            return new List<Quote>
            {
                new Quote
                {
                    Text = "Do what you feel in your heart to be right, for you’ll be criticized anyway.",
                    Owner = "Eleanor Roosevelt"
                },
                new Quote
                {
                    Text = "Whenever you find yourself on the side of the majority, it is time to pause and reflect.",
                    Owner = "Mark Twain"
                },
                new Quote
                {
                    Text = "Two things are infinite: the universe and human stupidity; and I'm not sure about the universe.",
                    Owner = "Albert Einstein"
                },
                new Quote
                {
                    Text = "The best argument against democracy is a five-minute conversation with the average voter.",
                    Owner = "Winston Churchill"
                },
                new Quote
                {
                    Text = "The saddest aspect of life right now is that science gathers knowledge faster than society gathers wisdom.",
                    Owner = "Isaac Asimov"
                }
            };
        }
    }
}
