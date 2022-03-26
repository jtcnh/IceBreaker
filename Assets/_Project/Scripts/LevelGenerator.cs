using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Requirements")]
    [SerializeField] private Camera _gameCamera;
    [SerializeField] private LevelData _levelData;

    private Vector2 _extents;
    private GameObject _tileContainer;
    private GameObject _entityContainer;

    void Start()
    {
        CreateContainers();

        GenerateLevel();

        SetCamera();
    }


    private void CreateContainers()
    {
        _entityContainer = new GameObject();
        _tileContainer = new GameObject();

        _tileContainer.name = "TileContainer";
        _tileContainer.transform.parent = transform;

        _entityContainer.name = "EntityContainer";
        _entityContainer.transform.parent = transform;

    }


    private void SetCamera()
    {
        _gameCamera.transform.position = new Vector3(_extents.x / 2, _extents.y / 2, -10);
    }


    private void GenerateLevel()
    {
        Debug.Log("Generating Level: " + _levelData.levelName);
        for (int x = 0; x < _levelData.blockMap.width; x++)
        {
            for (int y = 0; y < _levelData.blockMap.height; y++)
            {
                GenerateTile(x, y);
                GenerateEntity(x, y);
            }
        }

        _extents = new Vector2(_levelData.blockMap.width, _levelData.blockMap.height);
    }


    private void GenerateTile(int x, int y)
    {
        Color pixelColor = _levelData.blockMap.GetPixel(x, y);
        if (pixelColor.a == 0)
        {
            return;
        }


        foreach (ColorToPrefab colorMapping in _levelData.prefabRefrence)
        {
            if (colorMapping.color.Equals(pixelColor))
            {
                Vector2 position = new Vector2(x, y);
                Instantiate(colorMapping.prefab, position, Quaternion.identity, _tileContainer.transform);
            }
        }
    }


    private void GenerateEntity(int x, int y)
    {
        Color pixelColor = _levelData.entityMap.GetPixel(x, y);
        if (pixelColor.a == 0)
        {
            return;
        }

        //Debug.Log(pixelColor);

        foreach (ColorToPrefab colorMapping in _levelData.entityRefrence)
        {
            if (colorMapping.color.Equals(pixelColor))
            {
                Vector2 position = new Vector2(x, y);
                Instantiate(colorMapping.prefab, position, Quaternion.identity, _entityContainer.transform);
            }
        }
    }
}
