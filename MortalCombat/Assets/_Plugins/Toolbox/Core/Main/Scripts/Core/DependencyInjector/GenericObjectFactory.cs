using System;
using UnityEngine;

namespace ToolBox.Injection
{
    public class GenericObjectFactory
    {
        public static T CreateObject<T>()
        {
            return CreateObject<T>(typeof(T), null);
        }

        public static T CreateObject<T>(Type type, string jsonData = null)
        {
            return (T)CreateObject(type, null, jsonData);
        }

        public static object CreateObject(string typeName, Injector injector, string jsonData = null)
        {
            Type type = TypeHelper.FindTypeByFullName(typeName);
            return CreateObject(type, injector, jsonData);
        }

        public static T CreateObject<T>(Injector injector)
        {
            T instance = Activator.CreateInstance<T>();
            injector.InjectDependencies(instance);
            return instance;
        }

        public static object CreateObject(Type type, Injector injector, string jsonData = null)
        {
            object instance;

            if (injector != null)
            {
                var constructors = type.GetConstructors();
                if (constructors.Length > 1)
                    throw new Exception($"Type {type.Name} has multiple constructors. Please make sure that this type only has a single constructor.");
                var constructorToUse = constructors[0];

                var constructorParameters = constructorToUse.GetParameters();
                if (constructorParameters.Length > 0)
                {
                    object[] parameters = injector.FetchInstances(constructorParameters);
                    return Activator.CreateInstance(type, parameters);
                }
            }

            if (string.IsNullOrEmpty(jsonData))
            {
                instance = Activator.CreateInstance(type);
            }
            else
            {
                try
                {
                    instance = JsonUtility.FromJson(jsonData, type);
                }
                catch (Exception)
                {
                    instance = Activator.CreateInstance(type);
                }
            }

            if (injector != null)
                injector.InjectDependencies(instance);

            return instance;
        }
    }
}
