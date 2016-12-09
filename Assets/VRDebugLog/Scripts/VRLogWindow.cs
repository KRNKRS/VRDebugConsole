using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;
using VRDebug;

namespace VRDebug
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(BoxCollider))]
    public class VRLogWindow : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField]
        private bool isBillboard = true;
        [SerializeField]
        private bool isFollorw = false;
        [SerializeField]
        private Vector3 localPosition;
        [SerializeField]
        private Vector3 localEulerAngles;
        [SerializeField]
        private Vector3 worldPosition;
        [SerializeField]
        private Vector3 worldEulerAngle;
        [SerializeField]
        private Vector2 sizeDelta;
        private VRLogWindowParamator windowData;
        private RectTransform rectTransform;
        private BoxCollider boxCollider;

        void Awake()
        {
            if (windowData == null)
            {
                windowData = Create();
            }
            rectTransform = this.GetComponent<RectTransform>();
            if(boxCollider == null)
            {
                boxCollider = this.gameObject.GetComponent<BoxCollider>();
            }
        }

        // Use this for initialization
        void Start()
        {
            if (windowData == null)
            {
                isBillboard = true;
                isFollorw = true;
                localEulerAngles = Vector3.zero;
                //Debug.LogError("WindowData is NULL");
                return;
            }
            isBillboard = windowData.isBillboard;
            isFollorw = windowData.isFollorw;
            localPosition = windowData.localPosition;
            localEulerAngles = windowData.localEulerAngles;
            worldPosition = windowData.worldPosition;
            worldEulerAngle = windowData.worldEulerAngle;
            sizeDelta = windowData.sizeDelta;
            if (isFollorw)
            {
                this.transform.SetParent(Camera.main.transform);
                this.transform.localPosition = localPosition;
                this.transform.localEulerAngles = localEulerAngles;
            }
            else
            {
                this.transform.SetParent(null);
                this.transform.position = worldPosition;
                this.transform.eulerAngles = worldEulerAngle;
            }
            rectTransform.sizeDelta = sizeDelta;
            boxCollider.size = new Vector3(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y, 0);
        }

        // Update is called once per frame
        void Update()
        {
            if (windowData != null)
            {
                if (isFollorw)
                {
                    this.transform.SetParent(Camera.main.transform);
                }
                else
                {
                    this.transform.SetParent(null);
                }

                if (isBillboard)
                {
                    var vec = this.transform.position - Camera.main.transform.position;
                    this.transform.rotation = Quaternion.LookRotation(vec);
                }
                else
                {
                    localEulerAngles = this.transform.localEulerAngles;
                    worldEulerAngle = this.transform.eulerAngles;
                }

                localPosition = this.transform.localPosition;
                worldPosition = this.transform.position;
                //サイズ調整
                var w = Mathf.Max(0.5f, rectTransform.sizeDelta.x);
                var h = Mathf.Max(0.5f, rectTransform.sizeDelta.y);
                rectTransform.sizeDelta = new Vector2(w, h);
                sizeDelta = rectTransform.sizeDelta;
                //Debug.Log(sizeDelta);

                windowData.isBillboard = isBillboard;
                windowData.isFollorw = isFollorw;
                windowData.localPosition = localPosition;
                windowData.localEulerAngles = localEulerAngles;
                windowData.worldPosition = worldPosition;
                windowData.worldEulerAngle = worldEulerAngle;
                windowData.sizeDelta = sizeDelta;
            }
            if(boxCollider != null)
            {
                boxCollider.size = new Vector3(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y, 0);
            }
            else
            {
                boxCollider = this.gameObject.GetComponent<BoxCollider>();
            }
        }

        private VRLogWindowParamator Create()
        {
            //var path = AssetDatabase.GenerateUniqueAssetPath("Assets/VRDebugLog/Resources/VRLogWindowParamator.asset");
            var path = "Assets/VRDebugLog/Resources/VRLogWindowParamator.asset";
            var asset = AssetDatabase.LoadAssetAtPath<VRLogWindowParamator>(path);
            if (asset == null)
            {
                var item = ScriptableObject.CreateInstance<VRLogWindowParamator>();
                //if (path == null || path == "")
                //{
                //    Debug.LogError("Failed to create asset.");
                //    return null;
                //}
                AssetDatabase.CreateAsset(item, path);
                AssetDatabase.Refresh();
                AssetDatabase.SaveAssets();
                asset = AssetDatabase.LoadAssetAtPath<VRLogWindowParamator>(path);
            }
            return asset;
        }

        public void AllWindowDestroy()
        {
            Destroy(GameObject.Find("VRDebug"));
            Destroy(this.gameObject);
        }
#endif
    }

#if UNITY_EDITOR
    public class VRLogWindowParamator : ScriptableObject
    {
        public bool isBillboard = true;
        public bool isFollorw = true;
        public Vector3 localPosition = new Vector3(0, 0, 2);
        public Vector3 localEulerAngles = new Vector3(0, 0, 0);
        public Vector3 worldPosition = new Vector3(0, 0, 2);
        public Vector3 worldEulerAngle = new Vector3(0, 0, 0);
        public Vector2 sizeDelta = new Vector2(2f, 1.5f);
    }
#endif
}