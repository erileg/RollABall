using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PlayerController : MonoBehaviour
{
	public float speed;

	private Vector3 startPos, endPos, direction;
	private float startTime;

	private Rigidbody rb;
	private GameController gameController;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	protected void FixedUpdate()
	{
		var cam = Camera.current;

		if (cam)
		{
			handleKeys(cam);

			handleMouse(cam);

			handleTouch(cam);

			handleAccelerometer(cam);
		}
	}
    
	private void handleKeys(Camera cam)
    {
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");
        var movement = cam.transform.TransformDirection(new Vector3(moveHorizontal, 0, moveVertical));
        
		rb.AddForce(movement * speed);
    }

	private void handleMouse(Camera cam)
	{
		var moveHoriztontal = Input.GetAxis("Mouse X");
		var moveVertical = Input.GetAxis("Mouse Y");
		var movement = cam.transform.TransformDirection(new Vector3(moveHoriztontal, 0, moveVertical));
        movement.y = 0;
        
		rb.AddForce(movement * speed);
	}

	private void handleAccelerometer(Camera cam)
    {
		var moveHoriztontal = Input.acceleration.x;
		var moveVertical = Input.acceleration.y;
		var movement = cam.transform.TransformDirection(new Vector3(moveHoriztontal, 0, moveVertical));
		movement.y = 0;

        rb.AddForce(movement * speed * 3);
    }

	private void handleTouch(Camera cam)
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startPos = Input.GetTouch(0).position;
            startTime = Time.time;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endPos = Input.GetTouch(0).position;
            direction = endPos - startPos;
			var movement = cam.transform.TransformDirection(direction);
			movement.y = 0;

			var intervall = Time.time - startTime;

			rb.AddForce(movement / intervall / 1.5f);
        }
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Pick Up"))
		{
			other.gameObject.SetActive(false);
			EventManager.TriggerEvent("PickUp");
		}
	}
}
