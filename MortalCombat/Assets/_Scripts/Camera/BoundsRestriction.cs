using UnityEngine;


namespace MortalCombat.CameraSystem
{
    public class BoundsRestriction : MonoBehaviour
    {
        [SerializeField] private Rect _Bounds;
        public Rect Bounds => _Bounds;
    }
}
