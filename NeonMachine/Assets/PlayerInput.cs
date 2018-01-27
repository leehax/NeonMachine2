using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour {

	
    private Rigidbody2D rb;

    [SerializeField]private int playerID;

    private Vector2 direction;

    private float maxVelocity = 5.0f;
    private float thursterCooldown = 0.0f;
    private float shootCooldown = 0.0f;
    private float scale;

    // Use this for initialization
	void Start ()
	{
	    rb = GetComponent<Rigidbody2D>();
	   // playerID = 0;
        direction = new Vector2(0,0);
	    scale = Mathf.Max(transform.localScale.x, transform.localScale.y);
    
	}
	
	// Update is called once per frame
	void Update ()
	{
	    
	    direction.x = Input.GetAxis("HorizontalGamePad" + playerID);
	    direction.y = Input.GetAxis("VerticalGamePad" + playerID);
	    shootCooldown -= Time.deltaTime;

        //Move and Rotate the player
        if (direction.magnitude != 0.0f)
	    {
	         if (Input.GetAxis("ThrusterGamePad"+playerID)>0.0f
                && thursterCooldown<=0.0f)
	            {
	            rb.AddForce((direction/direction.magnitude)*Input.GetAxis("ThrusterGamePad"+playerID)*10*scale);
	            thursterCooldown = 1.0f;
	            }

            rb.AddForce(direction/direction.magnitude*Time.deltaTime*scale);
            thursterCooldown -= Time.deltaTime;


	        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
	        if (playerID == 1)
	        {
	            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle)),
	                Time.deltaTime * -500);
            }
            else if (playerID == 0)
	        {
	            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle)),
	            Time.deltaTime*500);
	        }
	        
	    }
	     
        
	    //Regulate velocity
	    if (rb.velocity.magnitude > maxVelocity)
	    {

	        rb.velocity = (rb.velocity / rb.velocity.magnitude) * maxVelocity;
           
	    }

        //Shoot
	    if (Input.GetButton("FireGamePad" + playerID)
            &&shootCooldown<=0.0f)
	    {
	        ShootProjectile();
	    }
        

	}

    void ShootProjectile()
    {
        //TODO: add functionality to shoot projectile(s)

        shootCooldown = 1.0f;
    }
 

}
