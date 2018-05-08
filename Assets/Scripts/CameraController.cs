using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject player;
	public bool keepOffset;
    private Vector3 offset;

	// Use this for initialization
	void Start () {		
		offset = keepOffset ? transform.position - player.transform.position : Vector3.zero;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = player.transform.position + offset;
	}
}
