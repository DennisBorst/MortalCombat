using System;
using System.Collections;
using System.Collections.Generic;
using ToolBox;
using UnityEngine;

namespace MortalCombat
{
    public class OutroOverlay : MonoBehaviour
    {
        [SerializeField] TMPro.TMP_Text text = null;
        [SerializeField] Animator animator = null;
        [SerializeField] private string format = "Player {0} has won!";

        void Awake()
        {
            GlobalEvents.AddListener<PlayerWinMessage>(OnPlayerWin);
            gameObject.SetActive(false);
        }

        private void OnPlayerWin(PlayerWinMessage obj)
        {
            gameObject.SetActive(true);
            text.text = string.Format(format, obj.playerId + 1);
            animator.SetTrigger("start");
        }
    }
}