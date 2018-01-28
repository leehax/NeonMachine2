using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAmbience : MonoBehaviour {

    [SerializeField]
    List<AudioClip> whaleClips;
    AudioSource whaleSource;
    [SerializeField]
    float minWhaleDelay;
    [SerializeField]
    float maxWhaleDelay;
    float whaleTime;
    bool wasPlaying;

	void Awake()
    {
        //whaleSource = gameObject.AddComponent<AudioSource>() as AudioSource;
        //whaleSource.playOnAwake = false;
	}
	
	void Update ()
    {
	}

    void UpdateWhaleSounds()
    {
        if (wasPlaying && !whaleSource.isPlaying)
        {
            float rand = Random.Range(minWhaleDelay, maxWhaleDelay);
            whaleTime = Time.time + rand;
            wasPlaying = false;
        }

        if(!wasPlaying && !whaleSource.isPlaying && Time.time > whaleTime)
        {
            wasPlaying = true;
            int rand = Random.Range(0, whaleClips.Count);
            whaleSource.clip = whaleClips[rand];
            whaleSource.Play();
        }
    }
}
