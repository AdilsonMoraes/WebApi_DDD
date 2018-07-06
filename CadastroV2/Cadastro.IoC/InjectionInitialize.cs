using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using SimpleInjector.Integration.WebApi;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;

namespace Cadastro.IoC
{
    public static class InjectionInitialize
    {
        public static Container Initialize(HttpConfiguration configuration)
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterWebApiControllers(configuration);
            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            return container;
        }

        private static void InitializeContainer(Container container)
        {
            BootStrap.RegistrarServico(container);
        }

    }
}
