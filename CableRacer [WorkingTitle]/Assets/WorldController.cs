using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
	[SerializeField] private Transform _sceneRoot;
	[SerializeField] private PlayerController _playerController;

	public int Speed;
	public float Direction;
	private float _currentAngle;
	private List<float> _angles = new List<float>();

	public float speedDirection_X;
	public float speedDirection_Z;

	[HideInInspector]
	public float MovementSpeed;

	private void Awake()
	{
		MovementSpeed = 0.01f * Speed;
		Debug.Log(MovementSpeed);
	}

	void Update()
	{
		Debug.Log(_currentAngle);

		if (_playerController.isHit)
		{
			var currentTile = _playerController.HitTile;
			_currentAngle = currentTile.GetComponent<TileContainer>().Angle;
			if(_currentAngle != 0)
			{
				_angles.Add(_currentAngle);
			} 
		}

		switch (_currentAngle)
		{
			case 0:
				speedDirection_X = 3;
				speedDirection_Z = 1;
				break;

			case 15:
				speedDirection_X = 3;
				speedDirection_Z = 1;
				break;

			case 30:
				speedDirection_X = 3;
				speedDirection_Z = 1;
				break;

			case 45:
				speedDirection_X = 3;
				speedDirection_Z = 1;
				break;

			case 60:
				speedDirection_X = 3;
				speedDirection_Z = 1;
				break;

			case 75:
				speedDirection_X = 3;
				speedDirection_Z = 1;
				break;

			case 90:
				speedDirection_X = 3;
				speedDirection_Z = 1;
				break;

			case 105:
				speedDirection_X = 3;
				speedDirection_Z = 1;
				break;

			case 120:
				speedDirection_X = 3;
				speedDirection_Z = 1;
				break;

			case 135:
				speedDirection_X = 3;
				speedDirection_Z = 1;
				break;

			case 150:
				speedDirection_X = 3;
				speedDirection_Z = 1;
				break;

			case 165:
				speedDirection_X = 3;
				speedDirection_Z = 1;
				break;

			case 180:
				speedDirection_X = 3;
				speedDirection_Z = 1;
				break;

			case 195:
				speedDirection_X = 3;
				speedDirection_Z = 1;
				break;

			case 210:
				speedDirection_X = 3;
				speedDirection_Z = 1;
				break;

			case 225:
				speedDirection_X = 3;
				speedDirection_Z = 1;
				break;

			case 240:
				speedDirection_X = 3;
				speedDirection_Z = 1;
				break;

			case 255:
				speedDirection_X = 3;
				speedDirection_Z = 1;
				break;

			case 270:
				speedDirection_X = 3;
				speedDirection_Z = 1;
				break;

			case 285:
				speedDirection_X = 3;
				speedDirection_Z = 1;
				break;

			case 300:
				speedDirection_X = 3;
				speedDirection_Z = 1;
				break;

			case 315:
				speedDirection_X = 3;
				speedDirection_Z = 1;
				break;

			case 330:
				speedDirection_X = 3;
				speedDirection_Z = 1;
				break;

			case 345:
				speedDirection_X = 3;
				speedDirection_Z = 1;
				break;

			case 360:
				speedDirection_X = 3;
				speedDirection_Z = 1;
				break;

			default:
				Debug.Log("Cant't read Rotation");
				break;
		}


		if (_angles.Count > 0)
			Direction = _angles[_angles.Count -1];

		_sceneRoot.transform.Translate(Direction * Speed, 0, MovementSpeed);
	}
}
