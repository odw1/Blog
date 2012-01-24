using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Web.Controllers;
using Web.Plumbing;

namespace Web.Installers
{
    public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(AllTypes.FromThisAssembly()
                                   .BasedOn<IController>()
                                   .If(Component.IsInSameNamespaceAs<HomeController>())
                                   .If(t => t.Name.EndsWith("Controller"))
                                   .Configure(c => c.LifestyleTransient())
                                   .ConfigureIf(
                                       c => c.Implementation.IsDefined(typeof (RequiresAuditAttribute), false),
                                       c => c.Interceptors<AuditInterceptor>()));
        }
    }
}