using UnityEngine;
using System.Collections;

public class VRDebugMainWindow : MonoBehaviour {

    private bool isOpen = true;
    private RectTransform windowRect;
    private RectTransform parentRect;

    void Awake()
    {
        windowRect = this.transform.Find("../MainWindow").GetComponent<RectTransform>();
        parentRect = this.transform.parent.gameObject.GetComponent<RectTransform>();
    }

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
	    if(isOpen)
        {
            windowRect.sizeDelta = Vector2.Lerp(windowRect.sizeDelta, Vector2.zero, 0.2f);
            windowRect.anchoredPosition = Vector2.Lerp(windowRect.anchoredPosition, Vector2.zero, 0.2f);
        }
        else
        {
            var targetSize = new Vector2(0, -parentRect.sizeDelta.y);
            windowRect.sizeDelta = Vector2.Lerp(windowRect.sizeDelta, targetSize, 0.2f);
            var targetPos = new Vector2(0, parentRect.sizeDelta.y / 2f);
            windowRect.anchoredPosition = Vector2.Lerp(windowRect.anchoredPosition, targetPos, 0.2f);
        }
	}

    public void OpenCloseChange()
    {
        isOpen = !isOpen;
    }
}
