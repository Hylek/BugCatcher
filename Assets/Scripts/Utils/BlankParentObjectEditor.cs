#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Utils
{
    [CustomEditor(typeof(BlankParentObject))]
    [CanEditMultipleObjects]
    public class BlankParentObjectEditor : Editor
    {
        private BlankParentObject _blankParentObject;
        private Tool _lastTool = Tool.None;

        private void OnEnable()
        {
            CheckAndSetObject();

            if (_blankParentObject == null)
            {
                Debug.LogError("Cannot find BlankParentObject");

                return;
            }

            _blankParentObject.ZeroTransform();

            if (_blankParentObject.GetComponents<MonoBehaviour>()[0] != _blankParentObject)
                ComponentUtility.MoveComponentUp(_blankParentObject);

            _lastTool = Tools.current;
            Tools.current = Tool.None;
        }

        private void OnDisable()
        {
            Tools.current = _lastTool;
        }

        public override void OnInspectorGUI()
        {
            CheckAndSetObject();

            var objectTransform = _blankParentObject.gameObject.GetComponent<Transform>();
            if (objectTransform.hideFlags != HideFlags.HideInInspector)
                objectTransform.hideFlags = HideFlags.HideInInspector;
            Tools.current = Tool.None;

            InternalEditorUtility.SetIsInspectorExpanded(this, false);
        }

        private void CheckAndSetObject()
        {
            if (_blankParentObject == null) _blankParentObject = target as BlankParentObject;
        }
    }
}

#endif