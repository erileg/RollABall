﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Camera mainCam;
    public Camera birdCam;
	public Camera fireworksCam;
    public Text countText;
    public Text winText;
    public ParticleSystem fountain;

    private PlayerController playerController;
    private int pickUpCount;

	void OnEnable()
	{
        EventManager.StartListening("PickUp", OnPickUp);
	}

    void OnDisable()
    {
        EventManager.StopListening("PickUp", OnPickUp);
    }

	void Awake()
	{
		mainCam.enabled = true;
		birdCam.enabled = false;
	}

	// Use this for initialization
	void Start()
    {
        pickUpCount = 0;
        SetCountText();
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("c"))
        {
            mainCam.enabled = !mainCam.enabled;
			birdCam.enabled = !birdCam.enabled;
        }

		if (!GameOver() && Camera.current)
        {
			var cam = Camera.current;
			if ((cam.transform.position - fireworksCam.transform.position).magnitude > 1f)
			{
				cam.transform.position = Vector3.Lerp(cam.transform.position, fireworksCam.transform.position, .3f * Time.deltaTime);
				cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, fireworksCam.transform.rotation, .3f * Time.deltaTime);
			}
		}

		if (GameOver() && Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


    void OnPickUp()
    {
        pickUpCount++;
        SetCountText();

        if (GameOver())
        {
            HandleGameOver();
        }
    }

    private void SetCountText()
    {
        countText.text = "Picked Up: " + pickUpCount;
    }

	private void HandleGameOver()
    {
		mainCam.GetComponent<CameraController>().enabled=false;

        fountain.gameObject.SetActive(true);
        winText.gameObject.SetActive(true);
    }

    private bool GameOver()
    {
        return pickUpCount >= 12;
    }

}
