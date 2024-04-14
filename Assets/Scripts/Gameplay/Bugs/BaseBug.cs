using System;
using UnityEngine;
using UnityEngine.AI;
using Utils;
using Random = UnityEngine.Random;

namespace Gameplay.Bugs
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class BaseBug : ExtendedBehaviour
    {
        public event Action<BaseBug> DestinationReached;
        
        [SerializeField] private float rotationDamping;
        [SerializeField] private Vector2 speedRange;
        
        private NavMeshAgent _agent;
        private Transform _targetLocation;
        private bool _canMove;
        
        protected override void Awake()
        {
            base.Awake();

            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = Random.Range(speedRange.x, speedRange.y);
        }

        private void Update()
        {
            if (!_canMove) return;
            
            var targetDirection = _agent.destination - transform.position;
            var targetRotation = Quaternion.LookRotation(targetDirection);
            var adjustedDamping = Mathf.Lerp(rotationDamping, 
                1f, Mathf.Abs(_agent.angularSpeed) / _agent.speed);

            transform.rotation = Quaternion.Slerp(transform.rotation, 
                targetRotation, adjustedDamping * Time.deltaTime);

            if (_agent.remainingDistance < .5f)
            {
                Debug.Log("Bug has reached the target!");
                _canMove = false;
                DestinationReached?.Invoke(this);
            }
        }

        public void SetStartPosition(Transform target)
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
        }

        public void SetTarget(Transform target)
        {
            _targetLocation = target;
            _agent.destination = _targetLocation.position;
            _canMove = true;
        }
    }
}