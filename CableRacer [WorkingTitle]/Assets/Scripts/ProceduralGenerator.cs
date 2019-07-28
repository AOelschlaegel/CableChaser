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

    [SerializeField] private PlayerController_Endless playerControllerEndless;

    private List<GameObject> _obstaclesLeft = new List<GameObject>();
    private List<GameObject> _obstaclesRight = new List<GameObject>();
    private List<GameObject> _obstaclesStraight = new List<GameObject>();

    public List<GameObject> TilePool;
    public List<GameObject> SpawnedObstacles = new List<GameObject>();
    [SerializeField] private Transform _sceneRoot;

    public List<GameObject> StraightTilePool;
    public List<GameObject> LeftTilePool;
    public List<GameObject> RightTilePool;

    public int PoolSize;

    private List<GameObject> _tiles;

    public int SpawnCount;
    public int DeleteThreshold;
    public int CurveSpawnChance;
    public int StraightSpawnChance;
    public int LastDeletedIndex = 0;

    public int SpawnRadius;

    public int ObstacleSpawnDivider;

    public int division = 2;

    public int CurrentTileId;

    [Header("Screen")]
    public bool isScreen;



    #region UnityEvents

    private void Awake()
    {
        _tiles = new List<GameObject>();
        TilePool = new List<GameObject>();
        _tiles.Add(_straight);
        _tiles.Add(_curve_Right);
        _tiles.Add(_curve_Left);



        var initialTile = _tiles[0];
        var initialPos = new Vector3(0, 0, 2);
        var initialInstance = Instantiate(initialTile, initialPos, Quaternion.identity);
        initialInstance.transform.SetParent(_sceneRoot);
        TilePool.Add(initialInstance);
        initialInstance.name = "tile_0";
        initialInstance.GetComponent<TileContainer>().Id = 0;

        GenerateTiles();

        InstantiatePool();

    }

    void InstantiatePool()
    {
        var poolPos = new Vector3(0, 100000, 0);

        for (int i = 0; i < PoolSize; i++)
        {
            var straightInstance = Instantiate(_tiles[0].gameObject, poolPos, Quaternion.identity);
            StraightTilePool.Add(straightInstance);
            straightInstance.transform.parent = _sceneRoot;

            var rightInstance = Instantiate(_tiles[1].gameObject, poolPos, Quaternion.identity);
            RightTilePool.Add(rightInstance);
            rightInstance.transform.parent = _sceneRoot;

            var leftInstance = Instantiate(_tiles[2].gameObject, poolPos, Quaternion.identity);
            LeftTilePool.Add(leftInstance);
            leftInstance.transform.parent = _sceneRoot;
        }
    }

    void Update()
    {
        var formerTileId = playerControllerEndless.CurrentTileId;

        if (formerTileId > SpawnRadius)
        {
            if (formerTileId != CurrentTileId)
            {
                RepositionTiles(formerTileId - SpawnRadius, CurrentTileId + 1);
            }
        }

        /*if (CurrentTileId > SpawnedTiles.Count - DeleteThreshold)
		{
			GenerateTiles();
			DeactivateTiles(CurrentTileId);
		}*/

        CurrentTileId = playerControllerEndless.CurrentTileId;
    }

    void RepositionTiles(int formerTileId, int newTileId)
    {
        var randTile = Random.Range(0, TilePool.Count);

        var repoTile = TilePool[formerTileId - SpawnRadius - 1];

        repoTile.transform.position = TilePool[TilePool.Count - 1].transform.position;

        //SpawnedTiles.Remove(repoTile);
        //SpawnedTiles.Add(repoTile);

        Debug.Log(repoTile);
    }

    void DeactivateTiles(int CurrentTileId)
    {
        for (int i = LastDeletedIndex; i < LastDeletedIndex + DeleteThreshold; i++)
        {
            TilePool[i].gameObject.SetActive(false);
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

                    spawnChance = Random.Range(5, StraightSpawnChance);
                    SpawnTile(tile, spawnChance);
                    break;
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
        if (!isScreen)
        {
            var lastTile = TilePool[TilePool.Count - 1].gameObject;
            var instance = Instantiate(_obstacle_Straight, lastTile.transform.position, lastTile.transform.rotation);
        }
    }

    private void SpawnTile(GameObject tile, int spawnChance)
    {
        for (var i = 0; i < spawnChance; i++)
        {
            if (i % ObstacleSpawnDivider == 0 && i != 0)
            {
                SpawnObstacles();
            }
            var formerTileContainer = TilePool[TilePool.Count - 1].GetComponent<TileContainer>();
            var newInstance = Instantiate(tile, formerTileContainer.EndConnector.transform.position, formerTileContainer.EndConnector.transform.rotation);
            newInstance.transform.SetParent(_sceneRoot);
            newInstance.name = "tile_" + TilePool.Count;
            newInstance.GetComponent<TileContainer>().Id = TilePool.Count;
            TilePool.Add(newInstance);
        }
    }

    private void SpawnTileFixed(GameObject tile, int spawnChance)
    {
        var formerTileContainer = TilePool[TilePool.Count - 1].GetComponent<TileContainer>();
        var newInstance = Instantiate(tile, formerTileContainer.EndConnector.transform.position, formerTileContainer.EndConnector.transform.rotation);
        newInstance.transform.SetParent(_sceneRoot);
        newInstance.name = "tile_" + TilePool.Count;
        newInstance.GetComponent<TileContainer>().Id = TilePool.Count;
        TilePool.Add(newInstance);
    }


    /*
     * var formerTileId = CurrentTileId;
     * 
     * 
     * if(formerTileId != CurrentTileId) {
     *      RepositionTiles();
     * 
     * }
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     * */

}
