using UnityEngine;
using UnityEngine.AI;

public class BoidController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent[] m_boids;
    [SerializeField] private float m_alignmentWeight = 1f;
    [SerializeField] private float m_cohesionWeight = 1f;
    [SerializeField] private float m_separationWeight = 1f;

    private Transform m_transform;
    private NavMeshAgent m_navMeshAgent;

    private void Awake()
    {
        m_transform = transform;
        m_navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        for (int i = 0; i < m_boids.Length; i++)
        {
            m_boids[i].velocity = GetAlignment(m_boids[i]) * m_alignmentWeight + GetCohesion(m_boids[i]) * m_cohesionWeight + GetSeparation(m_boids[i]) * m_separationWeight;
        }
    }

    private Vector3 GetAlignment(NavMeshAgent navMeshAgent)
    {
        Vector3 alignment = Vector3.zero;

        int count = 0;
        for (int i = 0; i < m_boids.Length; i++)
        {
            if(m_boids[i] == navMeshAgent)
            {
                continue;
            }

            count++;
            alignment += m_boids[i].transform.forward;
        }

        alignment += m_navMeshAgent.transform.forward;
        count++;

        alignment /= count;

        return alignment;
    }

    private Vector3 GetCohesion(NavMeshAgent navMeshAgent)
    {
        Vector3 boidCenter = Vector3.zero;
        int count = 0;

        for (int i = 0; i < m_boids.Length; i++)
        {
            if(m_boids[i] == navMeshAgent)
            {
                continue;
            }

            count++;
            boidCenter += m_boids[i].transform.position;
        }

        count++;
        boidCenter += m_transform.position;

        boidCenter /= count;

        return boidCenter - navMeshAgent.transform.position;
    }

    private Vector3 GetSeparation(NavMeshAgent navMeshAgent)
    {
        Vector3 separation = Vector3.zero;
        int count = 0;

        for (int i = 0; i < m_boids.Length; i++)
        {
            if (m_boids[i] == navMeshAgent)
            {
                continue;
            }

            count++;
            separation += m_boids[i].transform.position - navMeshAgent.transform.position;
        }

        separation /= count;
        separation *= -1;

        return separation;
    }
}
