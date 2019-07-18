using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WorldBuilder : MonoBehaviour
{
    public static WorldBuilder Singleton;
    [SerializeField] private Transform LandBlocksHolder;


    [SerializeField] private List<LandBlock> LandBlocksPrefabs;

    private int _currentLine;

    [Header("Settings")]
    [SerializeField] private int BuildHeight = 20;
    [SerializeField] private int BuildWidth = 20;

    [Header("Chance To Build")]
    //should use noise func
    [SerializeField] private float ChanceToBuild = 0.1f;

    [Header("Player Prefab")]
    [SerializeField] private GameObject playerPrefab;

    [Header("Enemy Prefab")]
    [SerializeField] private Enemy EnemyPrefab;

    private Dictionary<int, Dictionary<int, LandBlock>> _dictionaryPositionToLandBlock = new Dictionary<int, Dictionary<int, LandBlock>>();

    private HashSet<LandBlock> _allCreatedLandBlock = new HashSet<LandBlock>();

    private LandBlock start;

    //will be dynamic according to player going up.
    private int _currentBuildingLine = 0;

    private float _currentTimeToNextLine;

    public LandBlock GetLandBlocksPrefabs()
    {
        return LandBlocksPrefabs.PickRandom();
    }

    void Awake()
    {
        Singleton = this;

        LandBlocksHolder.position -= new Vector3(BuildWidth / 2f, 0, 0);

        for (int lineHeight = 0; lineHeight < BuildHeight; lineHeight++)
        {
            AddLandBlocksLine(lineHeight);
        }
    }

    public float GetTimeToNextLine()
    {
        return 1 / Configuration.Singleton.CameraSpeed;
    }

    void Start()
    {
        _currentTimeToNextLine = GetTimeToNextLine();
        start = _allCreatedLandBlock.FirstOrDefault();
        InitializePlayer();
    }

    private void AddLandBlocksLine(int lineHeight)
    {
        _dictionaryPositionToLandBlock[lineHeight] = new Dictionary<int, LandBlock>();
        _currentLine = lineHeight;

        for (int x = 0; x < BuildWidth; x++)
        {
            if (Random.value < ChanceToBuild)
            {
                AddLandBlock(x, lineHeight);
            }
        }
    }

    private void RemoveLine(int lineHeight)
    {
        if (!_dictionaryPositionToLandBlock.ContainsKey(lineHeight))
        {
            return;
        }

        var dic = _dictionaryPositionToLandBlock[lineHeight];
        foreach (var landBlock in dic.ToList())
        {
            RemoveBlock(landBlock.Value);
        }

        _dictionaryPositionToLandBlock.Remove(lineHeight);
    }

    private void RemoveBlock(LandBlock block)
    {
        _dictionaryPositionToLandBlock[block.Y].Remove(block.X);
        _allCreatedLandBlock.Remove(block);
        LandBlock.DestroyLand(block);
    }

    private void AddLandBlock(int x, int y)
    {
        var landBlockPrefab = GetLandBlocksPrefabs();
        var landBlock = LandBlock.LandBlockFactory(landBlockPrefab, x, y, LandBlocksHolder);
        _allCreatedLandBlock.Add(landBlock);
        _dictionaryPositionToLandBlock[y][x] = landBlock;
    }

    void Update()
    {
        _currentTimeToNextLine -= Time.deltaTime;

        if (_currentTimeToNextLine < 0)
        {
            _currentTimeToNextLine = GetTimeToNextLine();
            AddLandBlocksLine(++_currentLine);
            RemoveLine(_currentLine - BuildHeight - 5);

            AddEnemy(Random.Range(0, BuildWidth), _currentLine);

        }
    }

    private void AddEnemy(int x, float y)
    {
        var enemy = Instantiate(EnemyPrefab, 
            new Vector3(x, y + 3f ,0 ), Quaternion.identity);
    }

    private void InitializePlayer()
    {
        var player = Instantiate(playerPrefab, start.transform.position + (Vector3.up*3f), Quaternion.identity);
    }

}
