using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleContainer : MonoBehaviour
{
	[Header("Setup")]
	[SerializeField] private List<GameObject> _obstacles = new List<GameObject>();
	[SerializeField] private Material _obstacleMaterial;

    // Update is called once per frame
    void Update()
    {
		foreach (var obstacle in _obstacles)
		{
			obstacle.GetComponent<MeshRenderer>().material = _obstacleMaterial;
		}
	}
}
