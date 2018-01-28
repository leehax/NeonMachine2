using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVars : MonoBehaviour {

    public static float RippleOffset { get{ float temp = rippleOffset; rippleOffset += 0.0001f; return temp; } }
    private static float rippleOffset = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
