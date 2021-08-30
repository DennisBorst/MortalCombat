using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace MortalCombat
{

    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float m_Damage;
        [SerializeField] private float m_Force;
        [SerializeField] private float m_Speed;
        [SerializeField] private float m_HeightForce;

        [SerializeField] private GameObject hitEnemyParticle;
        [SerializeField] private GameObject hitTerrainParticle;

        [HideInInspector] public CharacterID m_CharacterID;

        private Rigidbody2D rb;

        private CircleCollider2D m_CircleCol;

        private void Awake()
        {
            m_CircleCol = GetComponent<CircleCollider2D>();
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = transform.right * m_Speed;
            rb.AddForce(Vector2.up * m_HeightForce);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 7)
            {
                if (collision.gameObject.GetComponent<CharacterID>().m_PlayerID == m_CharacterID.m_PlayerID) { return; }
                CharacterHealth health = collision.gameObject.GetComponent<CharacterHealth>();
                Instantiate(hitEnemyParticle, collision.gameObject.transform.position, Quaternion.identity);
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
                Destroy(this.gameObject);
            }
            if (collision.gameObject.layer == 6)
            {
                Instantiate(hitTerrainParticle, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
        }
    }
}
