using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;

public class VRDebugSetIcon : MonoBehaviour {

    public enum TYPE
    {
        INFO,
        WARNING,
        ERROR
    }
    [SerializeField]
    private TYPE type;
    private Image image;

    void Awake()
    {
        image = this.GetComponent<Image>();
        Texture2D icon = null;
#if UNITY_EDITOR
        switch (type)
        {
            case TYPE.INFO:
                icon = EditorGUIUtility.Load("icons/d_console.infoicon.png") as Texture2D;
                break;

            case TYPE.WARNING:
                icon = EditorGUIUtility.Load("icons/console.warnicon.png") as Texture2D;
                break;

            case TYPE.ERROR:
                icon = EditorGUIUtility.Load("icons/console.erroricon.png") as Texture2D;
                break;
        }
        image.sprite = Sprite.Create(icon, new Rect(0, 0, icon.width, icon.height), Vector2.zero);
#endif
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
