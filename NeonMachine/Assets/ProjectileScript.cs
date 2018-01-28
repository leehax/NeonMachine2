using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

    public float ttl = 5;
    [SerializeField]
    float fadeTime = 0.5f;
    public int ownerID;
    [SerializeField]
    GameObject ripple;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ttl -= Time.deltaTime;
        if (ttl <= 0.0f)
        {
            GlobalVars.inactiveProjectiles.Add(gameObject);
            gameObject.SetActive(false);
            return;
        }
        Color color = GetComponent<SpriteRenderer>().color;
        if (ttl <= fadeTime)
            GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, Mathf.Lerp(0, 1, ttl * 1.0f / fadeTime));
        else
            GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 9)
        {
            if (Random.value < 0.25f)
            {
                GameObject instance = Instantiate(ripple);
                instance.transform.position = new Vector3(transform.position.x, transform.position.y, instance.transform.position.z - GlobalVars.RippleOffset);
                Ripple rippleInst = instance.GetComponent<Ripple>();
                rippleInst.ttl = Random.value * 0.3f + 0.5f;
                rippleInst.startRadius = 0.05f;
                rippleInst.endRadius = Random.value * 0.2f + 0.2f;
                rippleInst.startDistortion = 10.0f;
                rippleInst.endDistortion = 0.0f;
            }

            GlobalVars.inactiveProjectiles.Add(gameObject);
            gameObject.SetActive(false);
            return;
            //Vector2 normal = other.transform.position - transform.position;
            //normal.Normalize();
            //Rigidbody2D rBody = GetComponent<Rigidbody2D>();
            //rBody.velocity = rBody.velocity - 2 * Vector2.Dot(rBody.velocity, normal) * normal;
        }
        else if (other.gameObject.layer == 10)
        {

        }
    }
}
