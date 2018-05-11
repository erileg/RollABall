using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
	public float speed;

	private Vector3 startPos, endPos, direction;

	private Rigidbody rb;
	private GameController gameController;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{

		var currentCam = Camera.current;

		if (currentCam)
		{
			// WASD and arrow keys
            var moveHorizontal = Input.GetAxis("Horizontal");
            var moveVertical = Input.GetAxis("Vertical");
            var movement = currentCam.transform.TransformDirection(new Vector3(moveHorizontal, 0.0f, moveVertical));
            rb.AddForce(movement * speed);

			// Mouse Support
			var mouseHoriztontal = Input.GetAxis("Mouse X");
			var mouseVertical = Input.GetAxis("Mouse Y");
			var mouseMovement = currentCam.transform.TransformDirection(new Vector3(mouseHoriztontal, 0, mouseVertical));
			mouseMovement.y = 0;
			rb.AddForce(mouseMovement * speed);

			// Mobile Touch Support
			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
			{
				var touchPos = Input.GetTouch(0).position;
				startPos = new Vector3(touchPos.x, 0, touchPos.y);
			}

			if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				var touchPos = Input.GetTouch(0).position;
				endPos = new Vector3(touchPos.x, 0, touchPos.y);
				direction = endPos - startPos;

				var touchMovement = currentCam.transform.TransformDirection(direction);
				rb.AddForce(direction * direction.magnitude * .002f);
			}

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
