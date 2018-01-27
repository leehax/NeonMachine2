using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour {

	
    private Rigidbody2D rb;

    [SerializeField]
    int playerID;
    [SerializeField]
    float forceMult;

    [Header("Shooting")]
    [SerializeField]
    int burstAmount = 4;
    [SerializeField]
    float burstDelay = 0.05f;
    [SerializeField]
    float cooldown = 5.0f;
    [SerializeField]
    float spreadInDegrees = 22.5f;
    [SerializeField]
    int amount = 100;
    [SerializeField]
    float speed = 10.0f;
    [SerializeField]
    float offset = 0.2f;
    [SerializeField]
    GameObject emittedObject;
    [SerializeField]
    GameObject ripple;
    [SerializeField]
    Color color = Color.white;

    Vector2 direction;

    float maxVelocity = 5.0f;
    float thursterCooldown = 0.0f;
    float shootCooldown = 0.0f;
    float scale;

    int burst = 0;
    float accumulator = 0.0f;

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
	            rb.AddForce((direction/direction.magnitude)*Input.GetAxis("ThrusterGamePad"+playerID)*forceMult*scale);
	            thursterCooldown = 1.0f;
	            }

            rb.AddForce(direction/direction.magnitude*Time.deltaTime*scale);
            thursterCooldown -= Time.deltaTime;


	        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

	        
	            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle)),
	            Time.deltaTime*500);
	        
	        
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

        if (burst > 0)
        {
            if (accumulator >= burstDelay)
            {
                accumulator = 0;
                Burst();
                burst--;
            }

            accumulator += Time.deltaTime;

        }

	}

    void ShootProjectile()
    {
        //TODO: add functionality to shoot projectile(s)

        shootCooldown = cooldown;
        burst = burstAmount;
        accumulator = burstDelay;
    }
    
    void Burst()
    {
        float rotation = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
        float spread = Mathf.Deg2Rad * spreadInDegrees;

        rotation -= spread / 2.0f;

        Vector2 headingVector = new Vector2(Mathf.Cos(rotation), Mathf.Sin(rotation));
        for (int i = 0; i < amount; i++)
        {
            GameObject instance = Instantiate(emittedObject);
            instance.gameObject.GetComponent<SpriteRenderer>().color = color;
            
            instance.transform.position = transform.position + new Vector3(headingVector.x * offset, headingVector.y * offset, 0);
            instance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, (rotation + (spread / amount) * i)) * Mathf.Rad2Deg);
            instance.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(rotation + (spread / amount) * i), Mathf.Sin(rotation + (spread / amount) * i)) * speed;
        }
        GameObject instance2 = Instantiate(ripple);
        instance2.transform.position = transform.position + new Vector3(headingVector.x * offset, headingVector.y * offset, -burst * 0.1f);
    }
}
