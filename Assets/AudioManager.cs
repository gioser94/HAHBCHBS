using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (GameObject.FindObjectsOfType<AudioManager>().Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
