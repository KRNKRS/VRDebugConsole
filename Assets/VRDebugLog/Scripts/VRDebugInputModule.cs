using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.VR;
using VRDebug;

namespace VRDebug
{
    [AddComponentMenu("Event/VRDebug Input Module")]
    [RequireComponent(typeof(VRDebugInputAdapter))]
    public class VRDebugInputModule : BaseInputModule
    {
        public GameObject controllerObject;

        private GameObject pointerResource;
        private GameObject pointer;
        private LineRenderer lineRenderer;
        private Material material;
        private VRDebugInputAdapter inputAdapter;

        /// <summary>
        /// Update
        /// </summary>
        public override void Process()
        {
            if(controllerObject == null)
            {
                return;
            }
            SendInputEvent(GetVRPointerEventData());
        }

        /// <summary>
        /// VRController pointer data(Get fitst hit object by raycaster)
        /// </summary>
        /// <returns></returns>
        protected virtual PointerEventData GetVRPointerEventData()
        {
            PointerEventData pointerData = new PointerEventData(eventSystem);

            pointerData.Reset();
            var oldPos = pointerData.position;
            pointerData.position = GetVRPointerPosition();
            pointerData.delta = pointerData.position - pointerData.position;
            pointerData.scrollDelta = Vector3.zero;
            eventSystem.RaycastAll(pointerData, m_RaycastResultCache);
            var raycast = FindFirstRaycast(m_RaycastResultCache);
            pointerData.pointerCurrentRaycast = raycast;
            m_RaycastResultCache.Clear();
            return pointerData;
        }

        /// <summary>
        /// Get vrcontroller raycast hit point
        /// and draw line
        /// </summary>
        /// <returns></returns>
        private Vector2 GetVRPointerPosition()
        {
            ResourceCheck();
            if (VRDevice.isPresent)
            {
                var name = VRSettings.loadedDeviceName;
                //Debug.Log("Load vr device : " + name);
                switch (name)
                {
                    case "OpenVR":
                        RaycastHit hit;
                        if (Physics.Raycast(controllerObject.transform.position, controllerObject.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
                        {
                            if (hit.collider.gameObject.name != "VRLogWindow")
                            {
                                lineRenderer.SetPosition(0, Vector3.zero);
                                lineRenderer.SetPosition(1, Vector3.zero);
                                if (pointer != null)
                                {
                                    Destroy(pointer);
                                }
                                return Vector2.zero;
                            }
                            lineRenderer.SetPosition(0, controllerObject.transform.position);
                            lineRenderer.SetPosition(1, hit.point);
                            pointer.transform.position = hit.point;
                            return Camera.main.WorldToScreenPoint(hit.point);
                        }
                        else
                        {
                            lineRenderer.SetPosition(0, Vector3.zero);
                            lineRenderer.SetPosition(1, Vector3.zero);
                            if (pointer != null)
                            {
                                Destroy(pointer);
                            }
                            return Vector2.zero;
                        }
                    case "Oculus":
                    default:
                        return new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);
                }

            }
            else
            {
                return new Vector2(0, 0);
            }
        }

        /// <summary>
        /// Resource component check
        /// </summary>
        private void ResourceCheck()
        {
            pointerResource = pointerResource ?? Resources.Load("Pointer") as GameObject;
            pointer = pointer ?? (GameObject)Instantiate(pointerResource);
            material = material ?? Resources.Load("Line") as Material;
            if (lineRenderer == null)
            {
                lineRenderer = controllerObject.GetComponent<LineRenderer>();
                lineRenderer = lineRenderer ?? controllerObject.gameObject.AddComponent<LineRenderer>();
                lineRenderer.widthMultiplier = 0.01f;
                lineRenderer.material = material;
            }

            inputAdapter = inputAdapter ?? this.gameObject.GetComponent<VRDebugInputAdapter>();
            inputAdapter = inputAdapter ?? this.gameObject.AddComponent<VRDebugInputAdapter>();
        }

        /// <summary>
        /// Send event to seleced object
        /// </summary>
        /// <returns></returns>
        private bool SendInputEvent(PointerEventData _pointerData)
        {
            if (_pointerData.pointerCurrentRaycast.gameObject == null)
            {
                return false;
            }
            BaseEventData data = GetBaseEventData();
            var inputable = inputAdapter.GetInputable();
            inputable.SetController(controllerObject);
            var hitObj = _pointerData.pointerCurrentRaycast.gameObject;
            if (inputable.IsSubmit(hitObj))
            {
                //Debug.Log("Submit to " + hitObj);
                ExecuteEvents.Execute(hitObj, data, ExecuteEvents.submitHandler);
            }
            if(inputable.IsHold(_pointerData.selectedObject))
            {
                ExecuteEvents.Execute(hitObj, data, ExecuteEvents.dragHandler);
            }
            return data.used;
        }

        /// <summary>
        /// This module force stop
        /// </summary>
        public void Stop()
        {
            if(pointer != null)
            {
                Destroy(pointer);
            }
            if(lineRenderer != null)
            {
                Destroy(lineRenderer);
            }
        }
    }
}