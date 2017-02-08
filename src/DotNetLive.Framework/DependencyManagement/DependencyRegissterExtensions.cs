using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DotNetLive.Framework.DependencyManagement
{
    public static class DependencyRegissterExtensions
    {
        private static IConfigurationRoot _configuration;
        public static void AddDependencyRegister(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddDependencyRegister(configuration, DependencyContext.Default);
        }
        public static void AddDependencyRegister(this IServiceCollection services, IConfigurationRoot configuration, params Assembly[] assemblies)
        {
            _configuration = configuration;
            AddDependencyRegisterClass(services, assemblies);
        }

        public static void AddDependencyRegister(this IServiceCollection services, IConfigurationRoot configuration, DependencyContext dependencyContext)
        {
            services.AddDependencyRegister(configuration,
                dependencyContext.RuntimeLibraries
                    // Only load assemblies that reference AutoMapper
                    //.Where(lib =>
                    //     //lib.Type.Equals("msbuildproject", StringComparison.OrdinalIgnoreCase)
                    //     lib.Dependencies.Any(d => d.Name.StartsWith("DotNetLive", StringComparison.OrdinalIgnoreCase))
                    //    )
                    .SelectMany(lib => lib.GetDefaultAssemblyNames(dependencyContext).Select(Assembly.Load))
                    .Where(x => x.FullName.StartsWith("DotNetLive"))
                    .ToArray());
        }

        private static void AddDependencyRegisterClass(IServiceCollection services, IEnumerable<Assembly> assembliesToScan)
        {
            assembliesToScan = assembliesToScan as Assembly[] ?? assembliesToScan.ToArray();

            var allTypes = assembliesToScan.SelectMany(a => a.ExportedTypes).ToArray();

            var dependencyRegisters =
                allTypes
                .Where(t => !t.GetTypeInfo().IsAbstract && t.GetInterfaces().SingleOrDefault(x => x == typeof(IDependencyRegister)) != null)
                .ToList();

            var serviceProvider = services.BuildServiceProvider();
            foreach (var profile in dependencyRegisters)
            {
                var registerInstance = (IDependencyRegister)serviceProvider.TryGetService(profile);
                var registerMethod = registerInstance.GetType().GetMethod("Register");
                var methodParameters = registerMethod.GetParameters().Select(parm => serviceProvider.GetService(parm.ParameterType)).ToArray();
                registerMethod.Invoke(registerInstance, methodParameters);
            }
        }

        public static object TryGetService(this IServiceProvider serviceProvider, Type type)
        {
            var constructors = type.GetConstructors();
            foreach (var constructor in constructors)
            {
                try
                {
                    var parameters = constructor.GetParameters();
                    var parameterInstances = new List<object>();
                    foreach (var parameter in parameters)
                    {
                        var service = serviceProvider.GetService(parameter.ParameterType);
                        if (service == null) throw new Exception("Unkown dependency");
                        parameterInstances.Add(service);
                    }
                    return Activator.CreateInstance(type, parameterInstances.ToArray());
                }
                catch (Exception)
                {

                }
            }
            return null;
        }

        private static bool ImplementsGenericInterface(this Type type, Type interfaceType)
        {
            if (type.IsGenericType(interfaceType))
            {
                return true;
            }
            foreach (var @interface in type.GetTypeInfo().ImplementedInterfaces)
            {
                if (@interface.IsGenericType(interfaceType))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsGenericType(this Type type, Type genericType)
        {
            return type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == genericType;
        }

    }
}
