using System.Web.Http;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using WebApp.BusinessLayer;
using WebApp.DataAccess;

namespace WebApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IRepository , Repository>();
            container.RegisterType<IServices, Services>();

            // Configures container for ASP.NET MVC
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            // Configures container for WebAPI
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}