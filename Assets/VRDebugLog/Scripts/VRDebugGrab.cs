using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRDebug;

namespace VRDebug
{
    [RequireComponent(typeof(SphereCollider))]
    public class VRDebugGrab : MonoBehaviour
    {
#if EXSISTENCE_STEAM_VR
        private VRDebugSetting setting;
        private SteamVR_TrackedObject trackedObject;
        private SteamVR_Controller.Device device;
        private SphereCollider sphereCollider;
        private GameObject window;

        // Use this for initialization
        void Awake()
        {
            setting = setting ?? GameObject.Find("VRDebug").GetComponent<VRDebugSetting>();
            if (trackedObject == null)
            {
                trackedObject = setting.GetGrabController.GetComponent<SteamVR_TrackedObject>();
                device = SteamVR_Controller.Input((int)trackedObject.index);
            }
            sphereCollider = this.GetComponent<SphereCollider>();
        }

        void Start()
        {
            sphereCollider.isTrigger = true;
            sphereCollider.radius = setting.GetGrabColliderRadius;
        }

        // Update is called once per frame
        void Update()
        {
            if(device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
            {
                if(window != null)
                {
                    window.transform.SetParent(this.transform);
                    setting.SetGetIsGrab = true;
                }
            }
            else
            {
                if (window != null)
                {
                    window.transform.SetParent(null);
                    setting.SetGetIsGrab = false;
                }
            }
        }

        void OnTrrigerEnter(Collider collider)
        {
            if(collider.gameObject.GetComponent<VRLogWindow>())
            {
                window = collider.gameObject;
            }
        }

        void OnTriggerExit(Collider collider)
        {
            if (collider.gameObject.GetComponent<VRLogWindow>())
            {
                window = null;
            }
        }
#endif
    }
}