using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    NewTimeScaleManager tm;
    float newTimeScale;
    float lerpSpeed;
    PlayerLogics player;
    BallLogics ball;

    public static bool hasStarted = false;

    // Use this for initialization
    void Start () {
        ball = GameObject.FindObjectOfType<BallLogics>();
        player = GetComponent<PlayerLogics>();
        tm = NewTimeScaleManager.instance;
    }
	
	// Update is called once per frame
	void Update () {



        if (Input.GetKey(KeyCode.RightArrow))
        {
            player.SetMoving(true, 0);

            newTimeScale = 1f;
            lerpSpeed = 10f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            player.SetMoving(true, 1);
            newTimeScale = 1f;
            lerpSpeed = 10f;
        }
        else
        {
            player.SetMoving(false, 0);
            newTimeScale = 0f;
            lerpSpeed = 5f;
        }



        if (hasStarted)
        {
            tm.SetCustomTimeScale(Mathf.Lerp(tm.GetCustomTimeScale(), newTimeScale, Time.deltaTime * lerpSpeed));
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            hasStarted = true;
            ball.BallInGame();
        }

        
        //Debug.Log(tm.GetCustomTimeScale());
    }
}
