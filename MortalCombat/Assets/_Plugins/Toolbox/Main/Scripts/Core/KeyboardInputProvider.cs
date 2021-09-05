
using System;
using UnityEngine;

namespace ToolBox.Input
{
    public class KeyboardInputProvider : BaseInputProvider
    {
        public override string Name => "Keyboard";
        public override string[] AvailableInputs => Enum.GetNames(typeof(KeyCode));
        public override Action<int> OnDisconnect => null;
        public override Action<int> OnConnect => null;

        public override float GetValue(InputBinding binding)
        {
            if (Enum.TryParse(binding.DeviceInputName, out KeyCode code))
            {
                return UnityEngine.Input.GetKey(code) ? 1f : 0f;
            }
            else
            {
                UnityEngine.Debug.LogWarning($"[{typeof(KeyboardInputProvider)}] Couldn't convert DeviceInputName'{binding.DeviceInputName}' to enum type '{nameof(KeyCode)}'. Returning 0.");
                return 0;
            }
        }
    }
}

