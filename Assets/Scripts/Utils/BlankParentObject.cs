using UnityEngine;

namespace Utils
{
    [ExecuteInEditMode]
    public class BlankParentObject : MonoBehaviour
    {
        private void Start() => ZeroTransform();

        private void OnDestroy()
        {
#if UNITY_EDITOR
            transform.hideFlags = HideFlags.None;
#endif
        }

        public void ZeroTransform()
        {
            transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
            transform.localEulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }
}