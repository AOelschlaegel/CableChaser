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

		if (_angles.Count > 0)
			Direction = _angles[_angles.Count -1];

		_sceneRoot.transform.Translate(Direction * Speed, 0, MovementSpeed);
	}
}
