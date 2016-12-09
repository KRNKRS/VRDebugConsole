using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.VR;
using VRDebug;

namespace VRDebug
{
    public interface IVRInputable
    {
        void SetController(GameObject _controllerObject);
        bool IsSubmit(GameObject _selectedObject);
        bool IsHold(GameObject _selectedObject);
    }

    /// <summary>
    /// Vive input
    /// </summary>
#if EXSISTENCE_STEAM_VR
    public class ViveInput : IVRInputable
    {
        private bool isSubmit = false;
        private bool isHold = false;
        private SteamVR_TrackedObject trackedObject;
        SteamVR_Controller.Device device;

        public void SetController(GameObject _controllerObject)
        {
            if(trackedObject == null)
            {
                trackedObject = _controllerObject.GetComponent<SteamVR_TrackedObject>();
                device = SteamVR_Controller.Input((int)trackedObject.index);
            }
        }

        public bool IsSubmit(GameObject _selectedObject)
        {
            isSubmit = device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger);
            //Debug.Log(_selectedObject+", "+isSubmit);
            return isSubmit;
        }
        public bool IsHold(GameObject _selectedObject)
        {
            isHold = device.GetPress(SteamVR_Controller.ButtonMask.Trigger);
            return isHold;
        }
    }
#endif

    /// <summary>
    /// Oculus input
    /// </summary>
    public class OculusInput : IVRInputable
    {
        private bool isSubmit = false;
        private bool isHold = false;
        private float submitCount = 0;
        private float holdCount = 0;

        public void SetController(GameObject _controllerObject)
        {
        }

        public bool IsSubmit(GameObject _selectedObject)
        {
            if(_selectedObject != null)
            {
                submitCount += Time.deltaTime;
                if (submitCount > 0.5f)
                {
                    isSubmit = true;
                    submitCount = 0;
                }
            }
            else
            {
                isSubmit = false;
                submitCount = 0;
            }
            return isSubmit;
        }
        public bool IsHold(GameObject _selectedObject)
        {
            if (_selectedObject != null)
            {
                holdCount += Time.deltaTime;
                if (holdCount > 0.5f)
                {
                    isHold = true;
                }
            }
            else
            {
                isHold = false;
                holdCount = 0;
            }
            return isHold;
        }
    }

    /// <summary>
    /// Other VR device input
    /// </summary>
    public class OtherVRInput : IVRInputable
    {
        private bool isSubmit = false;
        private bool isHold = false;
        private float submitCount = 0;
        private float holdCount = 0;

        public void SetController(GameObject _controllerObject)
        {
        }
        public bool IsSubmit(GameObject _selectedObject)
        {
            if (_selectedObject != null)
            {
                submitCount += Time.deltaTime;
                if (submitCount > 0.5f)
                {
                    isSubmit = true;
                    submitCount = 0;
                }
            }
            else
            {
                isSubmit = false;
                submitCount = 0;
            }
            return isSubmit;
        }
        public bool IsHold(GameObject _selectedObject)
        {
            if (_selectedObject != null)
            {
                holdCount += Time.deltaTime;
                if (holdCount > 0.5f)
                {
                    isHold = true;
                }
            }
            else
            {
                isHold = false;
                holdCount = 0;
            }
            return isHold;
        }
    }

    /// <summary>
    /// Get input status
    /// </summary>
    public class VRDebugInputAdapter : MonoBehaviour
    {
        public IVRInputable GetInputable()
        {
            if (VRDevice.isPresent)
            {
                var name = VRSettings.loadedDeviceName;
                //Debug.Log("Load vr device : " + name);
                switch (name)
                {
#if EXSISTENCE_STEAM_VR
                    case "OpenVR":
                        return new ViveInput();
#endif
                    case "Oculus":
                        return new OculusInput();
                    default:
                        return new OtherVRInput();
                }
            }
            else
            {
                return new OtherVRInput();
            }
        }
    }
}