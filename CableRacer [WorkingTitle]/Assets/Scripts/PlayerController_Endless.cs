using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Endless : MonoBehaviour
{
	[SerializeField] private ProceduralGenerator _proceduralGenerator;
	[SerializeField] private SoundManager _soundManager;

	[SerializeField] private TriggerDetection _triggerLeft;
	[SerializeField] private TriggerDetection _triggerRight;

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

	[Header("Screen")]
	public bool isScreen;
	[SerializeField] private float _speed;


	public void Start()
	{
		startMarker = _proceduralGenerator.TilePool[CurrentTileId].transform;
		TileContainer endMarkerScript = _proceduralGenerator.TilePool[CurrentTileId + 1].GetComponent<TileContainer>();
		_soundManager = FindObjectOfType<SoundManager>();
		_colorManager = FindObjectOfType<ColorManager>();
		endMarker = endMarkerScript.EndConnectors[LaneId];

		startTimeTransform = Time.time;
		startTimeRotation = Time.time;
	}

	public void Update()
	{
<<<<<<< HEAD:CableRacer [WorkingTitle]/Assets/Scripts/PlayerController_Endless.cs

		if (isScreen)
		{
			TransformSpeed = _speed;
			RotationSpeed = _speed;
		}
		

		if (CurrentTileId > 0)
		{
			_score.enabled = true;
			_score.text = (CurrentTileId * 10).ToString();
		}

=======
>>>>>>> parent of af5eb60... Release 0.1:CableRacer [WorkingTitle]/Assets/Scripts/PlayerController_Fixed.cs
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
			TileContainer endMarkerScript = _proceduralGenerator.TilePool[CurrentTileId + 1].GetComponent<TileContainer>();
			endMarker = endMarkerScript.EndConnectors[LaneId];
			startTimeTransform = Time.time;
			startTimeRotation = Time.time;
		}

		// Lerp Rotation
		//
		float timerRotation = (Time.time - startTimeRotation) * RotationSpeed;
		transform.rotation = Quaternion.Lerp(startMarker.rotation, endMarker.rotation, timerRotation);

		if (!isScreen)
		{
<<<<<<< HEAD:CableRacer [WorkingTitle]/Assets/Scripts/PlayerController_Endless.cs
			switch (CurrentTileId)
			{
				case 30:
=======
			case 25:
>>>>>>> parent of af5eb60... Release 0.1:CableRacer [WorkingTitle]/Assets/Scripts/PlayerController_Fixed.cs

					RotationSpeed = 4f;
					TransformSpeed = 4f;

					_colorManager.ChangeMainColor();
					_colorManager.ChangeObstacleColor();

					break;

<<<<<<< HEAD:CableRacer [WorkingTitle]/Assets/Scripts/PlayerController_Endless.cs
				case 60:
=======
			case 50:
>>>>>>> parent of af5eb60... Release 0.1:CableRacer [WorkingTitle]/Assets/Scripts/PlayerController_Fixed.cs

					RotationSpeed = 5f;
					TransformSpeed = 5f;

					_colorManager.ChangeMainColor();
					_colorManager.ChangeObstacleColor();
					break;

<<<<<<< HEAD:CableRacer [WorkingTitle]/Assets/Scripts/PlayerController_Endless.cs
				case 90:
=======
			case 100:
>>>>>>> parent of af5eb60... Release 0.1:CableRacer [WorkingTitle]/Assets/Scripts/PlayerController_Fixed.cs

					RotationSpeed = 6f;
					TransformSpeed = 6f;

<<<<<<< HEAD:CableRacer [WorkingTitle]/Assets/Scripts/PlayerController_Endless.cs
					_colorManager.ChangeMainColor();
					_colorManager.ChangeObstacleColor();

					_proceduralGenerator.ObstacleSpawnDivider = 4;

					break;

				case 120:
=======
				_colorManager.ChangeMainColor();
				_colorManager.ChangeObstacleColor();
				break;

			case 200:
>>>>>>> parent of af5eb60... Release 0.1:CableRacer [WorkingTitle]/Assets/Scripts/PlayerController_Fixed.cs

					RotationSpeed = 7f;
					TransformSpeed = 7f;

					_colorManager.ChangeMainColor();
					_colorManager.ChangeObstacleColor();
					break;

<<<<<<< HEAD:CableRacer [WorkingTitle]/Assets/Scripts/PlayerController_Endless.cs
				case 150:
=======
			case 400:
>>>>>>> parent of af5eb60... Release 0.1:CableRacer [WorkingTitle]/Assets/Scripts/PlayerController_Fixed.cs

					RotationSpeed = 8f;
					TransformSpeed = 8f;

<<<<<<< HEAD:CableRacer [WorkingTitle]/Assets/Scripts/PlayerController_Endless.cs
					_colorManager.ChangeMainColor();
					_colorManager.ChangeObstacleColor();

					_proceduralGenerator.ObstacleSpawnDivider = 5;
					break;

				case 250:
=======
				_colorManager.ChangeMainColor();
				_colorManager.ChangeObstacleColor();
				break;

			case 800:
>>>>>>> parent of af5eb60... Release 0.1:CableRacer [WorkingTitle]/Assets/Scripts/PlayerController_Fixed.cs

					RotationSpeed = 9f;
					TransformSpeed = 9f;

					_colorManager.ChangeMainColor();
					_colorManager.ChangeObstacleColor();
					break;

<<<<<<< HEAD:CableRacer [WorkingTitle]/Assets/Scripts/PlayerController_Endless.cs
				case 400:
=======
			case 1600:
>>>>>>> parent of af5eb60... Release 0.1:CableRacer [WorkingTitle]/Assets/Scripts/PlayerController_Fixed.cs

					RotationSpeed = 10f;
					TransformSpeed = 10f;

<<<<<<< HEAD:CableRacer [WorkingTitle]/Assets/Scripts/PlayerController_Endless.cs
					_colorManager.ChangeMainColor();
					_colorManager.ChangeObstacleColor();

					_proceduralGenerator.ObstacleSpawnDivider = 6;
					break;
			}
=======
				_colorManager.ChangeMainColor();
				_colorManager.ChangeObstacleColor();
				break;
>>>>>>> parent of af5eb60... Release 0.1:CableRacer [WorkingTitle]/Assets/Scripts/PlayerController_Fixed.cs
		}
	}


	void ControllerInput()
	{
		TileContainer markerScript = _proceduralGenerator.TilePool[CurrentTileId].GetComponent<TileContainer>();
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
