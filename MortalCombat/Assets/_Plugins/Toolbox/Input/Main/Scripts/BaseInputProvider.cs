
using System;

namespace ToolBox.Input
{
    public abstract class BaseInputProvider
    {
        public abstract string Name { get; }
        public abstract string[] AvailableInputs { get; }

        public abstract Action<int> OnDisconnect { get; }
        public abstract Action<int> OnConnect { get; }

        public abstract float GetValue(InputBinding binding);
        public virtual void Update() { }

        public float DeadZone(float value, float deadzone)
        {
            return value < deadzone ? 0 : value;
        }
    }
}

