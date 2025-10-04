using System;
using System.Collections.Generic;

namespace DuelArena.Infrastructure
{
    /// <summary>
    /// Provides a simple service locator for registering and resolving dependencies.
    /// </summary>
    public sealed class ServiceRegistry
    {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        /// <summary>
        /// Registers a concrete instance for the given service type.
        /// </summary>
        /// <typeparam name="TService">The service contract type.</typeparam>
        /// <param name="instance">The service implementation instance.</param>
        public void Register<TService>(TService instance)
            where TService : class
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            _services[typeof(TService)] = instance;
        }

        /// <summary>
        /// Attempts to resolve a service from the registry.
        /// </summary>
        /// <typeparam name="TService">The requested service type.</typeparam>
        /// <param name="service">The resolved service instance when available.</param>
        /// <returns><c>true</c> if the service was found; otherwise <c>false</c>.</returns>
        public bool TryResolve<TService>(out TService service)
            where TService : class
        {
            if (_services.TryGetValue(typeof(TService), out var instance))
            {
                service = (TService)instance;
                return true;
            }

            service = null;
            return false;
        }

        /// <summary>
        /// Resolves a service or throws an exception when it is missing.
        /// </summary>
        /// <typeparam name="TService">The requested service type.</typeparam>
        /// <returns>The registered service instance.</returns>
        public TService Resolve<TService>()
            where TService : class
        {
            if (TryResolve<TService>(out var service))
            {
                return service;
            }

            throw new InvalidOperationException($"Service of type {typeof(TService).Name} is not registered.");
        }
    }
}
