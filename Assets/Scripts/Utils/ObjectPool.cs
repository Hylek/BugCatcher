using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils
{
    public class ObjectPool<T> : IDisposable where T : MonoBehaviour
    {
        private readonly Queue<T> _pooledObjects;
        private readonly T _prefab;
        private readonly Transform _parentTransform;
        private readonly bool _canExpand;

        public ObjectPool(T prefab, Transform parentTransform, int initialAmount = 0, bool canExpand = true)
        {
            _prefab = prefab;
            _parentTransform = parentTransform;
            _canExpand = canExpand;
            _pooledObjects = new Queue<T>();
            
            if (initialAmount <= 0) return;

            PreWarmPool(initialAmount);
        }

        private void PreWarmPool(int initialAmount)
        {
            // Pre-warm the pool with a set amount of objects ready to use.
            for (var i = 0; i < initialAmount; i++)
            {
                var go = Object.Instantiate(_prefab, _parentTransform);
                go.gameObject.SetActive(false);
                go.transform.SetParent(_parentTransform);
                _pooledObjects.Enqueue(go);
            }
        }

        public T GetObject()
        {
            T objectToReturn;
            
            if (_pooledObjects.Count > 0)
            {
                objectToReturn = _pooledObjects.Dequeue();
                objectToReturn.gameObject.SetActive(true);
            }
            else if (_canExpand)
            {
                objectToReturn = Object.Instantiate(_prefab, _parentTransform);
                objectToReturn.GetComponent<MonoBehaviour>().enabled = false; // Disable initial behavior
            }
            else
            {
                throw new UnityException("Object pool is out of objects and cannot expand!");
            }
            
            return objectToReturn;
        }

        public void ReturnObject(T objectToReturn)
        {
            objectToReturn.gameObject.SetActive(false);
            objectToReturn.transform.SetParent(_parentTransform);
            _pooledObjects.Enqueue(objectToReturn);
        }

        public void Dispose()
        {
            foreach (var pooledObject in _pooledObjects)
            {
                Object.Destroy(pooledObject);
            }
            _pooledObjects.Clear();
        }
    }
}