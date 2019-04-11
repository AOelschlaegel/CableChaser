using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleContainer : MonoBehaviour
{
	[Header("Setup")]
	[SerializeField] private List<GameObject> _obstacles = new List<GameObject>();
	[SerializeField] private Material _obstacleMaterial;
	public GameObject ChosenObstacle;

	void Update()
	{
		foreach (var obstacle in _obstacles)
		{
			obstacle.GetComponent<MeshRenderer>().material = _obstacleMaterial;
		}
	}

	private void Start()
	{
		for (int i = 0; i < 5; i++)
		{
			var rand = Random.Range(0, 4);
			_obstacles[rand].SetActive(false);
		}
	}
}
