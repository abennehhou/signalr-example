$(function () {

    var ticker        = $.connection.QuoteTickerMini, // the generated client-side hub proxy
        $quoteText    = $('#quote-text'),
        $quoteOwner   = $('#quote-owner'),
        $quotesNumber = $('#quotes-number'),
        $displayName  = $('#user-name'),
        $userQuote    = $('#user-quote'),
        $sendMessage  = $('#send-message-btn');

    function init() {
        $displayName.val(prompt('Enter your name:', '') || 'Anonymous');
        ticker.server.getCurrentQuote().done(function (quote) {
            setQuote(quote);
        });

        ticker.server.getQuotesNumber().done(function (number) {
            setQuotesNumber(number);
        });

        $sendMessage.click(function () {
            var quoteText = $userQuote.val();
            var owner = $displayName.val();
            if (quoteText) {
                // Call the Send method on the hub. 
                ticker.server.addQuote(owner, quoteText);
                // Clear text box. 
                $userQuote.val('');
            }
        });
    }

    // Add a client-side hub method that the server will call
    ticker.client.updateCurrentQuote = function (quote) {
        setQuote(quote);
    };

    ticker.client.updateQuotesNumber = function (number) {
        setQuotesNumber(number);
    };

    function setQuote(quote) {
        $quoteText.html(quote.Text);
        $quoteOwner.html(quote.Owner);
    }

    function setQuotesNumber(number) {
        $quotesNumber.html(number);
    }

    // Start the connection
    $.connection.hub.start().done(init);

});
