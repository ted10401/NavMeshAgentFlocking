using UnityEngine;
using UnityEngine.AI;

public class FlockingGroup : MonoBehaviour
{
    [SerializeField] private NavMeshAgent[] m_navMeshAgents;
    [SerializeField] private float m_seperateRadius = 1f;
    [SerializeField] private float m_seperateRatio = 1f;
    private Vector3 m_velocity;

    private void Update()
    {
        foreach (NavMeshAgent navMeshAgent in m_navMeshAgents)
        {
            m_velocity = GetSeperateVelocity(navMeshAgent) + navMeshAgent.transform.forward * navMeshAgent.speed;

            navMeshAgent.velocity = m_velocity;
        }
    }

    private Vector3 GetSeperateVelocity(NavMeshAgent navMeshAgent)
    {
        Vector3 seperateVelocity = Vector3.zero;
        Vector3 direction = Vector3.zero;
        int seperateCount = 0;

        foreach (NavMeshAgent agent in m_navMeshAgents)
        {
            if(navMeshAgent == agent)
            {
                continue;
            }

            direction = navMeshAgent.transform.position - agent.transform.position;

            if (direction.sqrMagnitude > m_seperateRadius * m_seperateRadius)
            {
                continue;
            }

            seperateVelocity += direction;
            seperateCount++;
        }

        if(seperateCount > 0)
        {
            seperateVelocity /= seperateCount;
        }

        return seperateVelocity;
    }
}
