using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public float speed;

    private Rigidbody rb;
    private GameController gameController;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Camera currentCam = Camera.current;
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
