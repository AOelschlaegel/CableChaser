using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerator : MonoBehaviour
{
	[SerializeField] private GameObject _straight;
	[SerializeField] private GameObject _curve_Right;
	[SerializeField] private GameObject _curve_Left;

	[SerializeField] private List<GameObject> _spawnedTiles;
	[SerializeField] private Transform _sceneRoot;

	private List<GameObject> _tiles;

	public int SpawnCount;
	public int CurveSpawnChance;
	public int StraightSpawnChance;

	#region UnityEvents
	void Awake()
    {
		_tiles = new List<GameObject>();
		_spawnedTiles = new List<GameObject>();
		_tiles.Add(_straight);
		_tiles.Add(_curve_Right);
		_tiles.Add(_curve_Left);

		var initialTile = _tiles[0];
		var initialInstance = Instantiate(initialTile, initialTile.transform.position, Quaternion.identity);
		initialInstance.transform.SetParent(_sceneRoot);
		_spawnedTiles.Add(initialInstance);
		initialInstance.name = "tile_0";

		GenerateTiles();
    }
	#endregion

	public void GenerateTiles()
	{
		for (int i = 0; i < SpawnCount; i++)
		{
			var rand = Random.Range(0, _tiles.Count);
			var tile = _tiles[rand];

			switch (rand)
			{
				//Straight
				case 0:
					var spawnChance = Random.Range(5, StraightSpawnChance);
					SpawnTile(tile, i, spawnChance);

					break;

				//Curve Right
				case 1:
					spawnChance = Random.Range(1, CurveSpawnChance);
					SpawnTile(tile, i, spawnChance);

					break;

				//Curve Left
				case 2:
					spawnChance = Random.Range(1, CurveSpawnChance);
					SpawnTile(tile, i, spawnChance);

					break;
			}
		}
	}

	private void SpawnTile(GameObject tile, int formerTileIndex, int spawnChance)
	{
		for (int i = 0; i < spawnChance; i++)
		{
			var instanceStartpoint = tile.GetComponent<TileContainer>().StartConnector;
			var formerTileContainer = _spawnedTiles[_spawnedTiles.Count-1].GetComponent<TileContainer>();
			var newInstance = Instantiate(tile, formerTileContainer.EndConnector.transform.position, formerTileContainer.EndConnector.transform.rotation);
			newInstance.transform.SetParent(_sceneRoot);
			newInstance.name = "tile_" + _spawnedTiles.Count;
			_spawnedTiles.Add(newInstance);
		}
	}
}
