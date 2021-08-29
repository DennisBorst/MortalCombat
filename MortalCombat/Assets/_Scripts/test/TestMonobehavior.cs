using System;
using System.Collections;
using System.Collections.Generic;
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

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                GlobalEvents.SendMessage(new PlayerDeathMessage(1));
            if (Input.GetKeyDown(KeyCode.Alpha2))
                GlobalEvents.SendMessage(new PlayerDeathMessage(0));

            if (Input.GetKeyDown(KeyCode.Keypad1))
                GlobalEvents.SendMessage(
                    new PlayerDamagedMessage(0, 10, 100, UnityEngine.Random.Range(0, 100), 100));
            if (Input.GetKeyDown(KeyCode.Keypad2))
                GlobalEvents.SendMessage(
                    new PlayerDamagedMessage(1, 10, 100, UnityEngine.Random.Range(0, 100), 100));

        }
    }
}