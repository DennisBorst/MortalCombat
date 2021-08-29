using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float m_Time;

    private void Update()
    {
        m_Time = Timer(m_Time);

        if(m_Time < 0) { Destroy(this.gameObject); }
    }

    private float Timer(float timer)
    {
        timer -= Time.deltaTime;
        return timer;
    }
}
