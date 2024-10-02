using Microsoft.Extensions.DependencyInjection;

namespace Application.DependencyInjectionExtensions;

/// <summary>
/// Marks class for later automatic registering to dependency injection container as all interfaces annotated class implements and as itself.
/// You don't have to register annotated class manually.
/// </summary>
/// <param name="lifetime">Service lifetime. Defaults to scoped</param>
[AttributeUsage(AttributeTargets.Class)]
public class ServiceAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped): Attribute
{
    public ServiceLifetime Lifetime { get; } = lifetime;
}