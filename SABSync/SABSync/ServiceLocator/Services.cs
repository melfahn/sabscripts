using System;
using System.Collections.Generic;

namespace SABSync.ServiceLocator
{
    static class Services
    {

        private static readonly Dictionary<Type, Object> Container = new Dictionary<Type, object>();

        private static readonly object Lock = new object();

        /// <summary>
        /// Registers an instance of a service based on it's type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        public static void Register<T>(T instance)
        {
            //Lock to avoid duplicate adds
            lock (Lock)
            {
                if (Container.ContainsKey(typeof(T)))
                {
                    Container.Remove(typeof(T));
                    Container.Add(typeof(T), instance);
                }
            }
        }



        /// <summary>
        /// Resolves a service based on its type out of the dictionary.
        /// If type is not previously registered a new instance will be created and returned.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Resolve<T>() where T : new()
        {
            object result;
            Container.TryGetValue(typeof(T), out result);

            if (result != null)
            {
                return (T)result;
            }

            return new T();
        }


    }
}
