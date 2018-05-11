using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private Vector3 startPos, endPos, direction;
    private float touchTimeStart, touchTimeEnd, timeInterval;

    private Rigidbody rb;
    private GameController gameController;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touchTimeStart = Time.time;
            var touchPos = Input.GetTouch(0).position;
            startPos = new Vector3(touchPos.x, 0, touchPos.y);
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            touchTimeEnd = Time.time;
            var touchPos = Input.GetTouch(0).position;
            endPos = new Vector3(touchPos.x, 0, touchPos.y);
            timeInterval = touchTimeEnd - touchTimeStart;
            direction = endPos - startPos;
            rb.AddForce(direction * direction.magnitude * .002f);
        }

        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        var currentCam = Camera.current;
        if (currentCam)
        {
            Vector3 movement = currentCam.transform.TransformDirection(new Vector3(moveHorizontal, 0.0f, moveVertical));
            rb.AddForce(movement * speed);
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
