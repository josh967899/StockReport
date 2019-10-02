using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StockReport.Startup))]
namespace StockReport
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            Function.CreateRoot.Root.CreateAllRoot();
           

        }
    }
}
