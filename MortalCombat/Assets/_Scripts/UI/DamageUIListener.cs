using System;
using System.Collections;
using System.Collections.Generic;
using ToolBox;
using UnityEngine;

namespace MortalCombat
{
    public class DamageUIListener : MonoBehaviour
    {
        [SerializeField] private int playerId = 0;
        [SerializeField] private TMPro.TMP_Text text = null;
        [SerializeField] private Animator Animator = null;
        [SerializeField] private string healthFormat = "";
        [SerializeField] private string playerFormat = "";

        private readonly int triggerId = Animator.StringToHash("OnDamage");

        private void Awake()
        {
            this.NullCheck(nameof(text), text);
            GlobalEvents.AddListener<PlayerDamagedMessage>(OnPlayerDamaged);
            GlobalEvents.AddListener<PlayerSpawnMessage>(OnPlayerSpawn);
        }

        private void OnDestroy()
        {
            GlobalEvents.RemoveListener<PlayerDamagedMessage>(OnPlayerDamaged);
            GlobalEvents.RemoveListener<PlayerSpawnMessage>(OnPlayerSpawn);
        }

        private void OnPlayerSpawn(PlayerSpawnMessage obj)
        {
            SetText(obj.health);
        }

        private void OnPlayerDamaged(PlayerDamagedMessage obj)
        {
            if (obj.playerId != playerId)
                return;

            SetText(obj.newHealth);
            TriggerAnimation();
        }

        private void SetText(float health)
        {
            text.text = string.Format(playerFormat, playerId + 1) + ": " + string.Format(healthFormat, health);
        }

        private void TriggerAnimation()
        {
            Animator.SetTrigger(triggerId);
        }
    }
}