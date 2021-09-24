using System.Collections;
using System.Collections.Generic;
using System.Text;
using ToolBox.Injection;
using UnityEngine;

namespace ToolBox.Input
{
    public class ToolboxInputTester : DependencyBehavior
    {
        [SerializeField] UnityEngine.UI.Text text;
        [Dependency] InputService input;

        void Update()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in input.previousValues)
            {
                if (item.Value > 0)
                {
                    sb.Append(item.Key).Append(": ").Append(item.Value).Append('\n');
                }
            }
            text.text = sb.ToString();
        }
    }

}
