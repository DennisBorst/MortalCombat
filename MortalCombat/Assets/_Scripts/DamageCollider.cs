using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MortalCombat
{
    public class DamageCollider : MonoBehaviour
    {
        [SerializeField] private float m_Damage;
        [SerializeField] private float m_Force;

        [SerializeField] private GameObject hitParticle;

        private CharacterMovement m_CharacterMovement;
        private CharacterID m_CharacterID;

        private BoxCollider2D m_BoxCol;
        private bool m_Disable;

        private void Awake()
        {
            m_CharacterMovement = GetComponentInParent<CharacterMovement>();
            m_CharacterID = GetComponentInParent<CharacterID>();
            m_BoxCol = GetComponent<BoxCollider2D>();
            m_BoxCol.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.layer == 7 && !m_Disable)
            {
                if (collision.gameObject.GetComponent<CharacterID>().m_PlayerID == m_CharacterID.m_PlayerID) { return; }
                m_Disable = true;
                CharacterHealth health = collision.gameObject.GetComponent<CharacterHealth>();
                Instantiate(hitParticle, collision.gameObject.transform.position, Quaternion.identity);
                Camera.main.transform.DOShakePosition(.4f, .5f, 20, 90, false, true);

                Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                float signX = (m_CharacterID.gameObject.transform.position.x < collision.gameObject.transform.position.x) ? 1.0f : -1.0f;
                float signY = (m_CharacterID.gameObject.transform.position.y < collision.gameObject.transform.position.y) ? -0.5f : 0.5f;
                Vector2 v2 = new Vector2(signX, signY);
                rb.AddForce(v2 * m_Force);

                if (health != null)
                {
                    health.DamageTaken(m_Damage);
                }
                m_Disable = false;
            }
        }
    }
}