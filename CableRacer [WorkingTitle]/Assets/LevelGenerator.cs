using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    [Header("Setup")] 
    [SerializeField] private int _straightSpawnChance;
    [SerializeField] private int _curveSpawnChance;
    [SerializeField] private int _obstacleSpawnModulo;
    [SerializeField] private Transform _sceneRoot;
    [SerializeField] private GameObject _tileStraight;
    [SerializeField] private GameObject _tileLeft;
    [SerializeField] private GameObject _tileRight;
    [SerializeField] private GameObject _obstacleStraight;

    [Header("Info")] 
    [SerializeField] private List<GameObject> _spawnedTiles;
   
    private List<GameObject> _tiles;
    
    
    private Level _level;

    public string Name;
    public int Length;
    public int Speed;
    public int Color;
    public int Difficulty;
    
    

    [ContextMenu("CreateLevel")]
    public void CreateLevel()
    {
        _tiles = new List<GameObject>();
        _tiles.Add(_tileStraight);
        _tiles.Add(_tileLeft);
        _tiles.Add(_tileRight);

        _level = new Level();
        _level.Length = Length;
        _level.Name = Name;
        _level.Speed = Speed;
        _level.Color = Color;
        _level.Difficulty = Difficulty;
        
        for (var i = 0; i < Length; i++)
        {
            var randTile = Random.Range(0, _tiles.Count);
            var tile = _tiles[randTile];
            
            int spawnChance;
            switch (randTile)
            {
                case 0:
                {
                    spawnChance = Random.Range(5, _straightSpawnChance);
                    SpawnTile(tile, i, spawnChance);
                    break;
                }
                case 1:
                    spawnChance = Random.Range(1, _curveSpawnChance);
                    SpawnTile(tile, i, spawnChance);
                    break;
                case 2:
                    spawnChance = Random.Range(1, _curveSpawnChance);
                    SpawnTile(tile, i, spawnChance);
                    break;
            }
        }
    }
    
    private void SpawnTile(GameObject tile, int formerTileIndex, int spawnChance)
    {
        for (var i = 0; i < spawnChance; i++)
        {
            if (i % _obstacleSpawnModulo == 0 && i != 0)
            {
                SpawnObstacles();
            }
            var formerTileContainer = _spawnedTiles[_spawnedTiles.Count - 1].GetComponent<TileContainer>();
            var newInstance = Instantiate(tile, formerTileContainer.EndConnector.transform.position, formerTileContainer.EndConnector.transform.rotation);
            newInstance.transform.SetParent(_sceneRoot);
            newInstance.name = "tile_" + _spawnedTiles.Count;
            newInstance.GetComponent<TileContainer>().Id = _spawnedTiles.Count;
            _spawnedTiles.Add(newInstance);
        }
    }
    
    public void SpawnObstacles()
    {
        var lastTile = _spawnedTiles[_spawnedTiles.Count - 1].gameObject;
        var instance = Instantiate(_obstacleStraight, lastTile.transform.position, lastTile.transform.rotation);
    }

    void Awake()
    {
        
    }

    void Update()
    {
    }
}

[Serializable]
public class Level
{
    public string Name;
    public int Length;
    public int Speed;
    public int Color;
    public int Difficulty;
}