using ToolBox.Injection;
using ToolBox.Services;
using UnityEngine.EventSystems;

namespace ToolBox.Input
{
    internal class ToolboxInputProvider : BaseInput, IService
    {
        [Dependency] InputService inputSystem = null;

        public override bool touchSupported => false;
        public override bool mousePresent => false;

        protected override void Awake()
        {
            GlobalInjector.Injector.InjectDependencies(this);
            base.Awake();
        }

        public override float GetAxisRaw(string axisName)
        {
            float raw = GetAxisRawImpl(axisName);

            //UnityEngine.Debug.Log($"{axisName} : {raw}");

            return raw;
        }

        private float GetAxisRawImpl(string axisName)
        {
            switch (axisName)
            {
                case "Horizontal": return inputSystem.Get(0,"Right") - inputSystem.Get(0,"Left");
                case "Vertical": return inputSystem.Get(0,"Up") - inputSystem.Get(0,"Down");
            }

            return inputSystem.Get(0,axisName);
        }

        public override bool GetButtonDown(string buttonName)
        {
            return inputSystem.GetDown(buttonName);
        }
    }
}
