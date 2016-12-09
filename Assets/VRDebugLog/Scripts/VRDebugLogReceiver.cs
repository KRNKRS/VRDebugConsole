using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using VRDebug;

namespace VRDebug
{
    [ExecuteInEditMode]
    public class VRDebugLogReceiver : MonoBehaviour
    {
        private GameObject vrLogWindow;
        private VRDebugScrollView scrollView;
        public int maxLog = 50;
        private EventSystem eventSystemObject;
        private BaseInputModule inputModule;
        private VRDebugInputModule debugInputModule;

        void OnEnable()
        {
            CheckResource();
            Application.logMessageReceived += LogCallBackHandler;
#if UNITY_EDITOR
            if (AssetDatabase.FindAssets("t:script SteamVR").Length > 0)
            {
                var nowSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
                var addSymbol = "EXSISTENCE_STEAM_VR";
                if(nowSymbols.IndexOf(addSymbol) < 0)
                {
                    addSymbol = ";" + addSymbol + ";";
                    var newSymbols = nowSymbols + addSymbol;
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(
                       EditorUserBuildSettings.selectedBuildTargetGroup,
                       newSymbols
                   );
                   Debug.Log("Find asset \"SteamVR\".\nAdd DefineSymbols(Player Settings -> Other Settings -> Scripting Define Symbols).");
                }
            }
            //StandaloneInputModule:ON / VRDebugInputModule:OFF
            else
            {
                var nowSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
                var newSymbols = nowSymbols.Replace("EXSISTENCE_STEAM_VR", "");
                PlayerSettings.SetScriptingDefineSymbolsForGroup(
                   EditorUserBuildSettings.selectedBuildTargetGroup,
                   newSymbols
                );

                inputModule = CheckInputModule(inputModule, typeof(StandaloneInputModule));
                inputModule.enabled = true;
                debugInputModule = (VRDebugInputModule)CheckInputModule(debugInputModule, typeof(VRDebugInputModule));
                debugInputModule.Stop();
                debugInputModule.enabled = false;
                Debug.Log("Asset \"SteamVR\" not found, but VRDebugConsole does work without it.");
            }
#endif
        }

        void OnDisable()
        {
            Application.logMessageReceived -= LogCallBackHandler;
        }

        void LogCallBackHandler(string _log, string _stackTrace, LogType _type)
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                scrollView.maxLog = maxLog;
                scrollView.AddItem(_log, _stackTrace, _type);
            }
#endif
        }

        private void CheckResource()
        {
            eventSystemObject = FindObjectOfType<EventSystem>();
            if (eventSystemObject == null)
            {
                var es = new GameObject("EventSystem", typeof(EventSystem));
                es.AddComponent<VRDebugInputModule>();
            }
            else
            {
                var getComp = eventSystemObject.gameObject.GetComponent<VRDebugInputModule>();
                if (getComp == null)
                {
                    eventSystemObject.gameObject.AddComponent<VRDebugInputModule>();
                }
            }
            if (vrLogWindow == null)
            {
                var obj = GameObject.Find("VRLogWindow");
                if (obj == null)
                {
                    obj = GameObject.Find("VRLogWindow(Clone)");
                }
                if (obj == null)
                {
                    vrLogWindow = Resources.Load("VRLogWindow") as GameObject;
                    vrLogWindow = (GameObject)Instantiate(vrLogWindow);
                    vrLogWindow.name = "VRLogWindow";
                }
                else
                {
                    vrLogWindow = obj;
                }
            }
            if(scrollView == null)
            {
                scrollView = vrLogWindow.transform.FindChild("MainWindow/ScrollView").GetComponent<VRDebugScrollView>();
            }
        }

        /// <summary>
        /// Check component by name
        /// </summary>
        /// <param name="_componentName"></param>
        private BaseInputModule CheckInputModule(BaseInputModule _module, Type _componentName)
        {
            if (_module == null)
            {
                var comp = (BaseInputModule)GameObject.Find("EventSystem").gameObject.GetComponent(_componentName);
                _module = comp != null ? comp : (BaseInputModule)this.gameObject.AddComponent(_componentName);
            }
            return _module;
        }
    }
}