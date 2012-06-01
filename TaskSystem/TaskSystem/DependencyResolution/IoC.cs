using StructureMap;
using TaskSystem.Data.Context;

namespace TaskSystem.DependencyResolution
{
    public static class IoC
    {
        public static IContainer Initialize()
        {
            ObjectFactory.Initialize(x =>
                        {
                            x.Scan(scan =>
                                    {
                                        scan.TheCallingAssembly();
                                        scan.ExcludeType<ITaskContext>();
                                        scan.WithDefaultConventions();
                                    });
                            //                x.For<IExample>().Use<Example>();
                            x.For<ITaskContext>().HttpContextScoped().Use(() => new TaskContext("TaskWebsite"));
                        });



            return ObjectFactory.Container;
        }
    }
}