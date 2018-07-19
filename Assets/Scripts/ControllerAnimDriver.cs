using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerAnimDriver : MonoBehaviour {

    private SteamVR_Controller.Device Controller;
    Animator Animate;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Controller.GetHairTriggerDown ())
        {



        }
       
}
}
