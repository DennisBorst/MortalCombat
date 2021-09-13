using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public List<Transform> m_Players = new List<Transform>();
    public Vector3 m_Offset;
    public float m_SmoothTime = 0.5f;

    [Header("Camera Boundries")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    [Header("Zoom Options")]
    public float m_MinZoom = 40f;
    public float m_MaxZoom = 10f;
    public float m_ZoomLimiter = 50f;

    private Vector3 m_Velocity;
    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if(m_Players.Count == 0) { return; }

        Zoom();
        Move();
    }

    private void Zoom()
    {
        float newZoom = Mathf.Lerp(m_MaxZoom, m_MinZoom, GetGreatestDistance() / m_ZoomLimiter);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
    }

    private void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + m_Offset;
        newPosition.x = Mathf.Clamp(newPosition.x , minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref m_Velocity, m_SmoothTime);
    }

    private Vector3 GetCenterPoint()
    {
        if(m_Players.Count == 1) { return m_Players[0].position; }

        var bounds = new Bounds(m_Players[0].position, Vector3.zero);
        for (int i = 0; i < m_Players.Count; i++)
        {
            bounds.Encapsulate(m_Players[i].position);
        }

        return bounds.center;
    }

    private float GetGreatestDistance()
    {
        var bounds = new Bounds(m_Players[0].position, Vector3.zero);

        for (int i = 0; i < m_Players.Count; i++)
        {
            bounds.Encapsulate(m_Players[i].position);
        }

        return bounds.size.x;
    }
}
