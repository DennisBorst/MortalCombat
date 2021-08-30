using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ToolBox;

namespace MortalCombat
{
    public class BulletUIListener : MonoBehaviour
    {
        [SerializeField] private int m_PlayerId = 0;
        [SerializeField] private Image m_Image = null;
        [SerializeField] private Animator m_Animator = null;

        private readonly int triggerId = Animator.StringToHash("OnBullet");

        private void Awake()
        {
            this.NullCheck(nameof(m_Image), m_Image);
            GlobalEvents.AddListener<PlayerBulletMessage>(PlayerBullet);
            GlobalEvents.AddListener<PlayerSpawnMessage>(OnPlayerSpawn);
        }

        private void OnDestroy()
        {
            GlobalEvents.RemoveListener<PlayerBulletMessage>(PlayerBullet);
            GlobalEvents.RemoveListener<PlayerSpawnMessage>(OnPlayerSpawn);
        }

        private void OnPlayerSpawn(PlayerSpawnMessage obj)
        {
            SetImage(true);
        }

        private void PlayerBullet(PlayerBulletMessage obj)
        {
            if (obj.playerId != m_PlayerId)
                return;

            SetImage(obj.bulletReady);
            TriggerAnimation();
        }

        private void SetImage(bool value)
        {
            m_Image.gameObject.SetActive(value);
        }

        private void TriggerAnimation()
        {
            m_Animator.SetTrigger(triggerId);
        }
    }
}
