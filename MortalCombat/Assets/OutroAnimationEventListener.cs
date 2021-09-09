using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MortalCombat
{

    public class OutroAnimationEventListener : MonoBehaviour
    {
        public Action OnAnimationCompleted;

        public void Anim_AnimationCompleted()
        {
            OnAnimationCompleted?.Invoke();
        }
    }

}