using System.Collections.Generic;
using ToolBox.Services;
using UnityEngine;

namespace ToolBox.Input
{
    public class InputBindingManager : IService
    {
        private List<InputBinding> bindings = null;

        public void Load()
        {
            InputBindings savedInputBindings = Resources.Load<InputBindings>("InputBindings");
            bindings = new List<InputBinding>(savedInputBindings.bindings);
        }

        public InputBinding[] GetAllBindings()
        {
            if (bindings == null)
                Load();

            return bindings.ToArray();
        }
    }
}

