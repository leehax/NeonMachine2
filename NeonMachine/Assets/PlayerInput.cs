using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour {

	
    private Rigidbody2D rb;
    private ParticleSystem particleSys;
    [SerializeField]
    int playerID;
    

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

    [Header("Thruster")]
    [SerializeField]
    float maxThrusterFuel = 3.0f;
    [SerializeField]
    float thrusterCoolDown;
    [SerializeField]
    float forceMult;

    Vector2 direction;

    float maxVelocity = 5.0f;
    float thrusterFuel = 1.0f;
    float shootCooldown = 0.0f;
    float scale;
    float currentThrusterCoolDown=0.0f;
    int burst = 0;
    float accumulator = 0.0f;
    private bool isBoosting = false;

    // Use this for initialization
	void Start ()
	{
	    rb = GetComponent<Rigidbody2D>();
	    particleSys=GetComponent<ParticleSystem>();
        direction = new Vector2(0,0);
	    scale = Mathf.Max(transform.localScale.x, transform.localScale.y);
    
	}
	
	// Update is called once per frame
	void Update ()
	{
	    
	    direction.x = Input.GetAxis("HorizontalGamePad" + playerID);
	    direction.y = Input.GetAxis("VerticalGamePad" + playerID);
	    shootCooldown -= Time.deltaTime;
	    currentThrusterCoolDown -= Time.deltaTime;

	    if (!isBoosting)
	    {
	        thrusterFuel += Time.deltaTime*10;
	        particleSys.Stop();

	    }
        
        //Move and Rotate the player
        if (direction.magnitude != 0.0f)
	    {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle)),
                    Time.deltaTime*500);
	        
	   
	        
	    }
        if (Input.GetAxis("ThrusterGamePad" + playerID) > 0.0f
	            && thrusterFuel >= 0.0f
                && thrusterCoolDown<=0.0f)
	        {

	            rb.AddForce(transform.forward * Input.GetAxis("ThrusterGamePad" + playerID) * forceMult *
	                        scale);
	            currentThrusterCoolDown = thrusterCoolDown;
	            thrusterFuel -= Time.deltaTime * 10;
	            isBoosting = true;
	            if (particleSys.isPlaying == false)
	            {
	                particleSys.Play();
	            }

	     }
        

        print(currentThrusterCoolDown);
	        
	    
     

        //Shoot
	    if (Input.GetButton("FireGamePad"+playerID)
            && shootCooldown<=0.0f)
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
        instance2.transform.position = transform.position + new Vector3(headingVector.x * offset, headingVector.y * offset, -GlobalVars.RippleOffset);
    }

    public float GetID()
    {
        return playerID;
    }
}
