using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
            if(grabController != null)
            {
                if (grabController.GetComponent<VRDebugGrab>() == null)
                {
                    grabController.AddComponent<VRDebugGrab>();
                }
            }
        }
    }
}