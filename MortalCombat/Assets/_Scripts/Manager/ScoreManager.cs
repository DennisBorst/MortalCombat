using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using ToolBox.Injection;

namespace MortalCombat
{
    public class ScoreManager : DependencyBehavior
    {
        [SerializeField] private TextMeshProUGUI m_ScoreText;
        [Dependency] private PlayerStatsService playerStats;

        protected override void Awake()
        {
            base.Awake();
            SetText();
        }

        private void SetText()
        {
            if(m_ScoreText != null) { m_ScoreText.text = playerStats.GetKills(0) + " - " + playerStats.GetKills(1); }
        }
    }
}
