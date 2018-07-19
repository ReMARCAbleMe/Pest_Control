using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehavior : MonoBehaviour {


    Rigidbody rigidbod;
    Vector3 vel;
    float speed;
    public bool isDangerous = false;
    public float force = 4;
    public float showSpeed;
	// Use this for initialization
	void Start ()
    {
        rigidbod = gameObject.GetComponent(typeof(Rigidbody)) as Rigidbody;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    private void FixedUpdate()
    {
        vel = rigidbod.velocity;
        speed = rigidbod.velocity.magnitude;
        showSpeed = speed;
        SpeedDetection();
        //print (speed);

        
    }

    public void SpeedDetection()
    {
        if (speed > force)
        {
            isDangerous = true;
        }

        if (speed < force)
        {
            isDangerous = false;
        }

    }
}
