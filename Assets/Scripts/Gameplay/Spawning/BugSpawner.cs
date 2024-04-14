using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Bugs;
using NUnit.Framework;
using TriInspector;
using UnityEngine;
using Utils;

namespace Gameplay.Spawning
{
    public class BugSpawner : ExtendedBehaviour
    {
        [SerializeField] private List<BaseBug> bugPrefabs;
        [SerializeField] private List<GameObject> targetPositions;

        private ObjectPool_Old<BaseBug> _bugPoolOld;

        protected override void Awake()
        {
            base.Awake();

            //_bugPool = new ObjectPool<BaseBug>();
        }

        public void SpawnBug()
        {
            var bug = _bugPoolOld.GetObject();
            bug.DestinationReached += OnDestinationReached;
            
            bug.SetStartPosition(transform);
            //bug.SetTarget();
        }

        private void OnDestinationReached(BaseBug bug)
        {
            bug.DestinationReached -= OnDestinationReached;
            
        }

        [Button]
        private void CreateTargetTransform()
        {
            var go = new GameObject();
            go.AddComponent<BlankTargetObject>();
            targetPositions.Add(go);
        }
    }
}