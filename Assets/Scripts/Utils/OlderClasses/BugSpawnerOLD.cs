using System.Collections.Generic;
using System.Linq;
using Gameplay.Bugs;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Gameplay.Spawning
{
    public class BugSpawnerOLD : ExtendedBehaviour
    {
        [SerializeField] private List<BaseBug> bugPrefabs;
        [SerializeField] private List<BlankPositionObject> positionObjects;
        
        private List<ObjectPool_Old<BaseBug>> _bugPools;

        protected override void Awake()
        {
            base.Awake();

            _bugPools = new List<ObjectPool_Old<BaseBug>>();

            // Create multiple pools for each type of bug, this makes searching much quicker than a single
            // monolithic pool.
            foreach (var pool in bugPrefabs.Select(bugPrefab => new ObjectPool_Old<BaseBug>(bugPrefab, transform)))
            {
                _bugPools.Add(pool);
            }
        }

        private void Start()
        {
            for (var i = 0; i < 100; i++)
            {
                SpawnBug();
            }
        }

        public void SpawnBug()
        {
            Debug.Log("SpawningBug");
            var bug = _bugPools[0].GetObject();
            bug.DestinationReached += OnDestinationReached;

            var positionObject = positionObjects[Random.Range(0, positionObjects.Count)];
            
            bug.SetStartPosition(positionObject.GetStartPosition());
            bug.SetTarget(positionObject.GetTargetPosition(0));
        }

        private void OnDestinationReached(BaseBug bug)
        {
            bug.DestinationReached -= OnDestinationReached;
            var pool = _bugPools[0];
            pool.ReturnObject(bug);
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