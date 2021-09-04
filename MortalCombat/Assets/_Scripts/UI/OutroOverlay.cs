using System;
using System.Collections;
using System.Collections.Generic;
using ToolBox;
using ToolBox.Injection;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MortalCombat
{
    public class OutroOverlay : DependencyBehavior
    {
        [SerializeField] TMPro.TMP_Text text = null;
        [SerializeField] Animator animator = null;
        [SerializeField] private string format = "Player {0} has won!";
        [Dependency] private PlayerStatsService playerStats;

        protected override void Awake()
        {
            base.Awake();
            GlobalEvents.AddListener<PlayerWinMessage>(OnPlayerWin);
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            GlobalEvents.RemoveListener<PlayerWinMessage>(OnPlayerWin);
        }

        private void OnPlayerWin(PlayerWinMessage obj)
        {
            gameObject.SetActive(true);
            text.text = string.Format(format, obj.playerId + 1);
            animator.SetTrigger("start");
            playerStats.Addkills(obj.playerId, 1);
        }

        public void GoToMainMenu()
        {
            SceneManager.LoadScene("Character Select", LoadSceneMode.Single);
        }
    }
}