using UnityEngine;
using System.Collections;

public class LogTest : MonoBehaviour {

    private int count = 0;

    void Start()
    {
        Debug.Log("Out put test log by space bar");
    }

    void Update ()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            //Debug.Log("VRDebugLogConsole test");
            Debug.Log("count = " + count);
            Debug.LogWarning("count = " + count);
            Debug.LogError("count = " + count);
            count++;
        }
    }
}
