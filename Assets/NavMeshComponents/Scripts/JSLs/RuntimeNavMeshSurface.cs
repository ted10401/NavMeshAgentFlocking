
namespace UnityEngine.AI
{
    [RequireComponent(typeof(NavMeshSurface))]
    public class RuntimeNavMeshSurface : MonoBehaviour
    {
        [SerializeField] private Transform m_targetAgent;
        [SerializeField] private float m_updateDistance = 1;

        private NavMeshSurface m_navMeshSurface;
        private Transform m_transform;
        private Vector3 m_lastBakePosition;
        private float m_cacheDistance;
        private bool m_initialized;

        private void Awake()
        {
            m_transform = transform;
            m_transform.SetParent(m_targetAgent.parent);
            m_navMeshSurface = GetComponent<NavMeshSurface>();

            Bake();
        }

        public void Initialize()
        {
            if(m_initialized)
            {
                return;
            }

            m_initialized = true;
            Bake();
        }

        private void Update()
        {
            m_cacheDistance = Vector3.Distance(m_targetAgent.position, m_lastBakePosition);
            if (m_cacheDistance >= m_updateDistance)
            {
                Bake();
            }
        }

        private void Bake()
        {
            if (m_navMeshSurface == null)
            {
                return;
            }

            m_transform.position = m_targetAgent.position;
            m_lastBakePosition = m_transform.position;
            m_navMeshSurface.BuildNavMesh();
        }
    }
}
