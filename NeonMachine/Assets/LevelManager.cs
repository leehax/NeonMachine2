﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private float blendTime = 0.1f;

    [Header("Player One")]
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player1HealthBar;
    [SerializeField] private GameObject player1Thruster;
    [SerializeField] private Image player1Shoot;

    [Header("Player Two")]
    [SerializeField] private GameObject player2;
    [SerializeField] private GameObject player2HealthBar;
    [SerializeField] private GameObject player2Thruster;
    [SerializeField] private Image player2Shoot;

     PlayerInput player1Variables;
     PlayerInput player2Variables;

	float targetAlpha1 = 1.0f;
    float targetAlpha2 = 1.0f;

    float curAlpha1 = 1.0f;
    float curAlpha2 = 1.0f;


    // Use this for initialization
    void Start ()
    {
        player1Variables = player1.GetComponent<PlayerInput>();
        player2Variables = player2.GetComponent<PlayerInput>();

    }
	
	// Update is called once per frame
	void Update ()
	{
        HUDUpdate();
	    


    }

    void HUDUpdate()
    {
 
        player1HealthBar.GetComponent<Slider>().value = player1Variables.GetHealth / 100;

        player1Thruster.GetComponent<Slider>().value = player1Variables.GetFuel / 100;

        if (player1Variables.CanShoot)
        {
            targetAlpha1 = 1.0f;
        }
        else if (!player1Variables.CanShoot)
        {
            targetAlpha1 = 0.0f;
        }


        curAlpha1 += curAlpha1 < targetAlpha1 ? Time.deltaTime * (1 / blendTime) : -Time.deltaTime * (1 / blendTime);
        Mathf.Clamp(curAlpha1, 0, 1);
        player1Shoot.color = new Color(1, 1, 1, curAlpha1);

        player2HealthBar.GetComponent<Slider>().value = player2Variables.GetHealth / 100;
        player2Thruster.GetComponent<Slider>().value = player2Variables.GetFuel / 100;

        if (player2Variables.CanShoot)
        {
            targetAlpha2 = 1.0f;
        }
        else if (!player2Variables.CanShoot)
        {
            targetAlpha2 = 0.0f;
        }

        curAlpha2 += curAlpha2 < targetAlpha2 ? Time.deltaTime * (1 / blendTime) : -Time.deltaTime * (1 / blendTime);
        Mathf.Clamp(curAlpha2, 0, 1);
        player2Shoot.color = new Color(1, 1, 1, curAlpha2);
    }
}
