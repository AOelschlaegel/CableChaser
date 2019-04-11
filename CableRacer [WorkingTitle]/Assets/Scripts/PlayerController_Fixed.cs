using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Fixed : MonoBehaviour
{


	[SerializeField] private ProceduralGenerator _proceduralGenerator;

	[SerializeField] private TriggerDetection _triggerLeft;
	[SerializeField] private TriggerDetection _triggerRight;

	public int CurrentTileId = 0;
	public int LaneId = 0;

	public Transform startMarker;
	public Transform endMarker;

	public GameObject CameraRig;

	public float mySpeedTransform = 0.5f;
	public float startTimeTransform;

	public float mySpeedRotation = 2f;
	public float startTimeRotation;


	public void Start()
	{
		startMarker = _proceduralGenerator.SpawnedTiles[CurrentTileId].transform;
		TileContainer endMarkerScript = _proceduralGenerator.SpawnedTiles[CurrentTileId + 1].GetComponent<TileContainer>();
		endMarker = endMarkerScript.EndConnectors[LaneId];

		startTimeTransform = Time.time;
		startTimeRotation = Time.time;
	}

	public void Update()
	{
		ControllerInput();

		// Lerp Transform
		//
		// Distance moved = time * speed.
		float timerTransform = (Time.time - startTimeTransform) * mySpeedTransform;
		// Set our position as a fraction of the distance between the markers.
		transform.position = Vector3.Lerp(startMarker.position, endMarker.position, timerTransform);
		if (timerTransform >= 1)
		{
			CurrentTileId++;
			startMarker = endMarker;
			TileContainer endMarkerScript = _proceduralGenerator.SpawnedTiles[CurrentTileId + 1].GetComponent<TileContainer>();
			endMarker = endMarkerScript.EndConnectors[LaneId];
			startTimeTransform = Time.time;
			startTimeRotation = Time.time;
		}

		// Lerp Rotation
		//
		float timerRotation = (Time.time - startTimeRotation) * mySpeedRotation;
		transform.rotation = Quaternion.Lerp(startMarker.rotation, endMarker.rotation, timerRotation);

		Debug.Log("CurrentID: " + CurrentTileId);

		switch(CurrentTileId)
		{
			case 25:

				mySpeedRotation = 4f;
				mySpeedTransform = 4f;
				break;

			case 50:

				mySpeedRotation = 5f;
				mySpeedTransform = 5f;
				break;

			case 100:

				mySpeedRotation = 6f;
				mySpeedTransform = 6f;
				break;

			case 200:

				mySpeedRotation = 7f;
				mySpeedTransform = 7f;
				break;

			case 400:

				mySpeedRotation = 8f;
				mySpeedTransform = 8f;
				break;

			case 800:

				mySpeedRotation = 9f;
				mySpeedTransform = 9f;
				break;

			case 1600:

				mySpeedRotation = 10f;
				mySpeedTransform = 10f;
				break;
		}
	}


	void ControllerInput()
	{
		TileContainer markerScript = _proceduralGenerator.SpawnedTiles[CurrentTileId].GetComponent<TileContainer>();
		if (_triggerLeft.IsTriggered && _triggerLeft.IsTriggered != _triggerLeft.IsLastTriggered)
		{
			//Debug.Log("left == right");
			LaneId++;
			if (LaneId >= markerScript.EndConnectors.Count)
			{
				LaneId = 0;
			}

		}

		if (_triggerRight.IsTriggered && _triggerRight.IsTriggered != _triggerRight.IsLastTriggered)
		{
			//Debug.Log("right == left");
			LaneId -= 1;
			if (LaneId < 0)
			{
				LaneId = markerScript.EndConnectors.Count - 1;
			}
		}


	}

	
}
