using ToolBox;
using UnityEngine;

namespace MortalCombat
{
    public class TestMonobehavior : MonoBehaviour
    {
        private void Awake()
        {
            GlobalEvents.AddGlobalListener(OnAnyMessage);
        }

        private void OnAnyMessage(Message obj)
        {
            Debug.Log($"[MESSAGE] {obj.GetType().Name}: {obj.ToString()}");
        }
    }
}