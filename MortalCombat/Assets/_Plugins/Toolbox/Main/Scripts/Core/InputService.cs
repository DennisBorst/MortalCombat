using ToolBox.Services;
using System.Linq;
using UnityEngine;
using ToolBox.Injection;
using System.Collections.Generic;

namespace ToolBox.Input
{
    [DefaultExecutionOrder(-100)]
    public class InputService : DependencyBehavior, IService
    {
        BaseInputProvider[] inputProviders = new BaseInputProvider[]
        {
            new KeyboardInputProvider(),
            new UnityControllerInput(),
        };

        InputBinding[] bindings = DefaultBindings.bindings;

        Dictionary<string, float> previousValues = new Dictionary<string, float>();

        public void LateUpdate()
        {
            var inputbindings = bindings.GroupBy(x => x.Name + x.ControlScheme + x.Player);

            foreach (var item in inputbindings)
            {
                var binding = item.First();
                previousValues[binding.Name + binding.Player] = 
                    Get(binding.Player, binding.Name);
            }
        }

        public float Get(string inputName)
        {
            return
                Mathf.Max(Get(0, inputName),
                Get(1, inputName),
                Get(2, inputName),
                Get(3, inputName));
                
        }

        public float Get(int player, string inputName)
        {
            IEnumerable<InputBinding> applicableBindings = GetApplicableBindings(player, inputName);

            float max = 0;
            foreach (var item in applicableBindings)
            {
                if (inputProviders.TryFindFirst(x => x.Name == item.DeviceName
                    && x.AvailableInputs.Contains(item.DeviceInputName),
                    out BaseInputProvider provider))
                {

                    float value = provider.GetValue(item);

                    if (item.Log)
                        UnityEngine.Debug.Log($"[{nameof(InputService)}] {item.Name} - {item.DeviceInputName}: {value}");

                    max = Mathf.Max(max, value);
                }
            }

            return max;
        }

        private IEnumerable<InputBinding> GetApplicableBindings(int player, string inputName)
        {
            return bindings.Where(x => x.Name == inputName
                && x.Player == player);
        }

        public bool GetUp(int player, string controlScheme, string inputName)
        {
            if (previousValues.TryGetValue(inputName + player, out float value))
            {
                return value > 0.5 && Get(player, inputName) < 0.5f;
            }

            return false;
        }

        public bool GetDown(string inputName)
        {
            return Down(0, inputName)
                || Down(1, inputName)
                || Down(2, inputName)
                || Down(3, inputName);
        }

        public bool Down(int player, string inputName)
        {
            if (previousValues.TryGetValue(inputName + player, out float value))
            {
                return value < 0.5 && Get(player, inputName) > 0.5f;
            }

            return false;
        }
    }
}

