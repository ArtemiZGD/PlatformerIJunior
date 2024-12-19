using UnityEngine;
using UnityEngine.AI;

public class BatSpawnPoints : MonoBehaviour
{
    private class BatSpawnPoint
    {
        public Transform Target;
        public bool IsEmpty;
    } 

    private BatSpawnPoint[] _batSpawnPoints;

    private void Start()
    {
        InitializePoints();
    }

    public Transform FindEmptyTarget(NavMeshAgent batAgent)
    {
        float closestDistance = float.MaxValue;
        BatSpawnPoint closestPoint = null;

        foreach (var point in _batSpawnPoints)
        {
            if (point.IsEmpty)
            {
                float distance = Vector3.Distance(batAgent.transform.position, point.Target.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPoint = point;
                }
            }
        }

        batAgent.SetDestination(closestPoint.Target.position);
        closestPoint.IsEmpty = false;
        return closestPoint.Target;
    }

    public void FreeTarget(Transform target)
    {
        for (int i = 0; i < _batSpawnPoints.Length; i++)
        {
            if (_batSpawnPoints[i].Target == target)
            {
                _batSpawnPoints[i].IsEmpty = true;
                return;
            }
        }
    }

    private void InitializePoints()
    {
        _batSpawnPoints = new BatSpawnPoint[transform.childCount];

        for (int i = 0; i < _batSpawnPoints.Length; i++)
        {
            _batSpawnPoints[i] = new BatSpawnPoint
            {
                Target = transform.GetChild(i),
                IsEmpty = true
            };
        }
    }
}
