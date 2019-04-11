using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	[SerializeField] private AudioClip _laneSwitch;
	[SerializeField] private AudioClip _preDrop;
	[SerializeField] private AudioClip _gameMusic;
	[SerializeField] private AudioClip _collision;


	[SerializeField] private AudioSource _musicSource;
	[SerializeField] private AudioSource _effectSource;

    void Start()
    {
		_musicSource.clip = _preDrop;
		_musicSource.Play();
	}

	public void GameMusic()
	{
		_musicSource.clip = _gameMusic;
		_musicSource.Play();
	}

	public void LaneSwitch()
	{
		_effectSource.clip = _laneSwitch;
		_effectSource.Play();
	}

	public void CollisionSound()
	{
		_effectSource.clip = _collision;
		_effectSource.Play();
	}
}
