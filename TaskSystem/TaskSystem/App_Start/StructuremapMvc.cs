using System.Web.Mvc;
using StructureMap;
using TaskSystem.DependencyResolution;

[assembly: WebActivator.PreApplicationStartMethod(typeof(TaskSystem.App_Start.StructuremapMvc), "Start")]

namespace TaskSystem.App_Start {
    public static class StructuremapMvc {
        public static void Start() {
            var container = (IContainer) IoC.Initialize();
            DependencyResolver.SetResolver(new SmDependencyResolver(container));
        }
    }
}