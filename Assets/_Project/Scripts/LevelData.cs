using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public string levelName;

    public Texture2D entityMap;
    public Texture2D blockMap;

    public ColorToPrefab[] prefabRefrence;
    public ColorToPrefab[] entityRefrence;
}