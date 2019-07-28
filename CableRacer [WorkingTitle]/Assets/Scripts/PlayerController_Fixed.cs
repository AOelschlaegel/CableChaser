using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;

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

	public float TransformSpeed;
	public float startTimeTransform;

	public float RotationSpeed;
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
		float timerTransform = (Time.time - startTimeTransform) * TransformSpeed;
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
		float timerRotation = (Time.time - startTimeRotation) * RotationSpeed;
		transform.rotation = Quaternion.Lerp(startMarker.rotation, endMarker.rotation, timerRotation);

		switch(CurrentTileId)
		{
			case 30:

				RotationSpeed = 6f;
				TransformSpeed = 6f;

				_colorManager.ChangeMainColor();
				_colorManager.ChangeObstacleColor();

				break;

			case 60:

				RotationSpeed = 7f;
				TransformSpeed = 7f;

				_colorManager.ChangeMainColor();
				_colorManager.ChangeObstacleColor();
				break;

			case 90:

				RotationSpeed = 8f;
				TransformSpeed = 8f;

				_colorManager.ChangeMainColor();
				_colorManager.ChangeObstacleColor();

				_proceduralGenerator.ObstacleSpawnDivider = 4;

				break;

			case 120:

				RotationSpeed = 9f;
				TransformSpeed = 9f;

				_colorManager.ChangeMainColor();
				_colorManager.ChangeObstacleColor();
				break;

			case 150:

				RotationSpeed = 10f;
				TransformSpeed = 10f;

				_colorManager.ChangeMainColor();
				_colorManager.ChangeObstacleColor();

				_proceduralGenerator.ObstacleSpawnDivider = 5;
				break;

			case 250:

				RotationSpeed = 11f;
				TransformSpeed = 11f;

				_colorManager.ChangeMainColor();
				_colorManager.ChangeObstacleColor();
				break;

			case 400:

				RotationSpeed = 12f;
				TransformSpeed = 12f;

				_colorManager.ChangeMainColor();
				_colorManager.ChangeObstacleColor();

				_proceduralGenerator.ObstacleSpawnDivider = 6;
				break;
			
			case 600:
				
				RotationSpeed = 13f;
				TransformSpeed = 13f;

				_colorManager.ChangeMainColor();
				_colorManager.ChangeObstacleColor();
				break;
			
			case 800:
				
				RotationSpeed = 14f;
				TransformSpeed = 14f;

				_colorManager.ChangeMainColor();
				_colorManager.ChangeObstacleColor();
				break;
			
			case 1000:
				
				RotationSpeed = 18f;
				TransformSpeed = 18f;

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
