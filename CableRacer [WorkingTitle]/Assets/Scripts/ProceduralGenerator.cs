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

	[SerializeField] private PlayerController_Fixed _playerController;

	private List<GameObject> _obstaclesLeft = new List<GameObject>();
	private List<GameObject> _obstaclesRight = new List<GameObject>();
	private List<GameObject> _obstaclesStraight = new List<GameObject>();

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

	private void Awake()
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
	}

	void DeactivateTiles(int tileId)
	{
		for (int i = LastDeletedIndex; i < LastDeletedIndex + DeleteThreshold; i++)
		{
			SpawnedTiles[i].gameObject.SetActive(false);
		}

		LastDeletedIndex += DeleteThreshold;
	}

	#endregion

	public void GenerateTiles()
	{
		for (var i = 0; i < SpawnCount; i++)
		{
			var randTile = Random.Range(0, _tiles.Count);
			var tile = _tiles[randTile];
			var tileId = tile.GetComponent<TileContainer>().Id;

			int spawnChance;
			switch (randTile)
			{
				case 0:
				{
					spawnChance = Random.Range(5, StraightSpawnChance);
					SpawnTile(tile, spawnChance);
					break;
				}
				case 1:
					spawnChance = Random.Range(1, CurveSpawnChance);
					SpawnTile(tile, spawnChance);
					break;
				case 2:
					spawnChance = Random.Range(1, CurveSpawnChance);
					SpawnTile(tile, spawnChance);
					break;
			}
		}
	}

	public void SpawnObstacles()
	{
		var lastTile = SpawnedTiles[SpawnedTiles.Count - 1].gameObject;
		var instance = Instantiate(_obstacle_Straight, lastTile.transform.position, lastTile.transform.rotation);
	}

	private void SpawnTile(GameObject tile, int spawnChance)
	{
		for (var i = 0; i < spawnChance; i++)
		{
			if (i % ObstacleSpawnDivider == 0 && i != 0)
			{
				SpawnObstacles();
			}
			var formerTileContainer = SpawnedTiles[SpawnedTiles.Count - 1].GetComponent<TileContainer>();
			var newInstance = Instantiate(tile, formerTileContainer.EndConnector.transform.position, formerTileContainer.EndConnector.transform.rotation);
			newInstance.transform.SetParent(_sceneRoot);
			newInstance.name = "tile_" + SpawnedTiles.Count;
			newInstance.GetComponent<TileContainer>().Id = SpawnedTiles.Count;
			SpawnedTiles.Add(newInstance);
		}
	}
}
