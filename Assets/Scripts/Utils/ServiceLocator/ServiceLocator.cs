using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Asteroids.Utils
{
    public sealed class ServiceLocator
    {
        private readonly Dictionary<string, IGameService> services = new Dictionary<string, IGameService>();

        public static ServiceLocator Instance { get; private set; }
        public static void Initialize() => Instance ??= new ServiceLocator();

        public T Get<T>() where T : IGameService
        {
            string serviceName = typeof(T).Name;
            if (!services.ContainsKey(serviceName))
            {
                Debug.LogError($"{serviceName} not registered");
                throw new InvalidOperationException();
            }

            return (T)services[serviceName];
        }

        public void Register<T>(T service) where T : IGameService
        {
            string serviceName = typeof(T).Name;
            if (services.ContainsKey(serviceName))
            {
                Debug.LogError($"{serviceName} is already registered");
                return;
            }

            services.Add(serviceName, service);
        }

        public List<IGameService> GetAllRegisteredServices()
        {
            return services.Values.ToList();
        }
    }
}