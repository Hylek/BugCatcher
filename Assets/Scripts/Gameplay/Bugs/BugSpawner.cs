using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace Gameplay.Bugs
{
    public class BugSpawner : ExtendedBehaviour
    {
        [SerializeField] private List<BaseBug> bugPrefabs;
        [SerializeField] private List<Transform> spawnPositions;
        [SerializeField] private List<Transform> targetPositions;
        
        private List<ObjectPool<BaseBug>> _bugPools;

        protected override void Awake()
        {
            base.Awake();

            _bugPools = new List<ObjectPool<BaseBug>>();

            // Create multiple pools for each type of bug, this makes searching much quicker than a single
            // monolithic pool.
            foreach (var pool in bugPrefabs.Select(bugPrefab => new ObjectPool<BaseBug>(bugPrefab, transform)))
            {
                _bugPools.Add(pool);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            foreach (var bugPool in _bugPools)
            {
                bugPool.Dispose();
            }
        }
    }
}