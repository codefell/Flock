using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {
    public GameObject[] pieces;
    public static Map inst;
	// Use this for initialization
	void Start () {
        if (inst == null)
        {
            inst = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
