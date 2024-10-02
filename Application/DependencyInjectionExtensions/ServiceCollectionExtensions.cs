using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjectionExtensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// This method adds each class that has <see cref="ServiceAttribute"/> from all assemblies with specified
    /// in attribute scope to container as each interface this class implements and as itself.
    /// </summary>
    /// <param name="serviceCollection">Service collection</param>
    public static void AddAllWithServiceAttribute(this IServiceCollection serviceCollection)
    {
        serviceCollection.Scan(selector =>
        {
            selector.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                //register scoped services
                .AddClasses(implementation =>
                {
                    implementation.Where(type =>
                    {
                        var attribute = Attribute.GetCustomAttributes(type)
                            .FirstOrDefault(attribute => attribute is ServiceAttribute, null) as ServiceAttribute;
                        if (attribute is null)
                        {
                            return false;
                        }

                        return attribute.Lifetime == ServiceLifetime.Scoped;
                    });
                })
                .AsImplementedInterfaces()
                .AsSelf()
                .WithScopedLifetime()
                //register singleton services
                .AddClasses(implementation =>
                {
                    implementation.Where(type =>
                    {
                        var attribute = Attribute.GetCustomAttributes(type)
                            .FirstOrDefault(attribute => attribute is ServiceAttribute, null) as ServiceAttribute;
                        if (attribute is null)
                        {
                            return false;
                        }

                        return attribute.Lifetime == ServiceLifetime.Singleton;
                    });
                })
                .AsImplementedInterfaces()
                .AsSelf()
                .WithSingletonLifetime()
                //register transient services
                .AddClasses(implementation =>
                {
                    implementation.Where(type =>
                    {
                        var attribute = Attribute.GetCustomAttributes(type)
                            .FirstOrDefault(attribute => attribute is ServiceAttribute, null) as ServiceAttribute;
                        if (attribute is null)
                        {
                            return false;
                        }

                        return attribute.Lifetime == ServiceLifetime.Transient;
                    });
                })
                .AsImplementedInterfaces()
                .AsSelf()
                .WithTransientLifetime();
        });
    }
}