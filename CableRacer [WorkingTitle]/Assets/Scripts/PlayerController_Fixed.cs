using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController_Fixed : MonoBehaviour
{
	[SerializeField] private ProceduralGenerator _proceduralGenerator;
	[SerializeField] private SoundManager _soundManager;

	[SerializeField] private TriggerDetection _triggerLeft;
	[SerializeField] private TriggerDetection _triggerRight;

	[SerializeField] private TextMeshProUGUI _score;

	private ColorManager _colorManager;

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
		_soundManager = FindObjectOfType<SoundManager>();
		_colorManager = FindObjectOfType<ColorManager>();
		endMarker = endMarkerScript.EndConnectors[LaneId];

		startTimeTransform = Time.time;
		startTimeRotation = Time.time;

		_score.enabled = false;
	}

	public void Update()
	{


		if (CurrentTileId > 0)
		{
			_score.enabled = true;
			_score.text = (CurrentTileId * 10).ToString();
		}

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

		switch(CurrentTileId)
		{
			case 30:

				mySpeedRotation = 6f;
				mySpeedTransform = 6f;

				_colorManager.ChangeMainColor();
				_colorManager.ChangeObstacleColor();

				break;

			case 60:

				mySpeedRotation = 7f;
				mySpeedTransform = 7f;

				_colorManager.ChangeMainColor();
				_colorManager.ChangeObstacleColor();
				break;

			case 90:

				mySpeedRotation = 8f;
				mySpeedTransform = 8f;

				_colorManager.ChangeMainColor();
				_colorManager.ChangeObstacleColor();

				_proceduralGenerator.ObstacleSpawnDivider = 4;

				break;

			case 120:

				mySpeedRotation = 9f;
				mySpeedTransform = 9f;

				_colorManager.ChangeMainColor();
				_colorManager.ChangeObstacleColor();
				break;

			case 150:

				mySpeedRotation = 10f;
				mySpeedTransform = 10f;

				_colorManager.ChangeMainColor();
				_colorManager.ChangeObstacleColor();

				_proceduralGenerator.ObstacleSpawnDivider = 5;
				break;

			case 250:

				mySpeedRotation = 11f;
				mySpeedTransform = 11f;

				_colorManager.ChangeMainColor();
				_colorManager.ChangeObstacleColor();
				break;

			case 400:

				mySpeedRotation = 12f;
				mySpeedTransform = 12f;

				_colorManager.ChangeMainColor();
				_colorManager.ChangeObstacleColor();

				_proceduralGenerator.ObstacleSpawnDivider = 6;
				break;
			
			case 600:
				
				mySpeedRotation = 13f;
				mySpeedTransform = 13f;

				_colorManager.ChangeMainColor();
				_colorManager.ChangeObstacleColor();
				break;
			
			case 800:
				
				mySpeedRotation = 14f;
				mySpeedTransform = 14f;

				_colorManager.ChangeMainColor();
				_colorManager.ChangeObstacleColor();
				break;
			
			case 1000:
				
				mySpeedRotation = 18f;
				mySpeedTransform = 18f;

				_colorManager.ChangeMainColor();
				_colorManager.ChangeObstacleColor();
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
