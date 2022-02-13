using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MortalCombat
{
    public class Shield : MonoBehaviour
    {
        [SerializeField] private GameObject hitParticle;

        private CharacterID m_CharacterID;
        private BoxCollider2D m_HitCol;

        private void Awake()
        {
            m_CharacterID = GetComponentInParent<CharacterID>();
            m_HitCol = GetComponent<BoxCollider2D>();
            //m_HitCol.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == 9)
            {
                //if (collision.gameObject.GetComponent<CharacterID>().m_PlayerID == m_CharacterID.m_PlayerID) { return; }
                //Instantiate(hitParticle, collision.gameObject.transform.position, Quaternion.identity);

                this.gameObject.SetActive(false);
            }
        }
    }
}
