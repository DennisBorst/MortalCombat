using System.Collections;
using System.Collections.Generic;
using ToolBox.Injection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ToolBox.Input.Editor
{
    class InputSettings : SettingsProvider
    {

        [Dependency] private InputBindingManager _Manager;
        private SerializedObject _Settings;

        public InputSettings(string path, SettingsScope scope = SettingsScope.Project): base(path, scope) 
        { 
        
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            GlobalInjector.Injector.InjectDependencies(this);
            _Settings = new SerializedObject(_Manager.LoadAsset());
        }

        public override void OnGUI(string searchContext)
        {
            EditorGUILayout.PropertyField(_Settings.FindProperty("bindings"));
        }

        [SettingsProvider]
        public static SettingsProvider CreateMyCustomSettingsProvider()
        {
            var provider = new InputSettings("Project/ToolBox/Input", SettingsScope.Project);
            //provider.keywords = GetSearchKeywordsFromGUIContentProperties<Styles>();
            return provider;
        }
    }
}
