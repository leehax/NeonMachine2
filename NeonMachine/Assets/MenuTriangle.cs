using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTriangle : MonoBehaviour {

    [SerializeField]
    float rotSpeed;

    void Update()
    {
        transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);
    }
}