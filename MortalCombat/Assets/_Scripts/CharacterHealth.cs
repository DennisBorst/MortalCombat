using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    public float health;

    private float m_CurrentHealth;

    private void Start()
    {
        m_CurrentHealth = health;
    }

    public void DamageTaken(float damage)
    {
        m_CurrentHealth -= damage;
        
        if(m_CurrentHealth <= 0)
        {
            m_CurrentHealth = 0;
            //Died
        }
    }
}
