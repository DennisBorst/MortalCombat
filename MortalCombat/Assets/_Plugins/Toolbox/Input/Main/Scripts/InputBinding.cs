using System;

namespace ToolBox.Input
{
    public enum ControllerType 
    {
        Keyboard,
        Xbox
    }

    [Serializable]
    public struct InputBinding : IIdentifiable<Guid>
    {
        public string Name;
        public string ControlScheme;
        public int Player;
        
        public string DeviceInputName;
        public ControllerType DeviceType;
        public int DeviceIndex;

        public float DeadZone;

        public Guid Id { get; set; }
    }
}

