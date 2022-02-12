using System.Linq;
using ToolBox.Services;
using UnityEngine;

namespace ToolBox.Input
{
    public class InputBindingManager : IService
    {
        private InputBinding[] bindings = null;

        public InputBindings LoadAsset()
        {
            return Resources.Load<InputBindings>("InputBindings");
        }

        //public void Load()
        //{
        //    bindings = DefaultBindings.bindings.ToList(); 
        //        //new List<InputBinding>(LoadAsset().bindings);
        //}

        public InputBinding[] GetAllBindings()
        {
            if (bindings == null)
                bindings = DefaultBindings.bindings;

            return bindings.ToArray();
        }
    }
}

