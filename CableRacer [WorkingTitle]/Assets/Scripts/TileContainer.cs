using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileContainer : MonoBehaviour
{
	[Header("Setup")]
	[SerializeField] private List<Transform> _lanes = new List<Transform>();
	[SerializeField] private Material _mainMaterial;
	[SerializeField] private Transform _endConnector;
	[SerializeField] private Transform _startConnector;

    void Start()
    {
        foreach(var lane in _lanes)
		{
			lane.GetComponent<MeshRenderer>().material = _mainMaterial;
		}
    }
}
