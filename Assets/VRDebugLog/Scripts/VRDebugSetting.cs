using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using VRDebug;

namespace VRDebug
{
    public class VRDebugSetting : MonoBehaviour
    {
        //MaxLog
        [SerializeField]
        private int maxLog = 50;
        public int GetMaxLog { get { return maxLog; } }

        //OparateController
        [SerializeField]
        private GameObject oparateController = null;
        public GameObject GetOparateController { get { return oparateController; } }

        //GrabContoller
        [SerializeField]
        private GameObject grabController = null;
        public GameObject GetGrabController { get { return grabController; } }
        [SerializeField]
        private float grabColliderRadius = 5;
        public float GetGrabColliderRadius { get { return grabColliderRadius; } }

        //Billboard
        [SerializeField]
        private bool isBillboard = true;
        public bool GetIsBillboard { get { return isBillboard; } }

        //BindAxis
        [SerializeField]
        private bool isBind = true;
        public bool GetIsBind { get { return isBind; } }
        public bool SetGetIsGrab { set; get; }

        void Awake()
        {
            if (grabController != null)
            {
                if (grabController.GetComponent<VRDebugGrab>() == null)
                {
                    grabController.AddComponent<VRDebugGrab>();
                }
            }
        }
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(VRDebugSetting))]
    public class MissileEditor : Editor
    {
        private SerializedProperty maxLog;
        private SerializedProperty oparateController;
        private SerializedProperty grabController;
        private SerializedProperty grabColliderRadius;
        private SerializedProperty isBillboard;
        private SerializedProperty isBind;
        private bool besicFold = false;
        private bool controllerFold = false;

        void OnEnable()
        {
            maxLog = serializedObject.FindProperty("maxLog");
            oparateController = serializedObject.FindProperty("oparateController");
            grabController = serializedObject.FindProperty("grabController");
            grabColliderRadius = serializedObject.FindProperty("grabColliderRadius");
            isBillboard = serializedObject.FindProperty("isBillboard");
            isBind = serializedObject.FindProperty("isBind");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            {
                EditorGUI.indentLevel = 0;
                besicFold = CustomFold.Foldout("BASIC", besicFold);
                if(besicFold)
                {
                    EditorGUILayout.PropertyField(maxLog, new GUIContent("Max Log"));
                    EditorGUILayout.PropertyField(isBillboard, new GUIContent("Billboard"));
                    EditorGUILayout.PropertyField(isBind, new GUIContent("Transform Bind"));
                }
                controllerFold = CustomFold.Foldout("CONTROLLER", controllerFold);
                if(controllerFold)
                {
                    EditorGUILayout.PropertyField(oparateController, new GUIContent("Oparate"));
                    EditorGUILayout.PropertyField(grabController, new GUIContent("Grab"));
                    if(grabController.objectReferenceValue != null)
                    {
                        EditorGUI.indentLevel = 1;
                        EditorGUILayout.PropertyField(grabColliderRadius, new GUIContent("Collider Radius"));
                        EditorGUI.indentLevel = 0;
                    }
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}