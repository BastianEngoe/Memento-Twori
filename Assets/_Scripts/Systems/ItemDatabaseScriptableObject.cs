using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ItemDatabase", order = 1)]
public class ItemDatabaseScriptableObject : ScriptableObject
{
    public List<Item> ItemDatabase;
}
