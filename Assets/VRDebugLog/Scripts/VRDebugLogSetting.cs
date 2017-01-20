using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRDebug;

namespace VRDebug
{
    [ExecuteInEditMode]
    public class VRDebugLogSetting : MonoBehaviour
    {
        //MaxLog
        [SerializeField]
        private int maxLog = 50;
        public int GetMaxLog { get { return maxLog; } }

        //Billboard
        [SerializeField]
        private bool isBillboard = true;
        public bool GetIsBillboard { get { return isBillboard; } }

        //BindAxis
        [SerializeField]
        private bool isBind = true;
        public bool GetIsBind { get { return isBind; } }
    }
}