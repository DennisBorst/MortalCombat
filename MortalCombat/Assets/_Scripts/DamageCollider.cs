using UnityEngine;

namespace MortalCombat
{
    public class DamageCollider : MonoBehaviour
    {
        [SerializeField] private float m_Damage;
        [SerializeField] private float m_Force;

        [SerializeField] private GameObject hitParticle;

        private CharacterID m_CharacterID;

        private CapsuleCollider2D m_HitCol;

        private void Awake()
        {
            m_CharacterID = GetComponentInParent<CharacterID>();
            m_HitCol = GetComponent<CapsuleCollider2D>();
            m_HitCol.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.layer == 7)
            {
                if (collision.gameObject.GetComponent<CharacterID>().m_PlayerID == m_CharacterID.m_PlayerID) { return; }
                CharacterHealth health = collision.gameObject.GetComponent<CharacterHealth>();
                CharacterMovement characterMovement = collision.gameObject.GetComponent<CharacterMovement>();
                Instantiate(hitParticle, collision.gameObject.transform.position, Quaternion.identity);
                // TODO: Implement camera shake
                //Camera.main.transform.DOShakePosition(.4f, .5f, 20, 90, false, true);

                if (!characterMovement.invincible)
                {
                    Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
                    float signX = (m_CharacterID.gameObject.transform.position.x < collision.gameObject.transform.position.x) ? 1.0f : -1.0f;
                    float signY = (m_CharacterID.gameObject.transform.position.y < collision.gameObject.transform.position.y) ? -0.5f : 0.5f;
                    Vector2 v2 = new Vector2(signX, signY);
                    rb.AddForce(v2 * m_Force);
                }

                if (health != null)
                {
                    health.DamageTaken(m_Damage);
                }
            }
        }
    }
}