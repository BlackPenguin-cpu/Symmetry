using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public enum EItem
    {
        WindEarRing,
        NeedleArmour,
        KnifeCape,
        CurseKnife,
        BloodGauntlet,
        CrystalOrb,
        TheOneRing,
        POWER,
        SPEED,
        ATTACKSPEED,
        HEALTH,
        TIME,
        DEFFENCE,
        None,
    }

    public EItem eItem;
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



