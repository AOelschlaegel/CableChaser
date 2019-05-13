using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private UIManager _uiManager;
	private PlayerController_Level _playerControllerLevel;
	private PlayerController_Endless _playerControllerEndless;
	private SoundManager _soundManager;
	public bool EndlessMode;

	private float _speed;

    void Awake()
    {
		_uiManager = FindObjectOfType<UIManager>();

		// Look for which mode is Played

		
		if (FindObjectOfType<PlayerController_Endless>())
		{
			_playerControllerEndless = FindObjectOfType<PlayerController_Endless>();
			
			_speed = _playerControllerEndless.TransformSpeed;
			
			_playerControllerEndless.TransformSpeed = 0f;
			_playerControllerEndless.RotationSpeed = 0f;

			
			
			EndlessMode = true;
		}
		else
		{
			_playerControllerLevel = FindObjectOfType<PlayerController_Level>();

			_speed = _playerControllerLevel.TransformSpeed;
			
			_playerControllerLevel.TransformSpeed = 0f;
			_playerControllerLevel.RotationSpeed = 0f;
			EndlessMode = false;
		}
		
		_soundManager = FindObjectOfType<SoundManager>();

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
		if (EndlessMode)
		{
			_playerControllerEndless.TransformSpeed = _speed;
			_playerControllerEndless.RotationSpeed = _speed;
		}
		else
		{
			_playerControllerLevel.SetSpeed();
		}
		
		_soundManager.GameMusic();
	}
}
