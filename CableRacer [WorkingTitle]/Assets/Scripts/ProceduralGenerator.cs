using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGenerator : MonoBehaviour
{
	[SerializeField] private GameObject _straight;
	[SerializeField] private GameObject _curve_Right;
	[SerializeField] private GameObject _curve_Left;

	[SerializeField] private GameObject _obstacle_Left;
	[SerializeField] private GameObject _obstacle_Right;
	[SerializeField] private GameObject _obstacle_Straight;
	[SerializeField] private GameObject _obstacle_Dynamic;
	[SerializeField] private GameObject _obstacle_Dynamic_fast;

	[SerializeField] private PlayerController_Fixed _playerController;

	private List<GameObject> _obstacles_Left = new List<GameObject>();
	private List<GameObject> _obstacles_Right = new List<GameObject>();
	private List<GameObject> _obstacles_Straight = new List<GameObject>();
	private List<GameObject> _obstacles_Dymanic = new List<GameObject>();

	public List<GameObject> SpawnedTiles;
	public List<GameObject> SpawnedObstacles = new List<GameObject>();
	[SerializeField] private Transform _sceneRoot;

	private List<GameObject> _tiles;

	public int SpawnCount;
	public int DeleteThreshold;
	public int CurveSpawnChance;
	public int StraightSpawnChance;
	public int LastDeletedIndex = 0;

	public int ObstacleSpawnDivider;

	public int division = 2;

	#region UnityEvents
	void Awake()
	{
		_tiles = new List<GameObject>();
		SpawnedTiles = new List<GameObject>();
		_tiles.Add(_straight);
		_tiles.Add(_curve_Right);
		_tiles.Add(_curve_Left);

		var initialTile = _tiles[0];
		var initialPos = new Vector3(0, 0, 2);
		var initialInstance = Instantiate(initialTile, initialPos, Quaternion.identity);
		initialInstance.transform.SetParent(_sceneRoot);
		SpawnedTiles.Add(initialInstance);
		initialInstance.name = "tile_0";
		initialInstance.GetComponent<TileContainer>().Id = 0;

		GenerateTiles();
	}

	void Update()
	{
		var tileId = _playerController.CurrentTileId;

		if (tileId > SpawnedTiles.Count - DeleteThreshold)
		{
			GenerateTiles();
			DeactivateTiles(tileId);
		}

		var mod = division % 3;
		Debug.Log(mod);
	}

	void DeactivateTiles(int tileId)
	{
		for (int i = LastDeletedIndex; i < LastDeletedIndex + DeleteThreshold; i++)
		{
			SpawnedTiles[i].gameObject.SetActive(false);
		}

		LastDeletedIndex += DeleteThreshold;
	}

	void DeactivateObstacles()
	{

	}

	#endregion

	private void GenerateTiles()
	{
		for (int i = 0; i < SpawnCount; i++)
		{
			var randTile = Random.Range(0, _tiles.Count);
			var tile = _tiles[randTile];
			var tileId = tile.GetComponent<TileContainer>().Id;

			switch (randTile)
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

	private void SpawnObstacles()
	{
		var lastTile = SpawnedTiles[SpawnedTiles.Count - 1].gameObject;

		var rand = Random.Range(0, 14);
		

		if (rand == 4 || rand == 2)
		{
			var instance = Instantiate(_obstacle_Dynamic_fast, lastTile.transform.position, lastTile.transform.rotation);
		} else if (rand == 7)
		{
			var instance = Instantiate(_obstacle_Dynamic, lastTile.transform.position, lastTile.transform.rotation);
		}
		else
		{
			var instance = Instantiate(_obstacle_Straight, lastTile.transform.position, lastTile.transform.rotation);
		}

		

	}

	private void SpawnTile(GameObject tile, int formerTileIndex, int spawnChance)
	{
		for (int i = 0; i < spawnChance; i++)
		{
			if (i % ObstacleSpawnDivider == 0 && i != 0)
			{
				SpawnObstacles();
			}

			var instanceStartpoint = tile.GetComponent<TileContainer>().StartConnector;
			var formerTileContainer = SpawnedTiles[SpawnedTiles.Count - 1].GetComponent<TileContainer>();
			var newInstance = Instantiate(tile, formerTileContainer.EndConnector.transform.position, formerTileContainer.EndConnector.transform.rotation);
			newInstance.transform.SetParent(_sceneRoot);
			newInstance.name = "tile_" + SpawnedTiles.Count;
			newInstance.GetComponent<TileContainer>().Id = SpawnedTiles.Count;
			SpawnedTiles.Add(newInstance);
		}
	}
}
