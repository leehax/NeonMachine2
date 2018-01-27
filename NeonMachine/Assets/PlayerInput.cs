using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

	// Use this for initialization
    private Rigidbody2D rigidbody;

    private int playerID;

    private Vector2 direction;
    private float maxVelocity = 5.0f;
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

	    if (Input.GetButton("Fire1"))
	    {
	        rigidbody.AddForce(direction);
            
	    }
	    if (rigidbody.velocity.magnitude > maxVelocity)
	    {

	        rigidbody.velocity = (rigidbody.velocity / rigidbody.velocity.magnitude) * maxVelocity;
	    }

	}

}
