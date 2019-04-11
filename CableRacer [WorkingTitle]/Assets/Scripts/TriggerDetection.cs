using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetection : MonoBehaviour
{
	public bool IsTriggered;
	public bool IsLastTriggered;
	private SoundManager _soundManager;

	private void Start()
	{
		_soundManager = FindObjectOfType<SoundManager>();
	}

	private void Update()
	{
		IsLastTriggered = IsTriggered;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Controller")
		{
			IsTriggered = true;
			_soundManager.LaneSwitch();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Controller")
		{
			IsTriggered = false;
		}
	}
}
