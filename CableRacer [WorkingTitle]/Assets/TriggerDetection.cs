using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetection : MonoBehaviour
{
	public bool IsTriggered;
	public bool IsLastTriggered;

	private void Update()
	{
		IsLastTriggered = IsTriggered;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Controller")
		{
			IsTriggered = true;
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
