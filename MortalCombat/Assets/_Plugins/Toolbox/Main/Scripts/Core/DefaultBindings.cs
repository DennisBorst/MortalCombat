using UnityEngine;

namespace ToolBox.Input
{
    public static class DefaultBindings
    {
        private const float XBOX_DEADZONE = 0.2f;
        

        public static InputBinding[] bindings = new[]
        {
            // ** PLAYER 1 **
            new InputBinding {Player = 0, Name = "LeftUI",     DeviceName = "XBox", DeviceIndex = 0, DeviceInputName = "ShoulderL"},
            new InputBinding {Player = 0, Name = "RightUI",    DeviceName = "XBox", DeviceIndex = 0, DeviceInputName = "ShoulderR"},

            new InputBinding {Player = 0, Name = "Left",     DeviceName = "XBox", DeviceIndex = 0, DeviceInputName = "LeftX-", DeadZone = XBOX_DEADZONE},
            new InputBinding {Player = 0, Name = "Right",    DeviceName = "XBox", DeviceIndex = 0, DeviceInputName = "LeftX+", DeadZone = XBOX_DEADZONE},
            new InputBinding {Player = 0, Name = "Up",       DeviceName = "XBox", DeviceIndex = 0, DeviceInputName = "LeftY-", DeadZone = XBOX_DEADZONE},
            new InputBinding {Player = 0, Name = "Down",     DeviceName = "XBox", DeviceIndex = 0, DeviceInputName = "LeftY+", DeadZone = XBOX_DEADZONE},
            new InputBinding {Player = 0, Name = "Jump",     DeviceName = "XBox", DeviceIndex = 0, DeviceInputName = "A"},
            new InputBinding {Player = 0, Name = "Confirm",  DeviceName = "XBox", DeviceIndex = 0, DeviceInputName = "A"},
            new InputBinding {Player = 0, Name = "Throw",    DeviceName = "XBox", DeviceIndex = 0, DeviceInputName = "X"},
            new InputBinding {Player = 0, Name = "Punch",    DeviceName = "XBox", DeviceIndex = 0, DeviceInputName = "B"},
            new InputBinding {Player = 0, Name = "Back",     DeviceName = "XBox", DeviceIndex = 0, DeviceInputName = "B"},

            new InputBinding {Player = 0, Name = "Left",    DeviceName = "Keyboard", DeviceIndex = 0, DeviceInputName = KeyCode.LeftArrow.ToString()},
            new InputBinding {Player = 0, Name = "Right",   DeviceName = "Keyboard", DeviceIndex = 0, DeviceInputName = KeyCode.RightArrow.ToString()},
            new InputBinding {Player = 0, Name = "Up",      DeviceName = "Keyboard", DeviceIndex = 0, DeviceInputName = KeyCode.UpArrow.ToString()},
            new InputBinding {Player = 0, Name = "Down",    DeviceName = "Keyboard", DeviceIndex = 0, DeviceInputName = KeyCode.DownArrow.ToString()},
            new InputBinding {Player = 0, Name = "Back",    DeviceName = "Keyboard", DeviceIndex = 0, DeviceInputName = KeyCode.Backspace.ToString()},
            new InputBinding {Player = 0, Name = "Confirm", DeviceName = "Keyboard", DeviceIndex = 0, DeviceInputName = KeyCode.Return.ToString()},
            new InputBinding {Player = 0, Name = "Jump",    DeviceName = "Keyboard", DeviceIndex = 0, DeviceInputName = KeyCode.Keypad0.ToString()},
            new InputBinding {Player = 0, Name = "Jump",    DeviceName = "Keyboard", DeviceIndex = 0, DeviceInputName = KeyCode.UpArrow.ToString()},
            new InputBinding {Player = 0, Name = "Throw",   DeviceName = "Keyboard", DeviceIndex = 0, DeviceInputName = KeyCode.Keypad2.ToString()},
            new InputBinding {Player = 0, Name = "Punch",   DeviceName = "Keyboard", DeviceIndex = 0, DeviceInputName = KeyCode.Keypad1.ToString()},

            // ** PLAYER 2 **
            new InputBinding {Player = 1, Name = "LeftUI",     DeviceName = "XBox", DeviceIndex = 1, DeviceInputName = "ShoulderL"},
            new InputBinding {Player = 1, Name = "RightUI",    DeviceName = "XBox", DeviceIndex = 1, DeviceInputName = "ShoulderR"},

            new InputBinding {Player = 1, Name = "Left",    DeviceName = "XBox", DeviceIndex = 1, DeviceInputName = "LeftX-", DeadZone = XBOX_DEADZONE},
            new InputBinding {Player = 1, Name = "Right",   DeviceName = "XBox", DeviceIndex = 1, DeviceInputName = "LeftX+", DeadZone = XBOX_DEADZONE},
            new InputBinding {Player = 1, Name = "Up",      DeviceName = "XBox", DeviceIndex = 1, DeviceInputName = "LeftY+", DeadZone = XBOX_DEADZONE},
            new InputBinding {Player = 1, Name = "Down",    DeviceName = "XBox", DeviceIndex = 1, DeviceInputName = "LeftY-", DeadZone = XBOX_DEADZONE},
            new InputBinding {Player = 1, Name = "Jump",    DeviceName = "XBox", DeviceIndex = 1, DeviceInputName = "A"},
            new InputBinding {Player = 1, Name = "Confirm", DeviceName = "XBox", DeviceIndex = 1, DeviceInputName = "A"},
            new InputBinding {Player = 1, Name = "Throw",   DeviceName = "XBox", DeviceIndex = 1, DeviceInputName = "X"},
            new InputBinding {Player = 1, Name = "Punch",   DeviceName = "XBox", DeviceIndex = 1, DeviceInputName = "B"},
            new InputBinding {Player = 1, Name = "Back",    DeviceName = "XBox", DeviceIndex = 1, DeviceInputName = "B"},

            new InputBinding {Player = 1, Name = "Left",    DeviceName = "Keyboard", DeviceIndex = 0, DeviceInputName = KeyCode.A.ToString()},
            new InputBinding {Player = 1, Name = "Right",   DeviceName = "Keyboard", DeviceIndex = 0, DeviceInputName = KeyCode.D.ToString()},
            new InputBinding {Player = 1, Name = "Up",      DeviceName = "Keyboard", DeviceIndex = 0, DeviceInputName = KeyCode.W.ToString()},
            new InputBinding {Player = 1, Name = "Down",    DeviceName = "Keyboard", DeviceIndex = 0, DeviceInputName = KeyCode.S.ToString()},
            new InputBinding {Player = 1, Name = "Jump",    DeviceName = "Keyboard", DeviceIndex = 0, DeviceInputName = KeyCode.F.ToString()},
            new InputBinding {Player = 1, Name = "Jump",    DeviceName = "Keyboard", DeviceIndex = 0, DeviceInputName = KeyCode.W.ToString()},
            new InputBinding {Player = 1, Name = "Throw",   DeviceName = "Keyboard", DeviceIndex = 0, DeviceInputName = KeyCode.H.ToString()},
            new InputBinding {Player = 1, Name = "Punch",   DeviceName = "Keyboard", DeviceIndex = 0, DeviceInputName = KeyCode.G.ToString()},
            
            new InputBinding {Player = 1, Name = "Back",    DeviceName = "Keyboard", DeviceIndex = 0, DeviceInputName = KeyCode.G.ToString()},
            new InputBinding {Player = 1, Name = "Confirm", DeviceName = "Keyboard", DeviceIndex = 0, DeviceInputName = KeyCode.F.ToString()},

            // ** PLAYER 3 **
            new InputBinding {Player = 2, Name = "Left",    DeviceName = "XBox", DeviceIndex = 2, DeviceInputName = "LStickLeft"},
            new InputBinding {Player = 2, Name = "Right",   DeviceName = "XBox", DeviceIndex = 2, DeviceInputName = "LStickRight"},
            new InputBinding {Player = 2, Name = "Jump",    DeviceName = "XBox", DeviceIndex = 2, DeviceInputName = "A"},
            new InputBinding {Player = 2, Name = "Throw",   DeviceName = "XBox", DeviceIndex = 2, DeviceInputName = "X"},
            new InputBinding {Player = 2, Name = "Punch",   DeviceName = "XBox", DeviceIndex = 2, DeviceInputName = "B"},

            // ** PLAYER 4 **
            new InputBinding {Player = 3, Name = "Left",    DeviceName = "XBox", DeviceIndex = 3, DeviceInputName = "LStickLeft"},
            new InputBinding {Player = 3, Name = "Right",   DeviceName = "XBox", DeviceIndex = 3, DeviceInputName = "LStickRight"},
            new InputBinding {Player = 3, Name = "Jump",    DeviceName = "XBox", DeviceIndex = 3, DeviceInputName = "A"},
            new InputBinding {Player = 3, Name = "Throw",   DeviceName = "XBox", DeviceIndex = 3, DeviceInputName = "X"},
            new InputBinding {Player = 3, Name = "Punch",   DeviceName = "XBox", DeviceIndex = 3, DeviceInputName = "B"},
        };
    }
}

