using System;
using UnityEngine;

namespace Utils
{
    public class BlankPositionObject : MonoBehaviour
    {
        [SerializeField] private Color gizmoColour;
        
        private void OnDrawGizmosSelected()
        {
            //Gizmos.color = gizmoColour;
            //Gizmos.DrawCube(transform.position, transform.localScale);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = gizmoColour;
            Gizmos.DrawCube(transform.position, transform.localScale);
        }
    }
}