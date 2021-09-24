using System;
using UnityEngine;

namespace ToolBox.Input
{
    public class UnityControllerInput : BaseInputProvider
    {
        public readonly string[] _availableInputs = new[]
{
            "LeftY+", "LeftY-", "LeftX+", "LeftX-",     // LeftStick
            "RightY+", "RightY-", "RightX+", "RightX-", // RightStick
            "ThumbL", "ThumbR",                         // Thumb presses (pressing in the sticks)
            "A", "B", "X", "Y", "Start", "Select",      // Buttons
            "TriggerR", "TriggerL",                     // Triggers
            "DUp", "DDown", "DRight", "DLeft",          // Dpad Buttons
            "ShoulderL", "ShoulderR"                    // Shoulder Buttons
        };

        public override string Name => "XBox";

        public override string[] AvailableInputs => _availableInputs;

        public override Action<int> OnDisconnect => throw new NotImplementedException();

        public override Action<int> OnConnect => throw new NotImplementedException();

        public override float GetValue(InputBinding binding)
        {
            float value  = GetValueImpl(binding.DeviceInputName, binding.DeviceIndex);

            value = DeadZone(value, binding.DeadZone);

            return value;
        }

        private float GetValueImpl(string inputName, int deviceIndex)
        {
            switch (inputName)
            {
                case "LeftY+": return AxisMax("Vertical" + deviceIndex);
                case "LeftY-": return AxisMin("Vertical" + deviceIndex);
                case "LeftX+": return AxisMax("Horizontal" + deviceIndex);
                case "LeftX-": return AxisMin("Horizontal" + deviceIndex);
                case "RightY+": return AxisMax("RVertical" + deviceIndex);
                case "RightY-": return AxisMin("RVertical" + deviceIndex);
                case "RightX+": return AxisMax("RHorizontal" + deviceIndex);
                case "RightX-": return AxisMin("RHorizontal" + deviceIndex);
                case "ThumbL": return GetThumbLeft(deviceIndex);
                case "ThumbR": return GetThumbRight(deviceIndex);
                case "A": return GetButtonA(deviceIndex);
                case "B": return GetButtonB(deviceIndex);
                case "X": return GetButtonX(deviceIndex);
                case "Y": return GetButtonY(deviceIndex);
                case "Start": return GetButtonStart(deviceIndex);
                case "Select": return GetButtonSelect(deviceIndex);
                case "DUp": return AxisMax("DVertical" + deviceIndex);
                case "DDown": return AxisMin("DVertical" + deviceIndex);
                case "DRight": return AxisMax("DHorizontal" + deviceIndex);
                case "DLeft": return AxisMin("DHorizontal" + deviceIndex);
                case "ShoulderL": return GetButtonLB(deviceIndex);
                case "ShoulderR": return GetButtonRB(deviceIndex);
                case "TriggerR": return Axis("TriggerR" + deviceIndex);
                case "TriggerL": return Axis("TriggerL" + deviceIndex);
            }
            throw new NotImplementedException($"[{nameof(UnityControllerInput)}] Input name not supported: '{inputName}'");
        }

        private float GetButtonA(int player)
        {
            switch (player)
            {
                case 0: return B(KeyCode.Joystick1Button0);
                case 1: return B(KeyCode.Joystick2Button0);
                case 2: return B(KeyCode.Joystick3Button0);
                case 3: return B(KeyCode.Joystick4Button0);
            }
            return 0;
        }

        private float GetButtonB(int player)
        {
            switch (player)
            {
                case 0: return B(KeyCode.Joystick1Button1);
                case 1: return B(KeyCode.Joystick2Button1);
                case 2: return B(KeyCode.Joystick3Button1);
                case 3: return B(KeyCode.Joystick4Button1);
            }
            return 0;
        }

        private float GetButtonX(int player)
        {
            switch (player)
            {
                case 0: return B(KeyCode.Joystick1Button2);
                case 1: return B(KeyCode.Joystick2Button2);
                case 2: return B(KeyCode.Joystick3Button2);
                case 3: return B(KeyCode.Joystick4Button2);
            }
            return 0;
        }

        private float GetButtonY(int player)
        {
            switch (player)
            {
                case 0: return B(KeyCode.Joystick1Button3);
                case 1: return B(KeyCode.Joystick2Button3);
                case 2: return B(KeyCode.Joystick3Button3);
                case 3: return B(KeyCode.Joystick4Button3);
            }
            return 0;
        }

        private float GetButtonLB(int player)
        {
            switch (player)
            {
                case 0: return B(KeyCode.Joystick1Button4);
                case 1: return B(KeyCode.Joystick2Button4);
                case 2: return B(KeyCode.Joystick3Button4);
                case 3: return B(KeyCode.Joystick4Button4);
            }
            return 0;
        }

        private float GetButtonRB(int player)
        {
            switch (player)
            {
                case 0: return B(KeyCode.Joystick1Button5);
                case 1: return B(KeyCode.Joystick2Button5);
                case 2: return B(KeyCode.Joystick3Button5);
                case 3: return B(KeyCode.Joystick4Button5);
            }
            return 0;
        }

        private float GetButtonStart(int player)
        {
            switch (player)
            {
                case 0: return B(KeyCode.Joystick1Button7);
                case 1: return B(KeyCode.Joystick2Button7);
                case 2: return B(KeyCode.Joystick3Button7);
                case 3: return B(KeyCode.Joystick4Button7);
            }
            return 0;
        }

        private float GetButtonSelect(int player)
        {
            switch (player)
            {
                case 0: return B(KeyCode.Joystick1Button6);
                case 1: return B(KeyCode.Joystick2Button6);
                case 2: return B(KeyCode.Joystick3Button6);
                case 3: return B(KeyCode.Joystick4Button6);
            }
            return 0;
        }

        private float GetThumbLeft(int player)
        {
            switch (player)
            {
                case 0: return B(KeyCode.Joystick1Button8);
                case 1: return B(KeyCode.Joystick2Button8);
                case 2: return B(KeyCode.Joystick3Button8);
                case 3: return B(KeyCode.Joystick4Button8);
            }
            return 0;
        }

        private float GetThumbRight(int player)
        {
            switch (player)
            {
                case 0: return B(KeyCode.Joystick1Button9);
                case 1: return B(KeyCode.Joystick2Button9);
                case 2: return B(KeyCode.Joystick3Button9);
                case 3: return B(KeyCode.Joystick4Button9);
            }
            return 0;
        }

        private float B(KeyCode code)
        {
            return UnityEngine.Input.GetKey(code) ? 1f : 0f;
        }

        private float AxisMax(string name)
        {
            return Mathf.Max(Axis(name), 0);
        }

        private float AxisMin(string name)
        {
            return Mathf.Max(-Axis(name), 0);
        }

        private float Axis(string name)
        {
            return UnityEngine.Input.GetAxisRaw(name);
        }
    }
}
