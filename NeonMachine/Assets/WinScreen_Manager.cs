using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen_Manager : MonoBehaviour {

    [SerializeField]
    GameObject player1;
    [SerializeField]
    Sprite player1TextSprite;

    [SerializeField]
    GameObject player2;
    [SerializeField]
    Sprite player2TextSprite;

    [SerializeField]
    SpriteRenderer winnerTextSR;

	void Start ()
    {
        switch(GlobalVars.winnerPlayerID)
        {
            case 0:
                player1.SetActive(true);
                winnerTextSR.sprite = player1TextSprite;
                break;

            case 1:
                player2.SetActive(true);
                winnerTextSR.sprite = player2TextSprite;
                break;
        }
	}
}
