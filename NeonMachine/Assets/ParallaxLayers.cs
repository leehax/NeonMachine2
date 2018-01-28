using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayers : MonoBehaviour
{
    [Header("Background")]
    [SerializeField]
    GameObject background;
    [Header("Parallax Layer 1")]
    [SerializeField]
    GameObject layer1;
    [SerializeField]
    float parallax1 = 0.1f;

    [Header("Parallax Layer 2")]
    [SerializeField]
    GameObject layer2;
    [SerializeField]
    float parallax2 = 0.2f;

    [Header("Parallax Layer 3")]
    [SerializeField]
    GameObject layer3;
    [SerializeField]
    float parallax3 = 0.3f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        layer1.transform.localPosition = new Vector3(-transform.position.x * parallax1, -transform.position.y * parallax1, layer1.transform.localPosition.z);
        layer2.transform.localPosition = new Vector3(-transform.position.x * parallax2, -transform.position.y * parallax2, layer2.transform.localPosition.z);
        layer3.transform.localPosition = new Vector3(-transform.position.x * parallax3, -transform.position.y * parallax3, layer3.transform.localPosition.z);

        float scale = 1.3f * (GetComponent<Camera>().orthographicSize / 5.0f);
        layer1.transform.localScale = new Vector3(scale, scale, 1);
        layer2.transform.localScale = new Vector3(scale, scale, 1);
        layer3.transform.localScale = new Vector3(scale, scale, 1);
        background.transform.localScale = new Vector3(scale, scale, 1);
    }
}
