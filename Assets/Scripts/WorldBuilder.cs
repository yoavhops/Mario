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
        start = _allCreatedLandBlock.FirstOrDefault();
        InitializePlayer();
    }


        _currentTimeToNextLine = GetTimeToNextLine();
    }

    private void AddLandBlocksLine(int lineHeight)
    {

        _currentLine = lineHeight;

        for (int x = 0; x < BuildWidth; x++)
        {
            if (Random.value < ChanceToBuild)
            {
                AddLandBlock(x, lineHeight);
            }
        }
    }

    private void AddLandBlock(int x, int y)
    {
        var landBlockPrefab = GetLandBlocksPrefabs();
        var landBlock = LandBlock.LandBlockFactory(landBlockPrefab, x, y, LandBlocksHolder);
        _allCreatedLandBlock.Add(landBlock);
    }

    void Update()
    {
        _currentTimeToNextLine -= Time.deltaTime;

        if (_currentTimeToNextLine < 0)
        {
            _currentTimeToNextLine = GetTimeToNextLine();
            AddLandBlocksLine(++_currentLine);
        }
    }

    private void InitializePlayer()
    {
        var player = Instantiate(playerPrefab, start.transform.position + (Vector3.up*3f) + Vector3.right, Quaternion.identity);
    }

}
