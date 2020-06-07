using UnityEngine;
using UnityEngine.AI;

public class Rotate : MonoBehaviour
{
    [SerializeField] private Vector3 m_center;
    [SerializeField] private float m_radius;
    [SerializeField] private float m_rotateSpeed;
    [SerializeField] private float m_angle;
    private NavMeshAgent m_navMeshAgent;
    private Vector3 m_destination;

    private void Awake()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(m_navMeshAgent.remainingDistance <= m_navMeshAgent.stoppingDistance)
        {
            m_angle += m_rotateSpeed * Time.deltaTime;
            m_destination = m_center + Quaternion.Euler(0, m_angle, 0) * Vector3.forward * m_radius;
        }

        m_navMeshAgent.SetDestination(m_destination);
    }
}
