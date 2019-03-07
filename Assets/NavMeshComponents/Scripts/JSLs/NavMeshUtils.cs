
namespace UnityEngine.AI
{
    public class NavMeshUtils
    {
        private static NavMeshPath m_navMeshPath = new NavMeshPath();
        private static NavMeshHit m_navMeshHit;
        public static bool CalculatePath(Vector3 sourcePosition, Vector3 targetPosition, int areaMask, out Vector3 destination)
        {
            destination = targetPosition;

            NavMesh.CalculatePath(sourcePosition, destination, areaMask, m_navMeshPath);
            if (m_navMeshPath.status != NavMeshPathStatus.PathComplete)
            {
                GetValidPosition(sourcePosition, targetPosition, areaMask, out destination);
                NavMesh.CalculatePath(sourcePosition, destination, areaMask, m_navMeshPath);

                return m_navMeshPath.status == NavMeshPathStatus.PathComplete;
            }

            return true;
        }

        public static void GetValidPosition(Vector3 sourcePosition, Vector3 targetPosition, int areaMask, out Vector3 validPosition)
        {
            validPosition = targetPosition;

            if (NavMesh.Raycast(sourcePosition, targetPosition, out m_navMeshHit, areaMask))
            {
                validPosition = m_navMeshHit.position;
            }
        }
    }
}