using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ripple : MonoBehaviour {

    [SerializeField]
    float ttl = 0.1f;
    [SerializeField]
    float startRadius = 0.1f;
    [SerializeField]
    float endRadius = 1.0f;
    [SerializeField]
    float startDistortion = 2.0f;
    [SerializeField]
    float endDistortion;

    float timeLived = 0.0f;
    float lerpMult;
    Material mat;

	// Use this for initialization
	void Start ()
    {
        mat = GetComponent<Renderer>().material;
        mat.SetFloat("_BumpAmt", startDistortion);
        transform.localScale = new Vector3(startRadius, startRadius, startRadius);
        lerpMult = 1.0f / ttl;
        Destroy(gameObject, ttl);
	}
	
	// Update is called once per frame
	void Update ()
    {
        timeLived += Time.deltaTime;
        mat.SetFloat("_BumpAmt", Mathf.Lerp(startDistortion, endDistortion, timeLived * lerpMult));
        float radius = Mathf.Lerp(startRadius, endRadius, timeLived * lerpMult);
        transform.localScale = new Vector3(radius, radius, radius);
    }
}
