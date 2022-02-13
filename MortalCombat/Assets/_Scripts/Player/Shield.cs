using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MortalCombat
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] private GameObject shieldBreakParticle;
        [SerializeField] private GameObject hitParticle;
        [SerializeField] private CharacterMovement _characterMovement;

        private CharacterID m_CharacterID;

        private void Awake()
        {
            m_CharacterID = _characterMovement.GetComponent<CharacterID>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 9)
            {
                if (collision.gameObject.GetComponent<Projectile>().m_CharacterID.m_PlayerID == m_CharacterID.m_PlayerID) { return; }

                //Instantiate(shieldBreakParticle, transform.position, collision.transform.rotation);
                Instantiate(hitParticle, transform.position, Quaternion.identity);
                _characterMovement.ShieldActive(false);
                Destroy(collision.gameObject);
            }
        }
    }
}
