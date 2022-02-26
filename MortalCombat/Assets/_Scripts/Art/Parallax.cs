using ToolBox;
using UnityEngine;

namespace MortalCombat
{
    [DefaultExecutionOrder(-1001)] // after ArenaCamera 2D
    public class Parallax : MonoBehaviour
    {
        [SerializeField] Transform _RelativeTo;
        [SerializeField] Vector3 _Amount;

        Vector3 _StartPosition;
        Vector3 _RelativeStartPosition;

        public void Start()
        {
            _StartPosition = transform.position;
            _RelativeStartPosition = _RelativeTo.position; ;
        }

        public void Update()
        {
            transform.position = _StartPosition + (_RelativeTo.position - _RelativeStartPosition).MultiplyAxis(_Amount);
        }
    }
}
