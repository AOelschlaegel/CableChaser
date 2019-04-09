using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public GameObject HitLane;
	public GameObject HitTile;

	public bool isHit;

	public void Update()
	{
		RaycastHit hit;

		if (Physics.Raycast(transform.position,
			Vector3.down,
			out hit))
		{
			isHit = true;
			HitLane = hit.transform.gameObject;
			HitTile = HitLane.transform.parent.transform.parent.gameObject;
			Debug.Log("hitlane: " + HitLane.name + "hitTile:" + HitTile.name);
		}
	}
}
