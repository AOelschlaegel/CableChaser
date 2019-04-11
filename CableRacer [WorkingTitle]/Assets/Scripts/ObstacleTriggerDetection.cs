using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTriggerDetection : MonoBehaviour
{
	public bool IsTriggered = false;
	private GameSceneManager _sceneManager;
	private PlayerController_Fixed _playerController;
	private UIManager _uIManager;

	private void Start()
	{
		_sceneManager = FindObjectOfType<GameSceneManager>();
		_playerController = FindObjectOfType<PlayerController_Fixed>();
		_uIManager = FindObjectOfType<UIManager>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			IsTriggered = true;
			_playerController.mySpeedTransform = 0f;
			_sceneManager.LoadGameOverScene();
			_uIManager.TransitionOut();
		}
	}
}
