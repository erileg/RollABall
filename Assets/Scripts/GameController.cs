using System.Collections;
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
	public AudioClip pickUpSound;

	private int pickUpCount;
	private bool fireworksCamPosReached;

	private void OnEnable()
	{
		EventManager.StartListening("PickUp", OnPickUp);
	}

	private void OnDisable()
	{
		EventManager.StopListening("PickUp", OnPickUp);
	}

	private void Start()
	{
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;


		if (SystemInfo.deviceType == DeviceType.Desktop)
		{
			mainCam.enabled = true;
			birdCam.enabled = false;
		}
		else
		{
			mainCam.enabled = false;
			birdCam.enabled = true;

		}


		pickUpCount = 0;
		SetCountText();
		fireworksCamPosReached = false;
	}

	private void Update()
	{
		if (Input.GetKeyDown("c"))
		{
			mainCam.enabled = !mainCam.enabled;
			birdCam.enabled = !birdCam.enabled;
		}

		if (Input.GetKeyDown("g"))
		{
			pickUpCount = 12;
			HandleGameOver();
		}

		if (GameOver() && Input.GetKey(KeyCode.Space))
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	private void LateUpdate()
	{
		if (GameOver() && Camera.current)
		{
			var cam = Camera.current;
			if ((cam.transform.position - fireworksCam.transform.position).magnitude > 1f && !fireworksCamPosReached)
			{
				cam.transform.position = Vector3.Lerp(cam.transform.position, fireworksCam.transform.position, .3f * Time.deltaTime);
				cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, fireworksCam.transform.rotation, .3f * Time.deltaTime);
			}
			else
			{
				fireworksCamPosReached = true;
				cam.transform.RotateAround(Vector3.zero, -Vector3.up, 10 * Time.deltaTime);
			}
		}
	}

	private void OnPickUp()
	{
		pickUpCount++;
		SetCountText();
		GetComponent<AudioSource>().Play();

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
		mainCam.GetComponent<CameraController>().enabled = false;

		fountain.gameObject.SetActive(true);
		winText.gameObject.SetActive(true);
	}

	private bool GameOver()
	{
		return pickUpCount >= 12;
	}

}
