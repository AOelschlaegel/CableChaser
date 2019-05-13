using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleTriggerDetection : MonoBehaviour
{
	public bool IsTriggered = false;
	private GameSceneManager _sceneManager;
	private PlayerController_Endless _playerControllerEndless;
	private PlayerController_Level _playerControllerLevel;
	private UIManager _uIManager;
	private SoundManager _soundManager;
	private GameManager _gameManager;

	private void Start()
	{
		_sceneManager = FindObjectOfType<GameSceneManager>();
		_gameManager = FindObjectOfType<GameManager>();

		if (_gameManager.EndlessMode)
		{
			_playerControllerEndless = FindObjectOfType<PlayerController_Endless>();
		}
		
		else
		{
			_playerControllerLevel = FindObjectOfType<PlayerController_Level>();
		}
		
		
		_uIManager = FindObjectOfType<UIManager>();
		_soundManager = FindObjectOfType<SoundManager>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			IsTriggered = true;

			if (_gameManager.EndlessMode)
			{
				_playerControllerEndless.TransformSpeed = 0f;
				PlayerPrefs.SetInt("EndlessScore", _playerControllerEndless.CurrentTileId * 10);
				_sceneManager.LoadGameOverScene_Endless();
			}
			else
			{
				_playerControllerLevel.TransformSpeed = 0f;
				PlayerPrefs.SetInt("LevelScore", _playerControllerLevel.CurrentTileId * 10);
				_sceneManager.LoadGameOverScene_Level();
			}
			
			_uIManager.TransitionOut();
			_soundManager.CollisionSound();
		}
	}
}
