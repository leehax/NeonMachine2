using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Scrolling : MonoBehaviour {

    [SerializeField]
    float scrollingSpeed;
    [SerializeField]
    float tileSize;

    Vector3 startPos;

	void Start ()
    {
        startPos = transform.position;
	}
	
	void Update ()
    {
        float newPos = Mathf.Repeat(Time.time * scrollingSpeed, tileSize);
        transform.position = Vector3.right * newPos + startPos;
	}
}
