using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
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

		pickUpCount = 0;
		SetCountText();
		fireworksCamPosReached = false;
	}

	private void Update()
	{
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
		fountain.gameObject.SetActive(true);
		winText.gameObject.SetActive(true);
	}

	private bool GameOver()
	{
		return pickUpCount >= 12;
	}

}
