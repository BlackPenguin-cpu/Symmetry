using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string name;
    public string explanation;
    public Sprite icon;
    public int percent;
}

[CreateAssetMenu(fileName = "ItemSo", menuName = "Scriptable Object/ItemSo")]
public class ItemSo : ScriptableObject
{
    public List<Item> Items;
    public List<Item> DA;
}



