using System.Collections.Generic;
using TriInspector;
using UnityEditor;
using UnityEngine;

namespace Utils
{
    public class BlankPositionObject : MonoBehaviour
    {
        [SerializeField] private bool enableGizmos;
        [SerializeField] private List<GameObject> targetPositions;

        private void OnDrawGizmos()
        {
            if (!enableGizmos)
            {
                foreach (var position in targetPositions)
                {
                    position.GetComponent<BlankTargetObject>().SetGizmoState(false);
                }

                return;
            }

            foreach (var position in targetPositions)
            {
                position.GetComponent<BlankTargetObject>().SetGizmoState(true);
            }

            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position, transform.localScale);

            if (targetPositions.Count <= 0) return;
            
            Handles.color = Color.green;
            Handles.ArrowHandleCap(0, transform.position, Quaternion.LookRotation(transform.forward), 2.0f, EventType.Repaint);
            
            Gizmos.color = Color.yellow;
            
            foreach (var targetPosition in targetPositions)
            {
                if (targetPosition == null) return;

                Gizmos.DrawLine(transform.position, targetPosition.transform.position);
            }
        }

        public Transform GetStartPosition() => transform;

        public Transform GetTargetPosition(int index)
        {
            if (index < targetPositions.Count) return targetPositions[index].transform;
            
            Debug.LogWarning("BlankPositionObject::GetPosition() Tried to use an index greater than array count!");

            return targetPositions[^1].transform;
        }

        [Button]
        private void CreateNewTargetTransform()
        {
            var go = new GameObject
            {
                name = $"{transform.name}Target{targetPositions.Count}",
                transform =
                {
                    position = new Vector3(
                    transform.position.x, transform.position.y, transform.position.z - 3),
                    parent = transform
                }
            };
            go.AddComponent<BlankTargetObject>();
            
            targetPositions.Add(go);
        }
    }
}