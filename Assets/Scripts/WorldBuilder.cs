using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    public Transform LandBlocksHolder;

    public List<LandBlock> LandBlocksPrefabs;

    [Header("Settings")]
    public int BuildHeight = 20;
    public int BuildWidth = 20;

    [Header("Chance To Build")]
    //should use noise func
    public float ChanceToBuild = 0.1f;

    private HashSet<LandBlock> _allCreatedLandBlock = new HashSet<LandBlock>();

    //will be dynamic according to player going up.
    private int _currentBuildingLine = 0;
    
    private LandBlock GetLandBlocksPrefabs()
    {
        return LandBlocksPrefabs.PickRandom();
    }

    void Awake()
    {
        LandBlocksHolder.position -= new Vector3(BuildWidth / 2f, 0, 0);

        for (int lineHeight = 0; lineHeight < BuildHeight; lineHeight++)
        {
            AddLandBlocksLine(lineHeight);
        }
    }

    private void AddLandBlocksLine(int lineHeight)
    {
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



}
