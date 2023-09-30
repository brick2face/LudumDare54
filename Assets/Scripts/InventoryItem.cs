using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
[CreateAssetMenu(menuName = "SubGame/Add New Item")]
public class InventoryItem : ScriptableObject
{
    public string ItemName;
    public Sprite ItemSprite;
    public string ItemDescription;
    public int MaxStack = 1;
}
