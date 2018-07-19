using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

    public GameObject fly;
    //public GameObject manager;
    private FlyMovement flyMovement;
   // private GameMngr gameMngr;
    private Component HealthText;
    public float totalTime = 150f;
    int minutes;
    int seconds;
    Text health;
    Text timer;

	// Use this for initialization
	void Start ()
    {
        //flyMovement = fly.GetComponent<FlyMovement>();
        //gameMngr = manager.GetComponent<GameMngr>();
        // health = GetComponentInChildren<Text>();
        timer = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        totalTime -= Time.deltaTime;
        UpdateLevelTimer(totalTime);

        if (totalTime <= 0)
        {
        
        }
	}

    private void FixedUpdate()
    {
      // flyMovement = fly.GetComponent<FlyMovement>();
       //health.text = "Health:" + flyMovement.flyHealth;
    }

    public void UpdateLevelTimer(float totalSeconds)
    {
        minutes = Mathf.FloorToInt(totalSeconds / 60f);
        seconds = Mathf.FloorToInt(totalSeconds % 60f);

        string formatedSeconds = seconds.ToString();

        if (seconds == 60)
        {
            seconds = 0;
            minutes += 1;
        }

        timer.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
