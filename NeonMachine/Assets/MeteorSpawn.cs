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

    [SerializeField]
    GameObject Petunia;
    [SerializeField]
    int petuniaChance;
	
	void Update ()
    {
        if(Time.time > nextSpawn)
        {
            nextSpawn = Random.Range(minSpawnTime, maxSpawnTime) + Time.time;
            int spawnPoint = Random.Range(0, spawnPoints.Count);

            float speed = Random.Range(minSpeed, maxSpeed);

            int petuniaRand = Random.Range(0, petuniaChance);

            GameObject go;

            if (petuniaRand == 1)
            {
                go = Instantiate(Petunia, spawnPoints[spawnPoint].position, spawnPoints[spawnPoint].rotation);
                go.GetComponent<Rigidbody2D>().AddForce(-go.transform.right * 800);
            }
            else
            {
                go = Instantiate(meteorPrefab, spawnPoints[spawnPoint].position, spawnPoints[spawnPoint].rotation);
                go.GetComponent<Rigidbody2D>().AddForce(-go.transform.right * speed);
            }
        }	
	}
}
