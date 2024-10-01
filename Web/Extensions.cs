namespace Web;

public static class Extensions
{
    /// <summary>
    /// This method will scan all assemblies for classes and add all classes that belong to provided namespace to dependency injection container as service with provided lifetime bound to itself. 
    /// </summary>
    /// <param name="serviceCollection">Service collection</param>
    /// <param name="namespaceName">Name of namespace</param>
    /// <param name="lifetime">Service Lifetime</param>
    public static void AddFromNamespace(this IServiceCollection serviceCollection,
        string namespaceName, ServiceLifetime lifetime)
    {
        serviceCollection.Scan(selector =>
        {
            selector.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddClasses(implementation =>
                    implementation.Where(type => type.IsClass && type.Namespace == namespaceName))
                .As(type => new[] { type })
                .WithLifetime(lifetime);
        });
    }
    
    /// <summary>
    /// This method will scan all assemblies for classes and add all classes that belong to provided namespace to dependency injection container as scoped service bound to itself. 
    /// </summary>
    /// <param name="serviceCollection">Service collection</param>
    /// <param name="namespaceName">Name of namespace</param>
    public static void AddScopedFromNamespace(this IServiceCollection serviceCollection,
        string namespaceName)
    {
        serviceCollection.AddFromNamespace(namespaceName, ServiceLifetime.Scoped);
    }
    
    /// <summary>
    /// This method will scan all assemblies for classes and add all classes that belong to provided namespace to dependency injection container as singleton service bound to itself. 
    /// </summary>
    /// <param name="serviceCollection">Service collection</param>
    /// <param name="namespaceName">Name of namespace</param>
    public static void AddSingletonFromNamespace(this IServiceCollection serviceCollection,
        string namespaceName)
    {
        serviceCollection.AddFromNamespace(namespaceName, ServiceLifetime.Singleton);
    }
    
    /// <summary>
    /// This method will scan all assemblies for classes and add all classes that belong to provided namespace to dependency injection container as transient service bound to itself. 
    /// </summary>
    /// <param name="serviceCollection">Service collection</param>
    /// <param name="namespaceName">Name of namespace</param>
    public static void AddTransientFromNamespace(this IServiceCollection serviceCollection,
        string namespaceName)
    {
        serviceCollection.AddFromNamespace(namespaceName, ServiceLifetime.Transient);
    }
}