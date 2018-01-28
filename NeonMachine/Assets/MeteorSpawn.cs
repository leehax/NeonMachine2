using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawn : MonoBehaviour {

    [SerializeField]
    List<Transform> spawnPoints;
    [SerializeField]
    float minSpawnTime;
    [SerializeField]
    float maxSpawnTime;
    [SerializeField]
    float minSpeed;
    [SerializeField]
    float maxSpeed;

    [SerializeField]
    GameObject meteorPrefab;

    float nextSpawn;

	void Start ()
    {
		
	}
	
	void Update ()
    {
        if(Time.time > nextSpawn)
        {
            nextSpawn = Random.Range(minSpawnTime, maxSpawnTime) + Time.time;
            int spawnPoint = Random.Range(0, spawnPoints.Count);

            float speed = Random.Range(minSpeed, maxSpeed);

            GameObject go = Instantiate(meteorPrefab, spawnPoints[spawnPoint].position, spawnPoints[spawnPoint].rotation);
            go.GetComponent<Rigidbody2D>().AddForce(transform.forward * speed);
        }	
	}
}
