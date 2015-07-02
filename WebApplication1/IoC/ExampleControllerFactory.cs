using System;
using System.Web.Mvc;

using InversionControl;

namespace WebApplication1.IoC
{
    public class ExampleControllerFactory : DefaultControllerFactory
    {
        private readonly IContainer container;

        public ExampleControllerFactory(IContainer container)
        {
            this.container = container;
        }

        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return container.Resolve(controllerType) as Controller;
        }
    }
}