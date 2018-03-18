using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public Level[] levels;
    BallLogics ball;
    PlayerLogics player;
    GameplayObject[] objects;

    public static int numBricks = 30;

    private void Awake()
    {
        ball = GameObject.FindObjectOfType<BallLogics>();
        player = GameObject.FindObjectOfType<PlayerLogics>();
    }

    // Use this for initialization
    void Start () {
        numBricks = 30;
        PlayerLogics.lives = 3;
        ResetGame();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResetGame()
    {
        InputManager.hasStarted = false;
        ball.ResetGame();
        player.ResetGame();
        
    }

    public static bool CheckWin()
    {
        if (numBricks <= 0)
            return true;

        return false;
    }

    public bool CheckGameOver()
    {
        if (PlayerLogics.lives <= 0)
        {
            return true;
        }

        return false;
    }
}
