using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Map[] _maps;
    [SerializeField] private int _mapIndex;

    [SerializeField] private Transform _tilePrefab;
    [SerializeField] private Transform _obstaclePrefab;
    [SerializeField] private Transform _navMeshMaskPrefab;
    [SerializeField] private Transform _mapFloor;
    [SerializeField] private Transform _navMashFloor;
    [SerializeField] private Vector2 _maxMapSize;

    [SerializeField][Range(0, 1)] private float _outlinePercent;

    [SerializeField] private float _tileSize;

    private List<Coord> _allTileCoords;
    private Queue<Coord> _shuffledTileCoords;
    private Queue<Coord> _shuffledOpenTileCoords;
    private Transform[,] _tileMap;

    private Map _currentMap;

    private void Start()
    {
        FindObjectOfType<Spawner>().OnNewWave += OnNewWave;
    }

    public void OnNewWave(int waveNumber)
    {
        _mapIndex = waveNumber - 1;
        GenerateMap();
    }
    public void GenerateMap()
    {
        _currentMap = _maps[_mapIndex];
        _tileMap = new Transform[_currentMap.mapSize.x, _currentMap.mapSize.y];

        System.Random prng = new System.Random(_currentMap.seed);

        //Generating coords.
        _allTileCoords = new List<Coord>();
        for (int x = 0; x < _currentMap.mapSize.x; x++)
        {
            for (int y = 0; y < _currentMap.mapSize.y; y++)
            {
                _allTileCoords.Add(new Coord(x, y));
            }
        }
        _shuffledTileCoords = new Queue<Coord>(Utility.ShuffleArray(_allTileCoords.ToArray(), _currentMap.seed));

        //Create map holder object.
        string holderName = "Generated Map";
        if (transform.Find(holderName))
        {
            DestroyImmediate(transform.Find(holderName).gameObject);
        }
        Transform mapHolder = new GameObject(holderName).transform;
        mapHolder.parent = transform;

        //Spawning tiles.
        for (int x = 0; x < _currentMap.mapSize.x; x++)
        {
            for (int y = 0; y < _currentMap.mapSize.y; y++)
            {
                Vector3 tilePosition = CoordToPosition(x, y);
                Transform newTile = Instantiate(_tilePrefab, tilePosition, Quaternion.Euler(Vector3.right * 90));
                newTile.localScale = Vector3.one * (1 - _outlinePercent) * _tileSize;
                newTile.parent = mapHolder;
                _tileMap[x, y] = newTile;
            }
        }

        //Spawning obstacles.
        bool[,] _obstacleMap = new bool[(int)_currentMap.mapSize.x, (int)_currentMap.mapSize.y];

        int obstacleCount = (int)(_currentMap.mapSize.x * _currentMap.mapSize.y * _currentMap.obstaclePercent);
        int currentObstacleCount = 0;
        List<Coord> allOpenCoords = new List<Coord>(_allTileCoords);

        for (int i = 0; i < obstacleCount; i++)
        {
            Coord randomCoord = GetRandomCoord();
            _obstacleMap[randomCoord.x, randomCoord.y] = true;
            currentObstacleCount++;

            if (randomCoord != _currentMap.mapCentre && MapIsFullyAccessible(_obstacleMap, currentObstacleCount))
            {
                float obstacleHeight = Mathf.Lerp(_currentMap.minObstacleHeight, _currentMap.maxObstacleHeight, (float)prng.NextDouble());

                Vector3 obstaclePosition = CoordToPosition(randomCoord.x, randomCoord.y);
                Transform newObstacle = Instantiate(_obstaclePrefab, obstaclePosition + Vector3.up * obstacleHeight / 2, Quaternion.identity);
                newObstacle.localScale = new Vector3((1 - _outlinePercent) * _tileSize, obstacleHeight, (1 - _outlinePercent) * _tileSize);
                newObstacle.parent = mapHolder;

                Renderer obstacleRenderer = newObstacle.GetComponent<Renderer>();
                Material obstacleMaterial = new Material(obstacleRenderer.sharedMaterial);
                float colourPercent = randomCoord.y / (float)_currentMap.mapSize.y;
                obstacleMaterial.color = Color.Lerp(_currentMap.foregroundColor, _currentMap.backgroundColor, colourPercent);
                obstacleRenderer.sharedMaterial = obstacleMaterial;

                allOpenCoords.Remove(randomCoord);
            }
            else
            {
                _obstacleMap[randomCoord.x, randomCoord.y] = false;
                currentObstacleCount--;
            }
        }

        _shuffledOpenTileCoords = new Queue<Coord>(Utility.ShuffleArray(allOpenCoords.ToArray(), _currentMap.seed));

        //Creating NavMash mask.
        Transform maskLeft = Instantiate(_navMeshMaskPrefab, Vector3.left * (_currentMap.mapSize.x + _maxMapSize.x) / 4f * _tileSize, Quaternion.identity);
        maskLeft.parent = mapHolder;
        maskLeft.localScale = new Vector3((_maxMapSize.x - _currentMap.mapSize.x) / 2f, 1, _currentMap.mapSize.y) * _tileSize;

        Transform maskRight = Instantiate(_navMeshMaskPrefab, Vector3.right * (_currentMap.mapSize.x + _maxMapSize.x) / 4f * _tileSize, Quaternion.identity);
        maskRight.parent = mapHolder;
        maskRight.localScale = new Vector3((_maxMapSize.x - _currentMap.mapSize.x) / 2f, 1, _currentMap.mapSize.y) * _tileSize;

        Transform maskTop = Instantiate(_navMeshMaskPrefab, Vector3.forward * (_currentMap.mapSize.y + _maxMapSize.y) / 4f * _tileSize, Quaternion.identity);
        maskTop.parent = mapHolder;
        maskTop.localScale = new Vector3((_maxMapSize.x), 1, (_maxMapSize.y - _currentMap.mapSize.y) / 2f) * _tileSize;

        Transform maskBottom = Instantiate(_navMeshMaskPrefab, Vector3.back * (_currentMap.mapSize.y + _maxMapSize.y) / 4f * _tileSize, Quaternion.identity);
        maskBottom.parent = mapHolder;
        maskBottom.localScale = new Vector3((_maxMapSize.x), 1, (_maxMapSize.y - _currentMap.mapSize.y) / 2f) * _tileSize;

        _navMashFloor.localScale = new Vector3(_maxMapSize.x, _maxMapSize.y) * _tileSize;
        _mapFloor.localScale = new Vector3(_currentMap.mapSize.x * _tileSize, _currentMap.mapSize.y * _tileSize);
    }
    private bool MapIsFullyAccessible(bool[,] obstacleMap, int currentObstacleCount)
    {
        bool[,] mapFlags = new bool[obstacleMap.GetLength(0), obstacleMap.GetLength(1)];
        Queue<Coord> queue = new Queue<Coord>();
        queue.Enqueue(_currentMap.mapCentre);
        mapFlags[_currentMap.mapCentre.x, _currentMap.mapCentre.y] = true;

        int accessibelTimeCount = 1;

        while (queue.Count > 0)
        {
            Coord tile = queue.Dequeue();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    int neighbourX = tile.x + x;
                    int neighbourY = tile.y + y;

                    if (x == 0 || y == 0)
                    {
                        if (neighbourX >= 0 && neighbourX < obstacleMap.GetLength(0) && neighbourY >= 0 && neighbourY < obstacleMap.GetLength(1))
                        {
                            if (!mapFlags[neighbourX, neighbourY] && !obstacleMap[neighbourX, neighbourY])
                            {
                                mapFlags[neighbourX, neighbourY] = true;
                                queue.Enqueue(new Coord(neighbourX, neighbourY));
                                accessibelTimeCount++;
                            }
                        }
                    }
                }
            }
        }
        int targetAccessibleTilesCount = (int)(_currentMap.mapSize.x * _currentMap.mapSize.y - currentObstacleCount);

        return targetAccessibleTilesCount == accessibelTimeCount;
    }
    private Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-_currentMap.mapSize.x / 2f + 0.5f + x, 0f, -_currentMap.mapSize.y / 2f + 0.5f + y) * _tileSize;
    }
    public Transform GetTileFromPosition(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x / _tileSize + (_currentMap.mapSize.x - 1) / 2f);
        int y = Mathf.RoundToInt(position.z / _tileSize + (_currentMap.mapSize.y - 1) / 2f);

        x = Mathf.Clamp(x, 0, _tileMap.GetLength(0) - 1);
        y = Mathf.Clamp(y, 0, _tileMap.GetLength(1) - 1);

        return _tileMap[x, y];
    }
    public Coord GetRandomCoord()
    {
        Coord randomCoord = _shuffledTileCoords.Dequeue();
        _shuffledTileCoords.Enqueue(randomCoord);

        return randomCoord;
    }

    public Transform GetRandomOpenTile()
    {
        Coord randomCoord = _shuffledOpenTileCoords.Dequeue();
        _shuffledOpenTileCoords.Enqueue(randomCoord);

        return _tileMap[randomCoord.x, randomCoord.y];
    }

    [System.Serializable]
    public struct Coord
    {
        public int x;
        public int y;

        public Coord(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public static bool operator ==(Coord c1, Coord c2)
        {
            return c1.x == c2.x && c1.y == c2.y;
        }

        public static bool operator !=(Coord c1, Coord c2)
        {
            return !(c1 == c2);
        }
        #region PlaceHolder
        public override bool Equals(object o)
        {
            return true;
        }
        public override int GetHashCode()
        {
            return -1;
        }
        #endregion
    }
    [System.Serializable]
    public class Map
    {
        public Coord mapSize;
        [Range(0, 1)] public float obstaclePercent;
        public int seed;
        public float minObstacleHeight;
        public float maxObstacleHeight;
        public Color foregroundColor;
        public Color backgroundColor;

        public Coord mapCentre
        {
            get
            {
                return new Coord(mapSize.x / 2, mapSize.y / 2);
            }
        }
    }
}
