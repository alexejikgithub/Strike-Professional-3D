using System;
using UnityEngine;
using UnityEngine.AI;

namespace Scripts
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class MainCharacterController : MonoBehaviour, Scripts.Observer.IObserver<bool>
    {
        [SerializeField] private WayPointComponent[] _wayPoints;

        private NavMeshAgent _agent;
        private int _wayPointIndex = 0;
        public Action OnLastWPReached;
        private bool _isLastWaypointReached = false;


        private bool _isGamplayOn;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            transform.position = _wayPoints[0].transform.position;
        }


        private void Update()
        {
            if (!_isGamplayOn) return;

            if (!_agent.pathPending && _agent.remainingDistance < _agent.stoppingDistance+0.5f && _wayPoints[_wayPointIndex].IsWPClear)
                SetNextWayPoint();
        }

        public void UpdateObservableData(bool gameplayStatus)
        {
            _isGamplayOn = gameplayStatus;
        }


        private void SetWaypoint(WayPointComponent waypoint)
        {
            _agent.SetDestination(waypoint.transform.position);
        }

        private void SetNextWayPoint()
        {
            if (_wayPointIndex < _wayPoints.Length - 1)
            {
                _wayPointIndex++;
                SetWaypoint(_wayPoints[_wayPointIndex]);
            }
            else if (!_isLastWaypointReached)
            {
                _isLastWaypointReached = true;
                OnLastWPReached?.Invoke();
            }
        }


        private void OnDrawGizmos()
        {
            for (var i = 0; i < _wayPoints.Length - 1; i++)
                DrawArrow(_wayPoints[i].transform.position, _wayPoints[i + 1].transform.position);
        }

        private void DrawArrow(Vector3 start, Vector3 finish)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(start, finish);
            var direction = (finish - start).normalized;
            var right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + 20f, 0) * new Vector3(0, 0, 1);
            var left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - 20f, 0) * new Vector3(0, 0, 1);
            Gizmos.DrawRay(finish, right * 1f);
            Gizmos.DrawRay(finish, left * 1f);
        }
    }
}