using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour {

    public float moveFactor = 0.02f;
	// Use this for initialization
	void Start () {
		
	}

    void OnGUI()
    {
        if (Event.current.type == EventType.MouseDrag)
        {
            float x = -Event.current.delta.x;
            float z = Event.current.delta.y;
            Vector3 v = new Vector3(x, 0, z);
            v = Camera.main.transform.TransformVector(v);
            v.y = 0;
            Camera.main.transform.position += v * moveFactor;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
