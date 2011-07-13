using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace SuperSchnell.Project.Mvc.Bootstrapping
{
    public class CustomControllerActivator : IControllerActivator
    {
        IController IControllerActivator.Create(RequestContext requestContext, Type controllerType)
        {
            return DependencyResolver.Current.GetService(controllerType) as IController;
        }
    }
}