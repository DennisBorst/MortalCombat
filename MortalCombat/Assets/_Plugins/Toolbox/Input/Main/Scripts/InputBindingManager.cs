using System.Collections.Generic;
using ToolBox.Services;
using UnityEngine;

namespace ToolBox.Input
{
    public class InputBindingManager : IService
    {
        private List<InputBinding> bindings = null;

        public InputBindings LoadAsset()
        {
            return Resources.Load<InputBindings>("InputBindings");
        }

        public void Load()
        {
            bindings = new List<InputBinding>(LoadAsset().bindings);
        }

        public InputBinding[] GetAllBindings()
        {
            if (bindings == null)
                Load();

            return bindings.ToArray();
        }
    }
}

