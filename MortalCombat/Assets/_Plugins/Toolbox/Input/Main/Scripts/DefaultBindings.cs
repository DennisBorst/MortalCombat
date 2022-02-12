using UnityEngine;

namespace ToolBox.Input
{
    public static class DefaultBindings
    {
        private const float XBOX_DEADZONE = 0.2f;
        
        
        public static InputBinding[] bindings = new[]
        {
            // ** PLAYER 1 **
            new InputBinding {Player = 0, Name = "LeftUI",     DeviceType = ControllerType.Xbox, DeviceIndex = 0, DeviceInputName = "ShoulderL"},
            new InputBinding {Player = 0, Name = "RightUI",    DeviceType = ControllerType.Xbox, DeviceIndex = 0, DeviceInputName = "ShoulderR"},
        
            new InputBinding {Player = 0, Name = "Left",     DeviceType = ControllerType.Xbox, DeviceIndex = 0, DeviceInputName = "LeftX-", DeadZone = XBOX_DEADZONE},
            new InputBinding {Player = 0, Name = "Right",    DeviceType = ControllerType.Xbox, DeviceIndex = 0, DeviceInputName = "LeftX+", DeadZone = XBOX_DEADZONE},
            new InputBinding {Player = 0, Name = "Up",       DeviceType = ControllerType.Xbox, DeviceIndex = 0, DeviceInputName = "LeftY-", DeadZone = XBOX_DEADZONE},
            new InputBinding {Player = 0, Name = "Down",     DeviceType = ControllerType.Xbox, DeviceIndex = 0, DeviceInputName = "LeftY+", DeadZone = XBOX_DEADZONE},
            new InputBinding {Player = 0, Name = "Jump",     DeviceType = ControllerType.Xbox, DeviceIndex = 0, DeviceInputName = "A"},
            new InputBinding {Player = 0, Name = "Confirm",  DeviceType = ControllerType.Xbox, DeviceIndex = 0, DeviceInputName = "A"},
            new InputBinding {Player = 0, Name = "Throw",    DeviceType = ControllerType.Xbox, DeviceIndex = 0, DeviceInputName = "X"},
            new InputBinding {Player = 0, Name = "Punch",    DeviceType = ControllerType.Xbox, DeviceIndex = 0, DeviceInputName = "B"},
            new InputBinding {Player = 0, Name = "Dash",     DeviceType = ControllerType.Xbox, DeviceIndex = 0, DeviceInputName = "ShoulderR"},
            new InputBinding {Player = 0, Name = "Back",     DeviceType = ControllerType.Xbox, DeviceIndex = 0, DeviceInputName = "B"},
#if DEBUG
            new InputBinding {Player = 0, Name = "Left",    DeviceType = ControllerType.Keyboard, DeviceIndex = 0, DeviceInputName = KeyCode.A.ToString()},
            new InputBinding {Player = 0, Name = "Right",   DeviceType = ControllerType.Keyboard, DeviceIndex = 0, DeviceInputName = KeyCode.D.ToString()},
            new InputBinding {Player = 0, Name = "Up",      DeviceType = ControllerType.Keyboard, DeviceIndex = 0, DeviceInputName = KeyCode.W.ToString()},
            new InputBinding {Player = 0, Name = "Down",    DeviceType = ControllerType.Keyboard, DeviceIndex = 0, DeviceInputName = KeyCode.S.ToString()},
            new InputBinding {Player = 0, Name = "Jump",    DeviceType = ControllerType.Keyboard, DeviceIndex = 0, DeviceInputName = KeyCode.W.ToString()},
            new InputBinding {Player = 0, Name = "Throw",   DeviceType = ControllerType.Keyboard, DeviceIndex = 0, DeviceInputName = KeyCode.H.ToString()},
            new InputBinding {Player = 0, Name = "Punch",   DeviceType = ControllerType.Keyboard, DeviceIndex = 0, DeviceInputName = KeyCode.G.ToString()},
            new InputBinding {Player = 0, Name = "Dash",    DeviceType = ControllerType.Keyboard, DeviceIndex = 0, DeviceInputName = KeyCode.J.ToString()},

            new InputBinding {Player = 0, Name = "Back",    DeviceType = ControllerType.Keyboard, DeviceIndex = 0, DeviceInputName = KeyCode.G.ToString()},
            new InputBinding {Player = 0, Name = "Confirm", DeviceType = ControllerType.Keyboard, DeviceIndex = 0, DeviceInputName = KeyCode.F.ToString()},
#endif
#if !DEBUG
            new InputBinding {Player = 0, Name = "Left",    DeviceType = ControllerType.Keyboard, DeviceIndex = 0, DeviceInputName = KeyCode.A.ToString()},
            new InputBinding {Player = 0, Name = "Right",   DeviceType = ControllerType.Keyboard, DeviceIndex = 0, DeviceInputName = KeyCode.D.ToString()},
            new InputBinding {Player = 0, Name = "Up",      DeviceType = ControllerType.Keyboard, DeviceIndex = 0, DeviceInputName = KeyCode.W.ToString()},
            new InputBinding {Player = 0, Name = "Down",    DeviceType = ControllerType.Keyboard, DeviceIndex = 0, DeviceInputName = KeyCode.S.ToString()},
            new InputBinding {Player = 0, Name = "Jump",    DeviceType = ControllerType.Keyboard, DeviceIndex = 0, DeviceInputName = KeyCode.W.ToString()},
            new InputBinding {Player = 0, Name = "Throw",   DeviceType = ControllerType.Keyboard, DeviceIndex = 0, DeviceInputName = KeyCode.X.ToString()},
            new InputBinding {Player = 0, Name = "Punch",   DeviceType = ControllerType.Keyboard, DeviceIndex = 0, DeviceInputName = KeyCode.Z.ToString()},
            new InputBinding {Player = 0, Name = "Dash",    DeviceType = ControllerType.Keyboard, DeviceIndex = 0, DeviceInputName = KeyCode.C.ToString()},
#endif

            // ** PLAYER 2 **
            new InputBinding {Player = 1, Name = "LeftUI",     DeviceType = ControllerType.Xbox, DeviceIndex = 1, DeviceInputName = "ShoulderL"},
            new InputBinding {Player = 1, Name = "RightUI",    DeviceType = ControllerType.Xbox, DeviceIndex = 1, DeviceInputName = "ShoulderR"},
        
            new InputBinding {Player = 1, Name = "Left",    DeviceType = ControllerType.Xbox, DeviceIndex = 1, DeviceInputName = "LeftX-", DeadZone = XBOX_DEADZONE},
            new InputBinding {Player = 1, Name = "Right",   DeviceType = ControllerType.Xbox, DeviceIndex = 1, DeviceInputName = "LeftX+", DeadZone = XBOX_DEADZONE},
            new InputBinding {Player = 1, Name = "Up",      DeviceType = ControllerType.Xbox, DeviceIndex = 1, DeviceInputName = "LeftY-", DeadZone = XBOX_DEADZONE},
            new InputBinding {Player = 1, Name = "Down",    DeviceType = ControllerType.Xbox, DeviceIndex = 1, DeviceInputName = "LeftY+", DeadZone = XBOX_DEADZONE},
            new InputBinding {Player = 1, Name = "Jump",    DeviceType = ControllerType.Xbox, DeviceIndex = 1, DeviceInputName = "A"},
            new InputBinding {Player = 1, Name = "Confirm", DeviceType = ControllerType.Xbox, DeviceIndex = 1, DeviceInputName = "A"},
            new InputBinding {Player = 1, Name = "Throw",   DeviceType = ControllerType.Xbox, DeviceIndex = 1, DeviceInputName = "X"},
            new InputBinding {Player = 1, Name = "Punch",   DeviceType = ControllerType.Xbox, DeviceIndex = 1, DeviceInputName = "B"},
            new InputBinding {Player = 1, Name = "Dash",    DeviceType = ControllerType.Xbox, DeviceIndex = 1, DeviceInputName = "ShoulderR"},
            new InputBinding {Player = 1, Name = "Back",    DeviceType = ControllerType.Xbox, DeviceIndex = 1, DeviceInputName = "B"},
#if DEBUG
            new InputBinding {Player = 1, Name = "Left",    DeviceType = ControllerType.Keyboard, DeviceIndex = 0, DeviceInputName = KeyCode.LeftArrow.ToString()},
            new InputBinding {Player = 1, Name = "Right",   DeviceType = ControllerType.Keyboard, DeviceIndex = 0, DeviceInputName = KeyCode.RightArrow.ToString()},
            new InputBinding {Player = 1, Name = "Up",      DeviceType = ControllerType.Keyboard, DeviceIndex = 0, DeviceInputName = KeyCode.UpArrow.ToString()},
            new InputBinding {Player = 1, Name = "Down",    DeviceType = ControllerType.Keyboard, DeviceIndex = 0, DeviceInputName = KeyCode.DownArrow.ToString()},
            new InputBinding {Player = 1, Name = "Back",    DeviceType = ControllerType.Keyboard, DeviceIndex = 0, DeviceInputName = KeyCode.Backspace.ToString()},
            new InputBinding {Player = 1, Name = "Confirm", DeviceType = ControllerType.Keyboard, DeviceIndex = 0, DeviceInputName = KeyCode.Return.ToString()},
            new InputBinding {Player = 1, Name = "Jump",    DeviceType = ControllerType.Keyboard, DeviceIndex = 0, DeviceInputName = KeyCode.UpArrow.ToString()},
            new InputBinding {Player = 1, Name = "Throw",   DeviceType = ControllerType.Keyboard, DeviceIndex = 0, DeviceInputName = KeyCode.Keypad2.ToString()},
            new InputBinding {Player = 1, Name = "Punch",   DeviceType = ControllerType.Keyboard, DeviceIndex = 0, DeviceInputName = KeyCode.Keypad1.ToString()},

#endif
        
            // ** PLAYER 3 **
            new InputBinding {Player = 2, Name = "Left",    DeviceType = ControllerType.Xbox, DeviceIndex = 2, DeviceInputName = "LStickLeft"},
            new InputBinding {Player = 2, Name = "Right",   DeviceType = ControllerType.Xbox, DeviceIndex = 2, DeviceInputName = "LStickRight"},
            new InputBinding {Player = 2, Name = "Jump",    DeviceType = ControllerType.Xbox, DeviceIndex = 2, DeviceInputName = "A"},
            new InputBinding {Player = 2, Name = "Throw",   DeviceType = ControllerType.Xbox, DeviceIndex = 2, DeviceInputName = "X"},
            new InputBinding {Player = 2, Name = "Punch",   DeviceType = ControllerType.Xbox, DeviceIndex = 2, DeviceInputName = "B"},
            new InputBinding {Player = 2, Name = "Dash",    DeviceType = ControllerType.Xbox, DeviceIndex = 2, DeviceInputName = "ShoulderR"},
        
            // ** PLAYER 4 **
            new InputBinding {Player = 3, Name = "Left",    DeviceType = ControllerType.Xbox, DeviceIndex = 3, DeviceInputName = "LStickLeft"},
            new InputBinding {Player = 3, Name = "Right",   DeviceType = ControllerType.Xbox, DeviceIndex = 3, DeviceInputName = "LStickRight"},
            new InputBinding {Player = 3, Name = "Jump",    DeviceType = ControllerType.Xbox, DeviceIndex = 3, DeviceInputName = "A"},
            new InputBinding {Player = 3, Name = "Throw",   DeviceType = ControllerType.Xbox, DeviceIndex = 3, DeviceInputName = "X"},
            new InputBinding {Player = 3, Name = "Punch",   DeviceType = ControllerType.Xbox, DeviceIndex = 3, DeviceInputName = "B"},
            new InputBinding {Player = 3, Name = "Dash",    DeviceType = ControllerType.Xbox, DeviceIndex = 3, DeviceInputName = "ShoulderR"},
        };
    }
}

