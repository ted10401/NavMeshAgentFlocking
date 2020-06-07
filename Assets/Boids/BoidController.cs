using UnityEngine;
using UnityEngine.AI;

public class BoidController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent[] m_boids;
    [SerializeField] private float m_controllerAlignmentWeight = 1f;
    [SerializeField] private float m_controllerCohesionWeight = 1f;
    [SerializeField] private float m_controllerSeparationWeight = 1f;
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
            m_boids[i].SetDestination(m_boids[i].transform.position + GetVelocity(m_boids[i]));
        }
    }

    private Vector3 GetVelocity(NavMeshAgent navMeshAgent)
    {
        Vector3 alignment = Vector3.zero;
        Vector3 cohesion = Vector3.zero;
        Vector3 separation = Vector3.zero;

        for(int i = 0; i < m_boids.Length; i++)
        {
            if(m_boids[i] == navMeshAgent)
            {
                continue;
            }

            alignment += m_boids[i].desiredVelocity;
            cohesion += m_boids[i].transform.position;
            separation += (navMeshAgent.transform.position - m_boids[i].transform.position) / (navMeshAgent.transform.position - m_boids[i].transform.position).magnitude;
        }

        alignment = alignment / (m_boids.Length - 1);
        alignment += m_navMeshAgent.desiredVelocity * m_controllerAlignmentWeight;
        alignment /= 2;
        alignment = Vector3.ClampMagnitude(alignment, navMeshAgent.speed);

        cohesion = cohesion / (m_boids.Length - 1);
        cohesion = (m_transform.position - m_navMeshAgent.velocity) * m_controllerCohesionWeight;
        cohesion = cohesion - navMeshAgent.transform.position;
        cohesion = Vector3.ClampMagnitude(cohesion, navMeshAgent.speed);

        separation += (navMeshAgent.transform.position - m_transform.position) * m_controllerSeparationWeight / (navMeshAgent.transform.position - m_transform.position).magnitude;
        separation = separation / m_boids.Length;
        separation = Vector3.ClampMagnitude(separation, navMeshAgent.speed);

        Vector3 velocity = Vector3.zero;
        velocity += alignment * m_alignmentWeight + cohesion * m_cohesionWeight + separation * m_separationWeight;
        velocity = Vector3.ClampMagnitude(velocity, navMeshAgent.speed);

        return velocity;
    }
}
