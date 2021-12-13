using AutoMapper;
using System.Web.Http;
using Tasks.Application.Mappers;
using Tasks.Application.Repositories;
using Tasks.Application.Services;
using Tasks.Infrastructure;
using Tasks.Infrastructure.Repositories;
using Unity;
using Unity.Injection;
using Unity.WebApi;

namespace Tasks
{
    public static class UnityConfig
    {
        private static IUnityContainer container = new UnityContainer();

        public static void RegisterComponents()
        {
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<ApplicationContext>();
            container.RegisterType<IMapper, Mapper>(
                new InjectionConstructor(new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile(new TaskProfile());
                }))
            );
            container.RegisterType<TaskService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }

        public static IUnityContainer GetConfiguredContainer()
        {
            return container;
        }
    }
}