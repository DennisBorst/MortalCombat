using ToolBox.Services;
using UnityEngine;

namespace MortalCombat
{
#if DEBUG
    public class TargetFpsOnStartup : IBootstrapService
    {
        public TargetFpsOnStartup()
        {
            Application.targetFrameRate = 170;
        }
    }
#endif
}
