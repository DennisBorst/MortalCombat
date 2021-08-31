using System.Collections;
using System.Collections.Generic;
using ToolBox;
using UnityEngine;

namespace MortalCombat
{
    public class CharacterHealth : MonoBehaviour
    {
        public float health;

        private int m_PlayerID;
        private float m_CurrentHealth;

        private CharacterMovement m_CharacterMovement;

        private void Start()
        {
            m_CurrentHealth = health;
            m_CharacterMovement = GetComponent<CharacterMovement>();
            m_PlayerID = GetComponent<CharacterID>().m_PlayerID;
            GlobalEvents.SendMessage(new PlayerSpawnMessage(m_PlayerID, m_CurrentHealth));
        }

        public void DamageTaken(float damage)
        {
            m_CurrentHealth -= damage;

            if (m_CurrentHealth <= 0)
            {
                m_CurrentHealth = 0;
                m_CharacterMovement.InputsAviable(false);
                GlobalEvents.SendMessage(new PlayerDeathMessage(m_PlayerID));
            }

            m_CharacterMovement.Hit();
            GlobalEvents.SendMessage(new PlayerDamagedMessage(m_PlayerID, m_CurrentHealth));
        }
    }

}