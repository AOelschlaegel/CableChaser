using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileContainer : MonoBehaviour
{
	[Header("Setup")]
	[SerializeField] private List<Transform> _lanes = new List<Transform>();
	[SerializeField] private Material _mainMaterial;
	public Transform EndConnector;
	public List<Transform> EndConnectors;
	public Transform StartConnector;
	public float Angle;
	public int Id;


    void Update()
    {
        foreach(var lane in _lanes)
		{
			lane.GetComponent<MeshRenderer>().material = _mainMaterial;
		}
    }
}
