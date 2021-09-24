using System.Collections.Generic;
using System;
using ToolBox.Services;
using UnityEngine;

namespace ToolBox.Injection
{
    class ServiceBootstrapper
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void StartBootstrappedServices()
        {
            var types = TypeHelper.GetAllTypesThatInherit<IBootstrapService>();

            if (types.NullOrNoElements())
                return;
            
            GlobalInjector.Injector.LoadTypes(types);
        } 
    }

    class ServiceProvider : DependencyInstanceProvider
    {
        public ServiceProvider()
        {
            m_BehaviorService = GenericObjectFactory.CreateObject<GlobalBehaviourService>();
        }

        private Dictionary<Type, IService> _Cache = new Dictionary<Type, IService>();
        private GlobalBehaviourService m_BehaviorService;

        override protected bool RequestType(Type type, out object result)
        {
            if (!typeof(IService).IsAssignableFrom(type))
            {
                result = null;
                return false;
            }

            if (_Cache.TryGetValue(type, out IService service))
            {
                result = service;
                return true;
            }

            if (typeof(MonoBehaviour).IsAssignableFrom(type))
            {
                result = m_BehaviorService.GetBehavior(type);
                if (result != null)
                    _Cache.Add(type, (IService)result);

                return true;
            }

            result = GenericObjectFactory.CreateObject(type, currentInjector);
            _Cache.Add(type, (IService)result);

            return true;
        }
    }
}
