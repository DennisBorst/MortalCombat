using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private bool m_DisableAfterTime = false;
    [SerializeField] private float m_Time;

    private float m_CurrentTime;

    private void Awake()
    {
        m_CurrentTime = m_Time;
    }

    private void OnEnable()
    {
        m_CurrentTime = m_Time;
    }

    private void Update()
    {
        m_CurrentTime = Timer(m_CurrentTime);

        if(m_CurrentTime < 0) 
        {
            if (m_DisableAfterTime) { this.gameObject.SetActive(false); }
            else { Destroy(this.gameObject); }
        }
    }

    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }
}
