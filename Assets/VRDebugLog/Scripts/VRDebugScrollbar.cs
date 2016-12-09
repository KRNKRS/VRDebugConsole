using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VRDebugScrollbar : MonoBehaviour {

    private Scrollbar scrollBar;
    private float value;

    void Awake()
    {
        scrollBar = this.GetComponent<Scrollbar>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //value = scrollBar.value;

    }

    public void Move()
    {
        //scrollBar.value = value;
    }
}
