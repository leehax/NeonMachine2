using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
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

    [Header("Countdown")] [SerializeField] private Image countDownImage;

     PlayerInput player1Variables;
     PlayerInput player2Variables;

	float targetAlpha1 = 1.0f;
    float targetAlpha2 = 1.0f;

    float curAlpha1 = 1.0f;
    float curAlpha2 = 1.0f;

    private bool winnerDecided = false;

    // Use this for initialization
    void Start ()
    {
        player1Variables = player1.GetComponent<PlayerInput>();
        player2Variables = player2.GetComponent<PlayerInput>();

        player1.GetComponent<Rigidbody2D>().simulated = false;
        player2.GetComponent<Rigidbody2D>().simulated = false;
        StartCoroutine(countDown());

    }

    IEnumerator countDown()
    {
      
        yield return new WaitForSeconds(3.5f);
        player1.GetComponent<Rigidbody2D>().simulated = true;
        player2.GetComponent<Rigidbody2D>().simulated = true;
        countDownImage.enabled = false;

    }
	
	// Update is called once per frame
	void Update ()
	{
        if (Input.GetKey(KeyCode.Escape))
        {
            for (int i = 0; i < GlobalVars.inactiveProjectiles.Count; i++)
            {
                Destroy(GlobalVars.inactiveProjectiles[i]);
            }
            GlobalVars.inactiveProjectiles.Clear();
            SceneManager.LoadScene("Game");
        }

        HUDUpdate();

	    if (player1Variables.GetHealth <= 0.0f
            && !winnerDecided)
	    {
	        GlobalVars.winnerPlayerID = 1;
	        winnerDecided = true;
	    
	    }
	    if (player2Variables.GetHealth <= 0.0f
            && !winnerDecided)
	    {
	        GlobalVars.winnerPlayerID = 0;
	        winnerDecided = true;
	    }

	    if (winnerDecided)
	    {
            StartCoroutine(NextLevel());
	        
	    }

    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < GlobalVars.inactiveProjectiles.Count; i++)
        {
            Destroy(GlobalVars.inactiveProjectiles[i]);
        }
        GlobalVars.inactiveProjectiles.Clear();
        SceneManager.LoadScene("WinScene");
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
