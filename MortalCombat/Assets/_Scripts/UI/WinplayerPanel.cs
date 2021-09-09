using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using ToolBox;
using ToolBox.Injection;
using UnityEngine;

namespace MortalCombat
{
    public class WinplayerPanel : DependencyBehavior
    {
        public event Action OnAnimationsCompleted;

        [SerializeField] private TMP_Text NextNumber;
        [SerializeField] private TMP_Text PreviousNumber;
        [SerializeField] private int playerId;

        [SerializeField] private Animator panelAnimator;

        [Dependency] private PlayerStatsService playerStats;

        private bool hasWon = false;

        protected override void Awake()
        {
            base.Awake();
            GlobalEvents.AddListener<PlayerWinMessage>(OnPlayerWin);
        }

        public void OnDestroy()
        {
            GlobalEvents.RemoveListener<PlayerWinMessage>(OnPlayerWin);
        }

        private void OnPlayerWin(PlayerWinMessage obj)
        {
            int kills = playerStats.GetKills(playerId);
            NextNumber.text = (kills + 1).ToString();
            PreviousNumber.text = kills.ToString();
            
            if (playerId == obj.playerId)
                hasWon = true;
        }

        public void PlayAnimation()
        {
            panelAnimator.SetTrigger(hasWon ? "won" : "lost");
        }

        public void Anim_OnPlayerAnimationsCompleted()
        {
            OnAnimationsCompleted?.Invoke();
        }
    }
}