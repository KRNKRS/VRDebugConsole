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

        //BindPosition
        [SerializeField]
        private bool isBindPositionX = true;
        public bool GetIsBindPositionX { get { return isBindPositionX; } }
        [SerializeField]
        private bool isBindPositionY = true;
        public bool GetIsBindPositionY { get { return isBindPositionY; } }
        [SerializeField]
        private bool isBindPositionZ = true;
        public bool GetIsBindPositionZ { get { return isBindPositionZ; } }

        //BindRotation
        [SerializeField]
        private bool isBindRotationX = true;
        public bool GetIsBindRotationX { get { return isBindRotationX; } }
        [SerializeField]
        private bool isBindRotationY = true;
        public bool GetIsBindRotationY { get { return isBindRotationY; } }
        [SerializeField]
        private bool isBindRotationZ = true;
        public bool GetIsBindRotationZ { get { return isBindRotationZ; } }
    }
}