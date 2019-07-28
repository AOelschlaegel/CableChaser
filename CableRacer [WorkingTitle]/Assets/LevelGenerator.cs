using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
	[Header("Setup")]
	[SerializeField] private int _straightSpawnChance;
	[SerializeField] private int _curveSpawnChance;
	[SerializeField] private int _obstacleSpawnModulo;
	[SerializeField] private Transform _levelSceneRoot;
	[SerializeField] private Transform _levelTilesRoot;
	[SerializeField] private Transform _levelObstaclesRoot;
	[SerializeField] private GameObject _tileStraight;
	[SerializeField] private GameObject _tileLeft;
	[SerializeField] private GameObject _tileRight;
	[SerializeField] private GameObject _obstacleStraight;


	[Header("Info")]
	[SerializeField] private List<GameObject> _spawnedTiles;
	[SerializeField] private List<GameObject> _spawnedObstacles;

	private List<GameObject> _tiles;
	private Level _level;
	private LevelContainer _levelContainer;

	[Header("Level Generation")]

	public string LevelName;
	public int Length;
	public int Speed;
	public Color Color;
	public int Difficulty;

	[Header("Debug")]
	[SerializeField] private int _obstacleSpawnDefaultModulo;


	[ContextMenu("CreateLevel")]

	public void CreateLevel()
	{
		_spawnedTiles = new List<GameObject>();
		_spawnedObstacles = new List<GameObject>();

		_tiles = new List<GameObject>();
		_tiles.Add(_tileStraight);
		_tiles.Add(_tileLeft);
		_tiles.Add(_tileRight);

		_level = new Level();
		_level.Length = Length;
		_level.LevelName = LevelName;
		_level.Speed = Speed;
		_level.Color = Color;
		_level.Difficulty = Difficulty;

		DeleteSceneObjects();
		CreateInitialTile();

		for (var i = 0; i < Length; i++)
		{
			var randTile = Random.Range(0, _tiles.Count);
			var tile = _tiles[randTile];

			int spawnChance;
			switch (randTile)
			{
				case 0:
					spawnChance = Random.Range(5, _straightSpawnChance);
					SpawnTile(tile, spawnChance);
					break;
				case 1:
					spawnChance = Random.Range(1, _curveSpawnChance);
					SpawnTile(tile, spawnChance);
					break;
				case 2:
					spawnChance = Random.Range(1, _curveSpawnChance);
					SpawnTile(tile, spawnChance);
					break;
			}
		}
	}

	[ContextMenu("Create Obstacles")]

	public void CreateObstacles()
	{
		int obstacleCount = 0;
		
		for (int i = 0; i < Length; i++)
		{
			var modulo = _obstacleSpawnDefaultModulo - Difficulty;

			if (i % modulo == 0 && i != 0)
			{
				var tile = _spawnedTiles[i].gameObject;
				var instance = Instantiate(_obstacleStraight, tile.transform.position, tile.transform.rotation);
				instance.name = "obstacle_" + obstacleCount;
				instance.transform.SetParent(_levelObstaclesRoot);
				_spawnedObstacles.Add(instance);
				obstacleCount++;
			}
		}
	}

	public void CreateInitialTile()
	{
		var initialTile = _tiles[0];
		var initialPos = new Vector3(0, 0, 2);
		var initialInstance = Instantiate(initialTile, initialPos, Quaternion.identity);

		initialInstance.transform.SetParent(_levelTilesRoot);
		_levelSceneRoot.name = _level.LevelName;
		_spawnedTiles.Add(initialInstance);
		initialInstance.name = "tile_0";
		initialInstance.GetComponent<TileContainer>().Id = 0;
	}

	public void DeleteSceneObjects()
	{
		if (_levelSceneRoot.childCount > 0)
		{
			for (int i = 0; i < 20; i++)
			{
				foreach (Transform tile in _levelObstaclesRoot)
				{
					DestroyImmediate(tile.gameObject);
				}
			}

			for (int i = 0; i < 20; i++)
			{
				foreach (Transform tile in _levelTilesRoot)
				{
					DestroyImmediate(tile.gameObject);
				}
			}
		}
	}

	private void SpawnTile(GameObject tile, int spawnChance)
	{
		for (var i = 0; i < spawnChance; i++)
		{
			// Only instantiate if Length is not reached
			if (_spawnedTiles.Count <= Length-1)
			{
				var formerTileContainer = _spawnedTiles[_spawnedTiles.Count - 1].GetComponent<TileContainer>();
				var tileInstance = Instantiate(tile, formerTileContainer.EndConnector.transform.position, formerTileContainer.EndConnector.transform.rotation);

				tileInstance.transform.SetParent(_levelTilesRoot);
				tileInstance.name = "tile_" + _spawnedTiles.Count;
				tileInstance.GetComponent<TileContainer>().Id = _spawnedTiles.Count;

				_spawnedTiles.Add(tileInstance);
			}
		}

		_levelContainer = _levelSceneRoot.GetComponent<LevelContainer>();

		_levelContainer.Length = Length;
		_levelContainer.LevelName = LevelName;
		_levelContainer.Speed = Speed;
		_levelContainer.Color = Color;
		_levelContainer.Difficulty = Difficulty;

		_levelContainer.SetLevel();
	}
}

[Serializable]
public class Level
{
	public string LevelName;
	public int Length;
	public int Speed;
	public Color Color;
	public int Difficulty;
}
