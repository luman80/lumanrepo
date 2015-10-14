using Owin;
using Microsoft.Owin;
[assembly: OwinStartup(typeof(sba1.App_Start.Startup))]

namespace sba1.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}