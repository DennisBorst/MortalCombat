using ToolBox.Injection;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ToolBox.Input
{
    public class ToolBoxInputModule : StandaloneInputModule
    {
        [Dependency] ToolboxInputProvider toolboxInput;
        public GameObject m_TargetObject;

        protected override void Awake()
        {
            GlobalInjector.Injector.InjectDependencies(this);
            m_InputOverride = toolboxInput;
            base.Awake();
        }
    }
}
