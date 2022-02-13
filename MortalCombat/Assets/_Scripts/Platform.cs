using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MortalCombat
{
    public class Platform : MonoBehaviour
    {
        private BoxCollider2D _collision;
        private PlatformEffector2D _effector;
        //private List<CharacterMovement> _characterMovement = new List<CharacterMovement>();
        private CharacterMovement _characterMovement;


        private void Awake()
        {
            _collision = GetComponent<BoxCollider2D>();
            _effector = GetComponent<PlatformEffector2D>();
        }

        private void Update()
        {
            if(_characterMovement.input.Down(_characterMovement.m_ControllerID, "Down"))
            {
                _collision.isTrigger = true;
                StopCoroutine(PlatformEnabled());
            }
            else if (_collision.isTrigger == true)
            {
                StartCoroutine(PlatformEnabled());
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.layer == 7)
            {
                _characterMovement = collision.gameObject.GetComponent<CharacterMovement>();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _characterMovement = null;
        }

        private IEnumerator PlatformEnabled()
        {
            yield return new WaitForSeconds(0.5f);
            _collision.isTrigger = false;
        }
    }
}
