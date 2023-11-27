using UnityEngine;

[CreateAssetMenu(fileName = "Map", menuName = "Create Map", order = 1)]
public class MapSO : ScriptableObject
{
    public Transform[] tilePrefabs;
    [Range(0, 1)] public float tileOutlinePercent;
    public bool gradientObstacleColor;
    public MapGenerator.Coord mapSize;
    [Range(0, 1)] public float obstaclePercent;
    public int seed;
    [Min(0)] public float minObstacleHeight;
    [Min(0)] public float maxObstacleHeight;
    public Color foregroundColor;
    public Color backgroundColor;

    public MapGenerator.Coord mapCentre
    {
        get
        {
            return new MapGenerator.Coord(mapSize.x / 2, mapSize.y / 2);
        }
    }
}
