using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMovement : MonoBehaviour {

    public CollisionDetectionMode collisionDetectionMode;

    //Baseline variables
    public float MoveSpeed = 10;
    public float RotateSpeed = 100;
    public float FlightSpeed = 50;
    public float Stamina = 30;
    public float restTime = 1;
    public float safetyTime = 6;
    public int flyHealth = 3;
    float Threshhold = 0;
    public bool isHit = false;
    //Grabs components from weapon game objects
    public GameObject weapon;
    private WeaponBehavior weaponBehavior;

    
    

    //Contain original values set in inspector
    float originalSpeed;
    float originalRestTime;
    float originalStamina;
    float originalSafetyTime;
    
    //GameObject variables
    Rigidbody rigidbod;
    Transform obj;
    Vector3 lastPos;
     

	// Use this for initialization
	void Start () {
        rigidbod = gameObject.GetComponent(typeof(Rigidbody)) as Rigidbody; //Calls for the GO's Rigidbody
        obj = GetComponent<Transform>();                                    //Calls for the GO's transform component
        lastPos = obj.position;                                             //Sets the LastPos var
        originalSpeed = MoveSpeed;                                          //Sets the OriginalSpeed var
        originalRestTime = restTime;                                        //Sets originalRestTime var
        originalStamina = Stamina;                                          //Sets originalStamina var
        originalSafetyTime = safetyTime;

        weaponBehavior = weapon.GetComponent<WeaponBehavior>();             //References other script to monitor weapon speed
    }
	
	// Update is called once per frame
	void Update () {

        Vector3 offset = obj.position - lastPos;        //Keeps track of change in Y position
        

        if (offset.y != Threshhold)                     //Activates when there is a change in the y postion
        {  

            Stamina -= Time.deltaTime;                  //Decreases stamina
            restTime = originalRestTime;                //resets rest timer to orignal ammount
            lastPos = obj.position;                     //Updates last Y Postion

        }

        if (offset.y == Threshhold && Stamina < originalStamina | offset.y <= Threshhold + 1 && offset.y >= Threshhold - 1 && Stamina < originalStamina)     //Checks to see if the Y position has changed and if there is any missing stamina
        {
            
            restTime -= Time.deltaTime;                 //Decreases time to rest
            lastPos = obj.position;                     //Updates last Y Position
            
        }
        Movement();
        rest();
        health();

	}

    void Movement()
    {   //BasicMovement
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate((Vector3.right) * -MoveSpeed * Time.deltaTime);//Move Forward
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate((Vector3.right) * MoveSpeed * Time.deltaTime);  //Move Backward
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate((Vector3.up) * RotateSpeed * Time.deltaTime);     //Turn Left
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate((Vector3.down) * RotateSpeed * Time.deltaTime);   //Turn Right
        }
        //Flight controls so far
        if (Input.GetKey(KeyCode.Space) & (Stamina > 0))                     //Looks for spacebar to be held as well as checking stamina level
        {
            
            transform.Translate((Vector3.up) * MoveSpeed * Time.deltaTime); //Allows Upward Flight
            rigidbod.constraints = RigidbodyConstraints.FreezePositionY;    //Keeps from being dragged back down by residule acceleration
            rigidbod.constraints = RigidbodyConstraints.FreezeRotation;     //Keeps rotation contraints on
            rigidbod.useGravity = false;                                    //Turns Gravity off
            Stamina -= Time.deltaTime;                                      //Drains Stamina
        }
        //turns gravity on after flying is completed or stamina runs out
        if (Input.GetKeyUp(KeyCode.Space) | (Stamina <= 0))                 //Searches for if spacebar is up OR stamina is drained completely
        {
            rigidbod.constraints &= ~RigidbodyConstraints.FreezePositionY;  //Unconstrains Y position
            rigidbod.constraints = RigidbodyConstraints.FreezeRotation;     //Refreezes Rotation Contraints that above line turns off for some reason
            rigidbod.useGravity = true;                                     //Reactivates Gravity
        }
        //Manual controls for flying down
        if (Input.GetKey(KeyCode.LeftControl))
        {
            transform.Translate((Vector3.down) * MoveSpeed * Time.deltaTime);//Fly Downward
        }

    }
    //Increases speed when flying
    private void OnCollisionExit(Collision collision)
    {
        MoveSpeed = FlightSpeed;
    }
    //Returns normal speed when grounded
    private void OnCollisionEnter(Collision collision)
    {
        MoveSpeed = originalSpeed;
    }
    //
    private void OnTriggerEnter(Collider other)
    {
        //Makes sure the weapon GameObject is actually being used as a weapon before deaaling damage
        if (weaponBehavior.isDangerous == true && other.gameObject.CompareTag("Weapon") && safetyTime == originalSafetyTime) //takes damige if weapon is moving fast enough, is tagged weapon, and if safe time has expired
        {
            flyHealth -= 1;
            isHit = true;
            
        }
    }
    //Function to reset stamina after being grounded for a set ammount of time.
    void rest ()
    {

        if (restTime <= 0)
        
        {
            Stamina = originalStamina;
            restTime = originalRestTime;
        }

    }

    void health ()
    {

        if (flyHealth <= 0)
        {
            Destroy(gameObject);
        }

        if (isHit == true)
        {
            safetyTime -= Time.deltaTime;
        }

        if (safetyTime <= 0)
        {
            isHit = false;
            safetyTime = originalSafetyTime;   
        }


    }

    }

