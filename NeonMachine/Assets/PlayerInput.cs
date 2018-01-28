using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour {

	
    private Rigidbody2D rb;
    private ParticleSystem particleSys;
    [SerializeField]
    int playerID;
    [SerializeField]
    float maxHealth;
    [SerializeField]
    float planetCollisionForce = 200.0f;

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
    float maxThrusterFuel = 100.0f;
    [SerializeField]
    float thrusterCoolDown;
    [SerializeField]
    float forceMult;

    Vector2 direction;

    float maxVelocity = 5.0f;
    float thrusterFuel;
    float shootCooldown = 0.0f;
    float scale;
    float currentThrusterCoolDown=0.0f;
    int burst = 0;
    float accumulator = 0.0f;
    bool isBoosting = false;
    Vector2 headingVector;
    float rotation;
    Vector3 position;
    float health;

    [HideInInspector]
    public bool CanShoot { get { return shootCooldown <= 0.0f; } }
    [HideInInspector]
    public float GetHealth { get { return health; } }
    [HideInInspector]
    public float GetFuel { get { return thrusterFuel; } }

    // Use this for initialization
    void Start ()
	{
        health = maxHealth;
        thrusterFuel = maxThrusterFuel;
	    rb = GetComponent<Rigidbody2D>();
	    particleSys=GetComponent<ParticleSystem>();
        direction = new Vector2(0,0);
	    scale = Mathf.Max(transform.localScale.x, transform.localScale.y);
        print(health);
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
	        thrusterFuel += Time.deltaTime * 45;
            thrusterFuel = Mathf.Clamp(thrusterFuel, 0, maxThrusterFuel);
	        particleSys.Stop();

	    }
        
        //Move and Rotate the player
        if (direction.magnitude != 0.0f)
	    {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, angle)),
                    Time.deltaTime*500);  
	    }

        isBoosting = false;
        if (Input.GetAxis("ThrusterGamePad" + playerID) > 0.0f && thrusterFuel >= 0.0f)
        {
            rb.AddForce(new Vector2(Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad)) * Input.GetAxis("ThrusterGamePad" + playerID) * forceMult * scale);
            thrusterFuel -= Time.deltaTime * 10;
            if (particleSys.isPlaying == false)
            {
                particleSys.Play();
            }
            isBoosting = true;
        }

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
        rotation = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
        headingVector = new Vector2(Mathf.Cos(rotation), Mathf.Sin(rotation));
        position = transform.position;
    }
    
    void Burst()
    {
        float tempRotation = rotation;
        float spread = Mathf.Deg2Rad * spreadInDegrees;

        tempRotation -= spread / 2.0f;

        for (int i = 0; i < amount; i++)
        {
            GameObject instance = Instantiate(emittedObject);
            instance.gameObject.GetComponent<SpriteRenderer>().color = color;
            
            instance.transform.position = position + new Vector3(headingVector.x * offset, headingVector.y * offset, 0);
            instance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, (tempRotation + (spread / amount) * i)) * Mathf.Rad2Deg);
            instance.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(tempRotation + (spread / amount) * i), Mathf.Sin(tempRotation + (spread / amount) * i)) * speed;
            instance.GetComponent<ProjectileScript>().ownerID = playerID;
        }
        GameObject instance2 = Instantiate(ripple);
        instance2.transform.position = transform.position + new Vector3(headingVector.x * offset, headingVector.y * offset, -GlobalVars.RippleOffset);
        Ripple rippleInst = instance2.GetComponent<Ripple>();
        rippleInst.ttl = 0.5f;
        rippleInst.startRadius = 0.1f;
        rippleInst.endRadius = 1.0f;
        rippleInst.startDistortion = 5.0f;
        rippleInst.endDistortion = 0.0f;
    }

    public float GetID()
    {
        return playerID;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 9)
        {
            Vector2 normal = other.transform.position - transform.position;
            normal.Normalize();
            Rigidbody2D rBody = GetComponent<Rigidbody2D>();
            rBody.velocity = (rBody.velocity.normalized - 2 * Vector2.Dot(rBody.velocity.normalized, normal) * normal) * planetCollisionForce;

            GameObject instance = Instantiate(ripple);
            instance.transform.position = transform.position + new Vector3(0, 0, -GlobalVars.RippleOffset);
            Ripple rippleInst = instance.GetComponent<Ripple>();
            rippleInst.ttl = 0.2f;
            rippleInst.startRadius = 0.1f;
            rippleInst.endRadius = 1.5f;
            rippleInst.startDistortion = 9.0f;
            rippleInst.endDistortion = 0.0f;
        }
        else if (other.gameObject.layer == 8)
        {
            if (other.gameObject.GetComponent<ProjectileScript>().ownerID != playerID)
            {
                Destroy(other.gameObject);
                health -= 0.5f;

                if (Random.value < .4f)
                {
                    GameObject instance = Instantiate(ripple);
                    instance.transform.position = other.transform.position + new Vector3(0, 0, -GlobalVars.RippleOffset);
                    Ripple rippleInst = instance.GetComponent<Ripple>();
                    rippleInst.ttl = 0.1f + Random.value * 0.2f;
                    rippleInst.startRadius = 0.02f;
                    rippleInst.endRadius = 0.2f + Random.value * 0.1f;
                    rippleInst.startDistortion = 3.0f + Random.value * 2.0f;
                    rippleInst.endDistortion = 0.0f;
                }
            }
        }
    }
}
