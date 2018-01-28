using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

    [SerializeField]
    float ttl = 5;
    [SerializeField]
    float fadeTime = 0.5f;
    public int ownerID;
    [SerializeField]
    GameObject ripple;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, ttl);
    }

    // Update is called once per frame
    void Update()
    {
        ttl -= Time.deltaTime;
        if (ttl <= fadeTime)
        {
            Color color = GetComponent<SpriteRenderer>().color;
            GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, Mathf.Lerp(0, 1, ttl * 1.0f / fadeTime));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 9)
        {
            Destroy(gameObject, .01f);
            if (Random.value < 0.25f)
            {
                GameObject instance = Instantiate(ripple);
                instance.transform.position = new Vector3(transform.position.x, transform.position.y, instance.transform.position.z - GlobalVars.RippleOffset);
                Ripple rippleInst = instance.GetComponent<Ripple>();
                rippleInst.ttl = Random.value * 0.3f + 0.5f;
                rippleInst.startRadius = 0.05f;
                rippleInst.endRadius = Random.value * 0.05f + 0.05f;
                rippleInst.startDistortion = 10.0f;
                rippleInst.endDistortion = 0.0f;
            }
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
