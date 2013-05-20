using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;
using SignaleRDemo.Hubs;

namespace SignaleRDemo
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            new TaskFactory().StartNew(() =>
            {
                while (true)
                {
                    var context = GlobalHost.ConnectionManager.GetHubContext<DemoHub>();
                    context.Clients.All.ping("from code", DateTime.Now.ToLongTimeString());
                    Thread.Sleep(1000);
                }

            });

            new TaskFactory().StartNew(() =>
            {
                while (true)
                {
                    var context = GlobalHost.ConnectionManager.GetHubContext<DemoHub>();
                    var sensorId = new Random(DateTime.Now.Millisecond).Next(9) + 1;
                    context.Clients.All.sensor(sensorId);
                    Thread.Sleep(1000);
                }

            });

        }
    }
}