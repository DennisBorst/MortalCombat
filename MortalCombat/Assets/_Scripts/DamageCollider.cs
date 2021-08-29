using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MortalCombat
{
    public class DamageCollider : MonoBehaviour
    {
        [SerializeField] private float m_Damage;

        private CharacterMovement m_CharacterMovement;
        private CharacterID m_CharacterID;

        private BoxCollider2D m_BoxCol;

        private void Awake()
        {
            m_CharacterMovement = GetComponentInParent<CharacterMovement>();
            m_CharacterID = GetComponentInParent<CharacterID>();
            m_BoxCol = GetComponent<BoxCollider2D>();
            m_BoxCol.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<CharacterID>().m_PlayerID == m_CharacterID.m_PlayerID) { return; }

            CharacterHealth health = collision.gameObject.GetComponent<CharacterHealth>();
            if (health != null)
            {
                health.DamageTaken(m_Damage);
            }
        }
    }
}