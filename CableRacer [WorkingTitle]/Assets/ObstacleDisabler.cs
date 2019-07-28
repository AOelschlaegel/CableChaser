using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDisabler : MonoBehaviour
{
	[SerializeField] private GameObject _obstacle;

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Player")
		{
			Debug.Log("Collied");
			_obstacle.SetActive(false);
		}
	}
}
