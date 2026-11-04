using UnityEngine;


public enum ItemType
{
    Potions, Food, Weapon
}

public class ItemScriptableObject : ScriptableObject
{
    public ItemType itemType;
   public string itemName;
   public int maxAmount;
   public GameObject itenPrefab;
   public Sprite icon;
}
