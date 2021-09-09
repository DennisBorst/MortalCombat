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
        [SerializeField] Animator animator = null;
        [SerializeField] WinplayerPanel playerPanel1 = null;
        [SerializeField] WinplayerPanel playerPanel2 = null;

        private int winningPlayerId = -1;


        [Dependency] private PlayerStatsService playerStats;


        protected override void Awake()
        {
            base.Awake();
            GlobalEvents.AddListener<PlayerWinMessage>(OnPlayerWin);
            gameObject.SetActive(false);

            playerPanel1.OnAnimationsCompleted += Anim_ClosePanels;
            playerPanel2.OnAnimationsCompleted += Anim_ClosePanels;
        }

        private void OnDestroy()
        {
            
            GlobalEvents.RemoveListener<PlayerWinMessage>(OnPlayerWin);
        }

        private void OnPlayerWin(PlayerWinMessage obj)
        {
            gameObject.SetActive(true);
            winningPlayerId = obj.playerId;
            animator.SetTrigger("start");
        }

        public void GoToMainMenu()
        {
            playerStats.Addkills(winningPlayerId, 1);
            SceneManager.LoadScene("Character Select", LoadSceneMode.Single);
        }

        // called by animator
        public void Anim_PlayPanelAnimations()
        {
            playerPanel1.PlayAnimation();
            playerPanel2.PlayAnimation();
        }

        // called by animator
        public void Anim_ClosePanels()
        {
            animator.SetTrigger("close");
        }
    }
}