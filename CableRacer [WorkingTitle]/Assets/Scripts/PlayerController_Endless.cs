using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController_Endless : MonoBehaviour
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

		Debug.Log("CurrentID: " + CurrentTileId);

		switch(CurrentTileId)
		{
			case 30:

				RotationSpeed = 4f;
				TransformSpeed = 4f;

				_colorManager.ChangeMainColor();
				_colorManager.ChangeObstacleColor();

				break;

			case 60:

				RotationSpeed = 5f;
				TransformSpeed = 5f;

				_colorManager.ChangeMainColor();
				_colorManager.ChangeObstacleColor();
				break;

			case 90:

				RotationSpeed = 6f;
				TransformSpeed = 6f;

				_colorManager.ChangeMainColor();
				_colorManager.ChangeObstacleColor();

				_proceduralGenerator.ObstacleSpawnDivider = 4;

				break;

			case 120:

				RotationSpeed = 7f;
				TransformSpeed = 7f;

				_colorManager.ChangeMainColor();
				_colorManager.ChangeObstacleColor();
				break;

			case 150:

				RotationSpeed = 8f;
				TransformSpeed = 8f;

				_colorManager.ChangeMainColor();
				_colorManager.ChangeObstacleColor();

				_proceduralGenerator.ObstacleSpawnDivider = 5;
				break;

			case 250:

				RotationSpeed = 9f;
				TransformSpeed = 9f;

				_colorManager.ChangeMainColor();
				_colorManager.ChangeObstacleColor();
				break;

			case 400:

				RotationSpeed = 10f;
				TransformSpeed = 10f;

				_colorManager.ChangeMainColor();
				_colorManager.ChangeObstacleColor();

				_proceduralGenerator.ObstacleSpawnDivider = 6;
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
