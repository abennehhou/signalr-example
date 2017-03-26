$(function () {

    var ticker = $.connection.QuoteTickerMini, // the generated client-side hub proxy
        $quoteText = $('#quoteText'),
        $quoteOwner = $('#quoteOwner');

    function init() {
        ticker.server.getCurrentQuote().done(function (quote) {
            setQuote(quote);
        });
    }

    // Add a client-side hub method that the server will call
    ticker.client.updateCurrentQuote = function (quote) {
        setQuote(quote);
    }

    function setQuote(quote) {
        $quoteText.html(quote.Text);
        $quoteOwner.html(quote.Owner);
    }

    // Start the connection
    $.connection.hub.start().done(init);

});
