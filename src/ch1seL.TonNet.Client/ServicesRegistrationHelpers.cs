using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ch1seL.TonNet.Client
{
    internal static class ServicesRegistrationHelpers
    {
        internal static IServiceCollection AddServicesAsTransient(this IServiceCollection services, Type inheritableInterfaceType,
            IEnumerable<Assembly> assemblies)
        {
            IReadOnlyList<Type> registrationTypes =
                assemblies
                    .SelectMany(a => a.GetTypes())
                    .Where(t => inheritableInterfaceType.IsAssignableFrom(t) && t != inheritableInterfaceType)
                    .ToArray();

            var concretesAndInterfaces = registrationTypes.Where(t => !t.IsAbstract)
                .Select(t => new
                {
                    type = t,
                    interfaces = registrationTypes.Where(i => i.IsInterface
                                                              && (i.IsAssignableFrom(t)
                                                                  || t.IsGenericTypeDefinition && i.IsGenericType &&
                                                                  i.MakeGenericType(typeof(object)).IsAssignableFrom(t.MakeGenericType(typeof(object)))))
                });

            foreach (var concreteAndInterfaces in concretesAndInterfaces)
            {
                foreach (Type @interface in concreteAndInterfaces.interfaces) services.AddTransient(@interface, concreteAndInterfaces.type);
                if (!concreteAndInterfaces.interfaces.Any()) services.AddTransient(concreteAndInterfaces.type, concreteAndInterfaces.type);
            }

            return services;
        }
    }
}