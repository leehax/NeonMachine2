using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraControlller : MonoBehaviour {

	// Use this for initialization
    [SerializeField]
    private GameObject[] players;
    [SerializeField]
    float minOrtho = 5.0f;

    [SerializeField]
    [Range(0,1)]
    float paddingAmount = 0.25f;

    private Camera camera;
    private Vector3 targetPosition;

    private Rect playerPosRect;
    private float aspectRatio;

	void Start ()
	{
	    aspectRatio = (float)Screen.width / Screen.height;
        camera = GetComponent<Camera>();
	    targetPosition = transform.position;


    }
	
	// Update is called once per frame
	void Update ()
	{
     

	    targetPosition = (players[0].transform.position + players[1].transform.position) / 2;
	    targetPosition.z = -10;
	    transform.position = Vector3.Lerp(transform.position,targetPosition, Time.deltaTime*Vector3.Distance(transform.position,targetPosition));
	  
	   
	    

        camera.orthographicSize = Mathf.Lerp(camera.orthographicSize,GetNewOrthoSize(),Time.deltaTime*Mathf.Abs(GetNewOrthoSize()-camera.orthographicSize));
	}

    float GetNewOrthoSize()
    {

        playerPosRect.xMax = Mathf.Max(players[0].transform.position.x, players[1].transform.position.x);
        playerPosRect.xMin = Mathf.Min(players[0].transform.position.x, players[1].transform.position.x);
        playerPosRect.yMax = Mathf.Max(players[0].transform.position.y, players[1].transform.position.y);
        playerPosRect.yMin = Mathf.Min(players[0].transform.position.y, players[1].transform.position.y);

        playerPosRect.xMax = transform.position.x - playerPosRect.xMax;
        playerPosRect.xMin = transform.position.x - playerPosRect.xMin;

        playerPosRect.yMax = transform.position.y - playerPosRect.yMax;
        playerPosRect.yMin = transform.position.y - playerPosRect.yMin;

        
        float x = Mathf.Max(playerPosRect.xMax, Mathf.Abs(playerPosRect.xMin));
        float y = Mathf.Max(playerPosRect.yMax, Mathf.Abs(playerPosRect.yMin));

        float newOrtho = Mathf.Max(x / aspectRatio,  y);
        
        newOrtho += minOrtho;


        float padding = newOrtho * paddingAmount;

        newOrtho += padding;

        return newOrtho;
    }
}
