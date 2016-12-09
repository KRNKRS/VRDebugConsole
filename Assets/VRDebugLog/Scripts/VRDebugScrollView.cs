using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using VRDebug;

namespace VRDebug
{
    public class VRDebugScrollView : MonoBehaviour
    {
        private GameObject vrLogWindow;
        private GameObject vrLogItem;
        private GameObject content;
        private RectTransform conentRect;
        private Scrollbar scrollBar;
        public struct Log
        {
            public Texture2D tex;
            public int count;
            public Text countText;
        }
        private Log error;
        private Log warning;
        private Log info;
        private int allItemCount = 0;
        private List<GameObject> pool = new List<GameObject>();

        private RectTransform rectTransform;
        private float height;
        
        private Texture2D tex;

        void Awake()
        {
            rectTransform = this.GetComponent<RectTransform>();
            conentRect = this.transform.FindChild("Content").GetComponent<RectTransform>();
        }

        // Use this for initialization
        void Start()
        {
#if UNITY_EDITOR
            CheckResource();
#endif
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void AddItem(string _log, string _stackTrace, LogType _type)
        {
            if (_log != "" && _log != null && _stackTrace != "" && _stackTrace != null)
            {
                //TODO:非表示設定しても追加すると表示される問題
#if UNITY_EDITOR
                CheckResource();
#endif
                //Cllapse
                if (pool.Count > 0)
                {
                    foreach (GameObject logItem in pool)
                    {
                        var itemComp = logItem.GetComponent<VRDebugItem>();
                        var param = itemComp.GetParamator();
                        if (param.stackTraceText == _stackTrace &&
                            param.logText == _log)
                        {
                            itemComp.AddLogCount();
                            return;
                        }
                    }
                }

                //Item pooling
                var item = GetInstance();

                //Select tex and add count
                switch (_type)
                {
                    case LogType.Error:
                    case LogType.Exception:
                        tex = error.tex;
                        error.count++;
                        SetLogCountText(error);
                        break;
                    case LogType.Log:
                        tex = info.tex;
                        info.count++;
                        SetLogCountText(info);
                        break;
                    case LogType.Warning:
                        tex = warning.tex;
                        warning.count++;
                        SetLogCountText(warning);
                        break;
                }

                //Paramator
                var comp = item.GetComponent<VRDebugItem>();
                comp.SetParamator(_log, _stackTrace, tex, _type, allItemCount);
                allItemCount++;
                scrollBar.value = 0;

                //Reset item color
                int count = 0;
                foreach (GameObject itemObj in pool)
                {
                    if (itemObj.activeSelf)
                    {
                        var objCcomp = itemObj.GetComponent<VRDebugItem>();
                        objCcomp.SetButtonColor(count);
                        count++;
                    }
                }
            }
        }

        /// <summary>
        /// Get one instance that unused from pool
        /// </summary>
        /// <returns></returns>
        private GameObject GetInstance()
        {
            pool.RemoveAll(obj => obj == null);
            if (pool == null)
            {
                pool = new List<GameObject>();
            }
            foreach(GameObject item in pool)
            {
                if(!item.activeSelf)
                {
                    item.SetActive(true);
                    return item;
                }
            }
            //Limit number of items
            if (pool.Count < maxLog)
            {
                var obj = Instantiate(vrLogItem, content.transform);
                obj.transform.localPosition = new Vector3(0, 0, -0.002f);
                obj.transform.localEulerAngles = Vector3.zero;
                pool.Add(obj);
                return obj;
            }
            else
            {
                var obj = pool[0];
                pool.RemoveAt(0);
                pool.Add(obj);
                for(int i=0; i< pool.Count; i++)
                {
                    pool[i].transform.SetSiblingIndex(i);
                }
                return obj;
            }
        }

        /// <summary>
        /// Count up
        /// </summary>
        /// <param name="_log"></param>
        private void SetLogCountText(Log _log)
        {
            if(_log.count < 1000)
            {
                _log.countText.text = _log.count.ToString();
            }
            else
            {
                _log.countText.text = "999+";
            }
        }

        /// <summary>
        /// Destroy console items by button event
        /// </summary>
        public void ClearList()
        {
            foreach(GameObject item in pool)
            {
                var comp = item.GetComponent<VRDebugItem>();
                comp.Initialize();
                item.SetActive(false);
            }
            info.count = 0;
            info.countText.text = info.count.ToString();
            warning.count = 0;
            warning.countText.text = warning.count.ToString();
            error.count = 0;
            error.countText.text = error.count.ToString();
        }

        /// <summary>
        /// Change active of info item by toggle button
        /// </summary>
        public void ViewToggleToInfo(Toggle _toggle)
        {
            SetActiveItems(_toggle, LogType.Log);
        }

        /// <summary>
        /// Change active of info warning by toggle button
        /// </summary>
        public void ViewToggleToWarning(Toggle _toggle)
        {
            SetActiveItems(_toggle, LogType.Warning);
        }

        /// <summary>
        /// Change active of info error by toggle button
        /// </summary>
        public void ViewToggleToError(Toggle _toggle)
        {
            SetActiveItems(_toggle, LogType.Error, LogType.Exception);
        }

        /// <summary>
        /// Set active status of items by toggle button
        /// </summary>
        /// <param name="_toggle"></param>
        /// <param name="_logType"></param>
        private void SetActiveItems(Toggle _toggle, params LogType[] _logType)
        {
            //Change active
            foreach (GameObject item in pool)
            {
                var comp = item.GetComponent<VRDebugItem>();
                if (_toggle.isOn)
                {
                    foreach (LogType type in _logType)
                    {
                        if (comp.GetParamator().logType == type)
                        {
                            item.SetActive(true);
                        }
                    }
                }
                else
                {
                    foreach (LogType type in _logType)
                    {
                        if (comp.GetParamator().logType == type)
                        {
                            item.SetActive(false);
                        }
                    }
                }
            }
            //Change item color
            int count = 0;
            foreach (GameObject item in pool)
            {
                if (item.activeSelf)
                {
                    var comp = item.GetComponent<VRDebugItem>();
                    comp.SetButtonColor(count);
                    count++;
                }
            }
        }

#if UNITY_EDITOR
        void CheckResource()
        {
            if (vrLogWindow == null)
            {
                vrLogWindow = GameObject.Find("VRLogWindow");
            }
            if (vrLogItem == null)
            {
                vrLogItem = Resources.Load("VRLogItem") as GameObject;
            }
            if (content == null)
            {
                content = vrLogWindow.transform.FindChild("MainWindow/ScrollView/Content").gameObject;
            }
            if (scrollBar == null)
            {
                scrollBar = vrLogWindow.transform.FindChild("MainWindow/Scrollbar").GetComponent<Scrollbar>();
            }
            if (error.tex == null)
            {
                error.tex = EditorGUIUtility.Load("icons/console.erroricon.png") as Texture2D;
            }
            if (warning.tex == null)
            {
                warning.tex = EditorGUIUtility.Load("icons/console.warnicon.png") as Texture2D;
            }
            if (info.tex == null)
            {
                info.tex = EditorGUIUtility.Load("icons/d_console.infoicon.png") as Texture2D;
            }
            if (error.countText == null)
            {
                error.countText = this.transform.Find("../Toolbar/ErrorToggle/Label").GetComponent<Text>();
                error.count = 0;
            }
            if (warning.countText == null)
            {
                warning.countText = this.transform.Find("../Toolbar/WarningToggle/Label").GetComponent<Text>();
                warning.count = 0;
            }
            if (info.countText == null)
            {
                info.countText = this.transform.Find("../Toolbar/InfoToggle/Label").GetComponent<Text>();
                info.count = 0;
            }
        }
#endif

        public int maxLog { set; get; }
    }
}