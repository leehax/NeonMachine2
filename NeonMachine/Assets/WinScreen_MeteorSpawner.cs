using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen_MeteorSpawner : MonoBehaviour {

    [SerializeField]
    GameObject meteor;

    [SerializeField]
    float minSpawnTime;
    [SerializeField]
    float maxSpawnTime;

    [SerializeField]
    float minMeteorSpeed;
    [SerializeField]
    float maxMeteorSpeed;
    
    float spawnTime;

	void Start ()
    {
		
	}
	
	void Update ()
    {
	    if(spawnTime < Time.time)
        {
            float delay = Random.Range(minSpawnTime, maxSpawnTime);
            spawnTime = Time.time + delay;

            GameObject go = Instantiate(meteor, transform.position, transform.rotation);
            float speed = Random.Range(minMeteorSpeed, maxMeteorSpeed);
            go.GetComponent<Rigidbody2D>().AddForce(-transform.right * speed);
        }
	}
}
