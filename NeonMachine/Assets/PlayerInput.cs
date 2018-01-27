using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour {

	// Use this for initialization
    private Rigidbody2D rigidbody;

    private int playerID;

    private Vector2 direction;
    private float maxVelocity = 5.0f;
    private float thursterFuel = 1.0f;
    private float thursterCooldown = 1.0f;
 

	void Start ()
	{
	    rigidbody = GetComponent<Rigidbody2D>();
	    playerID = 0;
        direction = new Vector2(0,0);

	}
	
	// Update is called once per frame
	void Update ()
	{

	    direction.x = Input.GetAxis("HorizontalGamePad" + playerID);
	    direction.y = Input.GetAxis("VerticalGamePad" + playerID);

	    if (direction.magnitude != 0.0f)
	    {
	         if (Input.GetAxis("ThrusterGamePad"+playerID)>0.0f
                && thursterCooldown<=0.0f)
	            {
	            rigidbody.AddForce((direction/direction.magnitude)*Input.GetAxis("ThrusterGamePad"+playerID)*10);
	            thursterCooldown = 1.0f;
	            }

            rigidbody.AddForce(direction/direction.magnitude*Time.deltaTime);
            thursterCooldown -= Time.deltaTime;
	    }
	     
        
	   
	    if (rigidbody.velocity.magnitude > maxVelocity)
	    {

	        rigidbody.velocity = (rigidbody.velocity / rigidbody.velocity.magnitude) * maxVelocity;
           
	    }


	}

}
