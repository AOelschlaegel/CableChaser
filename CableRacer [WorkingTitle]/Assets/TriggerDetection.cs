using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetection : MonoBehaviour
{
	public bool IsTriggered;

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag =="Player")
		{
			IsTriggered = true;
		}

		else
		{
			IsTriggered = false;
		}
	}
}
