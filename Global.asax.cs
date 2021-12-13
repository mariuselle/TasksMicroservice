using System;
using System.Web;
using System.Web.Http;
using Tasks;
using Tasks.Application.AsyncServices;
using Tasks.Application.Services;
using Unity;

namespace MyTestApp
{
    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            UnityConfig.RegisterComponents();
            GlobalConfiguration.Configure(WebApiConfig.Register);

            MessageBusSubscriber.Subscribe(UnityConfig.GetConfiguredContainer());
        }
    }
}