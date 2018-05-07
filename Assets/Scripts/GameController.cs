using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Camera mainCam;
    public Camera birdCam;
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

    // Use this for initialization
	void Start()
    {
        pickUpCount = 0;
        SetCountText();
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            mainCam.enabled = !mainCam.enabled;
            birdCam.enabled = !birdCam.enabled;
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
            handleGameOver();
        }
    }

    private void SetCountText()
    {
        countText.text = "Picked Up: " + pickUpCount;
    }

    private void handleGameOver()
    {
        fountain.gameObject.SetActive(true);
        winText.gameObject.SetActive(true);
    }

    private bool GameOver()
    {
        return pickUpCount >= 12;
    }

}
