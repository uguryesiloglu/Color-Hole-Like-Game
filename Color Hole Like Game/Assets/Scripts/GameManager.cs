using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager gameManager;
	public PlayerControler playerControler;
	public CameraController cameraController;
	public MenuManager menuUIManager;

	public bool started;
	public bool gameOver;

    public float gravity = 10f;
	//public float time = 10; // Total game time in seconds

	private void Awake()
	{
		if (!gameManager)
		{
			gameManager = this;
			DontDestroyOnLoad(this);
		}
	}
	private void Update()
	{
		if (!started || gameOver)
		{
			return;
		}
    }

	public void Play()
	{
		started = true;
	}

	public void GameOver()
	{
		gameOver = true;
		menuUIManager.gameplay.SetActive(false);
		menuUIManager.gameOver.SetActive(true);
	}

	public void RestartScene()
	{
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    //We will use this in the bonus section.
    //void CountDown()
    //{
    //    time -= Time.deltaTime;

    //    if (time <= 1)
    //    {
    //        GameOver();
    //        menuUIManager.timerText.text = "00:00";
    //        return;
    //    }

    //    var minutes = Mathf.FloorToInt(time / 60f);
    //    var seconds = Mathf.FloorToInt(time - minutes * 60f);
    //    menuUIManager.timerText.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    //}
    
}