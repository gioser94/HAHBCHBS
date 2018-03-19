using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayObject : MonoBehaviour {

    public int lives;


	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChekcDeath()
    {
        lives--;
        if (lives <= 0)
        {
            GameManager.numBricks--;
            Destroy(this.gameObject);
        }
            
    }

}
