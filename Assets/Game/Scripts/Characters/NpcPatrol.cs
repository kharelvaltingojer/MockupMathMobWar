using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Game.Scripts.Characters
{
    public class NpcPatrol : MonoBehaviour 
    {
        private NavMeshAgent _agent;
        [SerializeField]
        private float radiusRange;
        private Vector3 _centrePoint;
        public bool isPatrolling = true;
        private float _maxTimeInSamePatrol = 6.0f;
        private float _currentTimePatrolling = 0f;
        private Vector3 _lastPosition;
        private float _timeInSamePosition = 0f;

        private void OnEnable()
        {
            _agent = GetComponent<NavMeshAgent>();
            _centrePoint = transform.position;
        }
    
        
        void Update()
        {
            if (PauseInputController.IsPaused) return;
            if (!isPatrolling) return;
            
            if (_lastPosition == transform.position)
            {
                _timeInSamePosition += Time.deltaTime;
            }
            else
            {
                _timeInSamePosition = 0;
            }
            
            bool hasFinishedPath = _agent.remainingDistance <= _agent.stoppingDistance;
            bool isNavMeshActiveAgent = _agent.isActiveAndEnabled;
            bool isAgentOnNavMesh = _agent.isOnNavMesh;
            bool isStuck = _timeInSamePosition >= (_maxTimeInSamePatrol / 2);
            
            _currentTimePatrolling += Time.deltaTime;
            
            if (_currentTimePatrolling >= _maxTimeInSamePatrol || isStuck)
            {
                _currentTimePatrolling = 0;
                _timeInSamePosition = 0;
                hasFinishedPath = true;
                return;
            }
            
            if(hasFinishedPath && isNavMeshActiveAgent && isAgentOnNavMesh)
            {
                _currentTimePatrolling = 0;
                Vector3 point;
                if (RandomPoint(_centrePoint, radiusRange, out point)) //pass in our centre point and radius of area
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                    _agent.SetDestination(point);
                }
            }
            else if (!isNavMeshActiveAgent || !isAgentOnNavMesh)
            {
                Debug.Log($"Unable to move {gameObject.name}: hasFinishedPath: {hasFinishedPath}, isNavMeshActiveAgent: {isNavMeshActiveAgent}, isAgentOnNavMesh: {isAgentOnNavMesh}");
            }
    
        }
        bool RandomPoint(Vector3 center, float range, out Vector3 result)
        {
    
            Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
            { 
                //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
                //or add a for loop like in the documentation
                result = hit.position;
                return true;
            }
    
            result = Vector3.zero;
            return false;
        }
    
        
    }
}
