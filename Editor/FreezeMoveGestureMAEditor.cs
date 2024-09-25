
using sweetforest.freezemovegesturema.Components;
using UnityEditor;
using UnityEngine;

namespace sweetforest.freezemovegesturema.Editor
{
    
        [CustomEditor(typeof(FreezeMoveGestureComponent))]
    public class FreezeMoveGestureMAEditor : UnityEditor.Editor
    {
        
        public override void OnInspectorGUI()
        {
        
            GUILayout.Label("Select Gesture To Allow To Move", EditorStyles.boldLabel);
            
            base.OnInspectorGUI();
            GUILayout.Label("\nSelect Gesture To Allow for UNFREEZE", EditorStyles.boldLabel);
            GUILayout.Label("Parameter that use for enable: FreezeMoveGesture/Enable\n", EditorStyles.boldLabel);
            
        }
    }
}
