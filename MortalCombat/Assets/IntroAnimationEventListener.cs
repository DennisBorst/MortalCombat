using System.Collections;
using System.Collections.Generic;
using ToolBox;
using UnityEngine;

namespace MortalCombat
{
    public class IntroAnimationEventListener : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private void Awake()
        {

        }

        private void Start()
        {
            StartAnim();
        }

        public void StartAnim()
        {
            animator.SetTrigger("start");
        }

        public void StartGame()
        {
            GlobalEvents.SendMessage(new GameStartMessage());
        }

        public void EndAnim()
        {
            gameObject.SetActive(false);
        }
    }
}