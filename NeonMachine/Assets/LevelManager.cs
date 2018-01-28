using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    [Header("Player One")]
    [SerializeField] private GameObject player1;
    [SerializeField] private Image player1HealthBar;
    [SerializeField] private Image player1Thruster;
    [SerializeField] private Image player1Shoot;

    [Header("Player Two")]
    [SerializeField] private GameObject player2;
    [SerializeField] private Image player2HealthBar;
    [SerializeField] private Image player2Thruster;
    [SerializeField] private Image player2Shoot;

    private PlayerInput player1Variables;
    private PlayerInput player2Variables;

    // Use this for initialization
    void Start ()
    {
        player1Variables = player1.GetComponent<PlayerInput>();
        player2Variables = player2.GetComponent<PlayerInput>();

    }
	
	// Update is called once per frame
	void Update () {
		
        
	}
}
