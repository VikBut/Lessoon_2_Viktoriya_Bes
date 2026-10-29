using UnityEngine;


public enum ItemType
{
    Potions, Food, Weapon
}

public class ItemScriptableObject : ScriptableObject
{
   public string itemName;
   public int maxAmount;
   public GameObject itenPrefab;
   public Sprite icon;
   public ItemType itemType;
   
}
