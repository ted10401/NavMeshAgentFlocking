using UnityEngine;
using UnityEngine.AI;

public class Waypoints : MonoBehaviour
{
    [SerializeField] private Transform[] m_waypoints;
    private NavMeshAgent m_navMeshAgent;
    private int m_index = -1;

    private void Awake()
    {
        m_navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if(m_navMeshAgent.remainingDistance <= m_navMeshAgent.stoppingDistance)
        {
            m_index++;
            m_index %= m_waypoints.Length;

            m_navMeshAgent.SetDestination(m_waypoints[m_index].position);
        }
    }
}
