using UnityEngine;

namespace Utils
{
    public class BlankTargetObject : MonoBehaviour
    {
        private bool _gizmoState = true;
        
        private void OnDrawGizmos()
        {
            if (!_gizmoState) return;
            
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(transform.position, transform.localScale);
        }

        public void SetGizmoState(bool state) => _gizmoState = state;
    }
}