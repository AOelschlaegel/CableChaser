using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerator : MonoBehaviour
{
	[SerializeField] private GameObject _straight_Long;
	[SerializeField] private GameObject _straight_Short;
	[SerializeField] private GameObject _curve_Right;
	[SerializeField] private GameObject _curve_Left;

	[SerializeField] private List<GameObject> _spawnedTiles;
	[SerializeField] private Transform _sceneRoot;

	private List<GameObject> _tiles;

	public int SpawnCount;

	void Start()
    {
		_tiles = new List<GameObject>();
		_spawnedTiles = new List<GameObject>();
		_tiles.Add(_straight_Long);
		_tiles.Add(_straight_Short);
		_tiles.Add(_curve_Right);
		_tiles.Add(_curve_Left);

		var initialTile = _tiles[0];
		var initialInstance = Instantiate(initialTile, initialTile.transform.position, Quaternion.identity);
		initialInstance.transform.SetParent(_sceneRoot);
		_spawnedTiles.Add(initialInstance);

		for (int i = 0; i < SpawnCount; i++)
		{
			var rand = Random.Range(0, _tiles.Count);
			var tile = _tiles[rand];
			var instanceStartpoint = tile.GetComponent<TileContainer>().StartConnector;
			var formerTile = _spawnedTiles[i].GetComponent<TileContainer>();
			var newInstance = Instantiate(tile, formerTile.EndConnector.transform.position, formerTile.EndConnector.transform.rotation);
			newInstance.transform.SetParent(_sceneRoot);
			_spawnedTiles.Add(newInstance);
		}
    }
}
