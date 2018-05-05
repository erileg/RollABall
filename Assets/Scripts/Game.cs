using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
    public Camera mainCam;
    public Camera birdCam;


	// Use this for initialization
	void Start () {
        Cursor.visible = false;
	}

	void Update()
	{
        if (Input.GetKeyDown(KeyCode.C))
        {
            mainCam.enabled = !mainCam.enabled;
            birdCam.enabled = !birdCam.enabled;
        }	
	}

}
