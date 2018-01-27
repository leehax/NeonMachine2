﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

    [SerializeField]
    Color color = Color.white;
    [SerializeField]
    float ttl = 5;
    [SerializeField]
    int ownerID;
    [SerializeField]
    GameObject ripple;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, ttl);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 9)
        {
            Destroy(gameObject, .01f);
            GameObject instance = Instantiate(ripple);
            instance.transform.position = transform.position;
            //Vector2 normal = other.transform.position - transform.position;
            //normal.Normalize();
            //Rigidbody2D rBody = GetComponent<Rigidbody2D>();
            //rBody.velocity = rBody.velocity - 2 * Vector2.Dot(rBody.velocity, normal) * normal;
        }
    }
}