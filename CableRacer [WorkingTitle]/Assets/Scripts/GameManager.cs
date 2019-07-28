using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private UIManager _uiManager;
	private PlayerController_Fixed _playerController;
	private SoundManager _soundManager;

    void Start()
    {
		_uiManager = FindObjectOfType<UIManager>();
		_playerController = FindObjectOfType<PlayerController_Fixed>();
		_soundManager = FindObjectOfType<SoundManager>();

		_playerController.mySpeedTransform = 0f;
		_playerController.mySpeedRotation = 0f;

		_uiManager.TransitionIn();
		
		StartCoroutine(StartDelay());
	}

	IEnumerator StartDelay()
	{
		yield return new WaitForSeconds(3f);
		StartGame();
	}

	public void StartGame()
	{
		_playerController.mySpeedTransform = 4f;
		_playerController.mySpeedRotation = 4f;
		_soundManager.GameMusic();
	}
}
