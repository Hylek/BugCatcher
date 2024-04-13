using System;
using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Gameplay.Bugs
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class BaseBug : ExtendedBehaviour
    {
        [SerializeField] private float rotationDamping;
        [SerializeField] private Transform debugTarget;
        
        private NavMeshAgent _agent;
        private Transform _targetLocation;
        
        protected override void Awake()
        {
            base.Awake();

            _agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            SetTarget(debugTarget);
        }

        private void Update()
        {
            if (!(_agent.velocity.sqrMagnitude > Mathf.Epsilon)) return;
            
            var targetDirection = _agent.destination - transform.position;
            var targetRotation = Quaternion.LookRotation(targetDirection);
            var adjustedDamping = Mathf.Lerp(rotationDamping, 
                1f, Mathf.Abs(_agent.angularSpeed) / _agent.speed);

            transform.rotation = Quaternion.Slerp(transform.rotation, 
                targetRotation, adjustedDamping * Time.deltaTime);
            SetTarget(_targetLocation);
        }

        public void SetTarget(Transform target)
        {
            _targetLocation = target;
            _agent.destination = _targetLocation.position;
        }
    }
}