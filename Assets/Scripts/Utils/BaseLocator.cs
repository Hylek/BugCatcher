using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils
{
    /// <summary>
    /// Implements the Locator pattern so all service classes can be placed under one roof.
    /// </summary>
    public abstract class BaseLocator
    {
        private static readonly Dictionary<Type, object> Services = new();

        public static T Find<T>()
        {
            try
            {
                return (T)Services[typeof(T)];
            }
            catch
            {
                throw new ApplicationException("The requested service could not be found!");
            }
        }
        
        public static T Add<T>(object service)
        {
            var type = typeof(T);

            if (DoesServiceExist(type))
            {
                return Find<T>();
            }
            
            Services.Add(type, service);

            return (T)service;
        }

        public static void Remove<T>()
        {
            Services[typeof(T)] = null;
            Services.Remove(typeof(T));
        }

        public static bool DoesServiceExist(Type type) => Services.Any(s => s.Key == type);

        public static void RemoveAllServices()
        {
            // Use a for loop as foreach value would be immutable.
            for (var i = Services.Count - 1; i >= 0; i--)
            {
                var key = Services.Keys.ElementAt(i);
                Services[key] = null;
            }
            Services.Clear();
        }
    }
}