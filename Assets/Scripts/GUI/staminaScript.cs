using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class staminaScript : MonoBehaviour {

    public GameObject fly;
    private FlyMovement flyStamina;
    Text stamina;

    // Use this for initialization
    void Start ()
    {
        flyStamina = fly.GetComponent<FlyMovement >();
        stamina = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        stamina.text = "Stamina: " + flyStamina.Stamina;
    }
}
