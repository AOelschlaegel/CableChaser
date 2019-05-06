using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController_Level : MonoBehaviour
{
	[SerializeField] private List<GameObject> _levels;
	[SerializeField] private GameObject _levelPrefab;
	[SerializeField] private Transform _levelSceneRoot;
	[SerializeField] private SoundManager _soundManager;

	[SerializeField] private TriggerDetection _triggerLeft;
	[SerializeField] private TriggerDetection _triggerRight;

	[SerializeField] private TextMeshProUGUI _score;

	private ColorManager _colorManager;
	private LevelContainer _level;

	public int CurrentTileId;
	public int LaneId;

	public Transform startMarker;
	public Transform endMarker;

	public GameObject CameraRig;

	public float TransformSpeed;
	public float StartTimeTransform;

	public float RotationSpeed;
	public float StartTimeRotation;

	public bool SpeedIncrement;

	public void Start()
	{
		//TODO make this work
		
		var levelProgress = PlayerPrefs.GetInt("LevelProgress") - 1;
		_levelPrefab = _levels[0];
		_level = _levelPrefab.GetComponent<LevelContainer>();

		var levelInstance = Instantiate(_level, _levelSceneRoot);
		
		startMarker = _level.Tiles[0].transform;
		TileContainer endMarkerScript = _level.Tiles[1].GetComponent<TileContainer>();
		_soundManager = FindObjectOfType<SoundManager>();
		_colorManager = FindObjectOfType<ColorManager>();
		endMarker = endMarkerScript.EndConnectors[LaneId];

		StartTimeTransform = Time.time;
		StartTimeRotation = Time.time;

		_score.enabled = false;
	}

	public void Update()
	{
		if (CurrentTileId > 0)
		{
			_score.enabled = true;
			_score.text = (CurrentTileId * 10).ToString();
		}
		
		Debug.Log(PlayerPrefs.GetInt("LevelProgress"));
		
		ControllerInput();

		// Lerp Transform
		//
		// Distance moved = time * speed.
		float timerTransform = (Time.time - StartTimeTransform) * TransformSpeed;
		// Set our position as a fraction of the distance between the markers.
		transform.position = Vector3.Lerp(startMarker.position, endMarker.position, timerTransform);
		
		//TODO make this work
		
		if (timerTransform >= 1)
		{
			if (CurrentTileId >= _level.Tiles.Count-1)
			{
				Debug.Log("Level Over");
				var currentLevelProgress = 0;
				StartCoroutine(IncreaseLevel(currentLevelProgress));
				return;
			}
			
			else
			{
				CurrentTileId++;
				startMarker = endMarker;
				TileContainer endMarkerScript = _level.Tiles[CurrentTileId+1].GetComponent<TileContainer>();
				endMarker = endMarkerScript.EndConnectors[LaneId];
				StartTimeTransform = Time.time;
				StartTimeRotation = Time.time;
			}
		}

		// Lerp Rotation
		//
		float timerRotation = (Time.time - StartTimeRotation) * RotationSpeed;
		transform.rotation = Quaternion.Lerp(startMarker.rotation, endMarker.rotation, timerRotation);

		if (SpeedIncrement)
		{
			switch (CurrentTileId)
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

					//_proceduralGenerator.ObstacleSpawnDivider = 4;

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

					//_proceduralGenerator.ObstacleSpawnDivider = 5;
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

					//_proceduralGenerator.ObstacleSpawnDivider = 6;
					break;
			}
		}
	}

	public void SetSpeed()
	{
		TransformSpeed = _level.Speed;
		RotationSpeed = _level.Speed;
	}
	

	void ControllerInput()
	{
		var tileContainer = _level.Tiles[CurrentTileId].GetComponent<TileContainer>();
		if (_triggerLeft.IsTriggered && _triggerLeft.IsTriggered != _triggerLeft.IsLastTriggered)
		{
			LaneId++;
			if (LaneId >= tileContainer.EndConnectors.Count)
			{
				LaneId = 0;
			}
		}

		if (_triggerRight.IsTriggered && _triggerRight.IsTriggered != _triggerRight.IsLastTriggered)
		{
			LaneId -= 1;
			if (LaneId < 0)
			{
				LaneId = tileContainer.EndConnectors.Count - 1;
			}
		}
	}

	private static IEnumerator IncreaseLevel(int currentLevelProgress)
	{
		yield return  new WaitForSeconds(0.1f);
		PlayerPrefs.SetInt("LevelProgress", currentLevelProgress+1);
	}
}
