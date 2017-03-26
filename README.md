# signalr-example
Example of an application using SignalR for bidirectional communication between server and client.

##### About this example
* This example consists on a simple page containing:
    * A title with the number of available quotes
    * A section with a random quote
    * A section to add a quote
* The same random quote is displayed for all the _clients_, it is broadcasted by the _server_.
* A _client_ can add a quote. In this case, the number of quotes is instantly incremented for all the _clients_ and can be displayed, randomly.
