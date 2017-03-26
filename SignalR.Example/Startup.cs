using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SignalR.Example.Startup))]
namespace SignalR.Example
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}
