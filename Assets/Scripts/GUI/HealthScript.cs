using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour {

    public GameObject fly;
    private FlyMovement flyHealth;
    int remainingHealth;
    Text health;

    // Use this for initialization
    void Start ()
    {
        flyHealth = fly.GetComponent<FlyMovement>();
        health = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void FixedUpdate()
    {
        health.text = "Health: " + flyHealth.flyHealth;
    }
}
