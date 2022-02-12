using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ToolBox.Services
{
	/// <summary>
	/// System service, please do not touch
	/// </summary>
    public class GlobalBehaviourService : IService
	{
		public GameObject _GameObject;

        public GlobalBehaviourService()
        {
            _GameObject = new GameObject("Toolbox Service Behaviors");

#if UNITY_EDITOR
			if (EditorApplication.isPlaying)
				Object.DontDestroyOnLoad(_GameObject);
            _GameObject.hideFlags = HideFlags.DontSave;
            _GameObject.isStatic = true;
#else
			Object.DontDestroyOnLoad(_GameObject);
#endif
		}

		public object GetBehavior(Type type)
        {
			if (_GameObject.TryGetComponent(type, out Component component))
				return component;

			return _GameObject.AddComponent(type);
        }

		public T GetBehavior<T>() where T : MonoBehaviour, new()
		{
			return (T)GetBehavior(typeof(T));
		}
	}
}
