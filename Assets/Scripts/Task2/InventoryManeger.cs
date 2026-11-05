using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManeger  : MonoBehaviour
{
    public FirstPersonMovement playerMovement;
    public GameObject uiPanel;
    public Transform inventoryPanel;
    public List<Slot> slots = new List<Slot>();
    public bool isOpened;

    private void Awake()
    {
        uiPanel.SetActive(true);
    }

    private void Start()
    {
        for (int i = 0; i < inventoryPanel.childCount; i++)
        {
            slots.Add(inventoryPanel.GetChild(i).GetComponent<Slot>());
        }
        
        uiPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isOpened)
            {
                uiPanel.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                playerMovement.enabled = true;
            }
            else
            {
                uiPanel.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                playerMovement.enabled = false;
            }
            
            isOpened =  !isOpened;
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            int weaponCount = 0;
            int foodCount = 0;
            int potionCount = 0;
            int capacity = 0;
            int countIn = 0;
            
            int weaponUnique = 0;
            int foodUnique = 0;
            int potionUnique = 0;

            foreach (Slot slot in slots)
            {
                if (!slot.isEmpty && slot.item != null)
                {
                    if (slot.item.itemType == ItemType.Food)
                    {
                        foodCount += slot.amount;
                        foodUnique = 1;
                    }
                    else if (slot.item.itemType == ItemType.Weapon)
                    {
                        weaponCount += slot.amount;
                        weaponUnique = 1;
                    }
                    else if (slot.item.itemType == ItemType.Potions)
                    {
                        potionCount += slot.amount;
                        potionUnique = 1;
                    }
                    
                    countIn++;
                } 
                
                capacity++;
            }

            print($"Кол-во еды в инвентаре:  {foodCount}");
            print($"Кол-во оружия в инвентаре:  {weaponCount}");
            print($"Кол-во зелий в инвентаре:  {potionCount}");
            print($"Занято {countIn} ячейки/ячеек из {capacity}");

            if (potionUnique == 1 || weaponUnique == 1 || foodUnique == 1)
            {
                int allUnique = 0;
                allUnique = potionUnique + weaponUnique + foodUnique;
                print($"Уникальных предметов: {allUnique}");
                
            }
        }
    }
    
    public bool AddItem(ItemScriptableObject item, int amount)
    {
        foreach (Slot slot in slots)
        {
            if (slot.item == item && (slot.amount + amount) <= item.maxAmount)
            {
                slot.amount += amount;
                slot.itemAmountText.text = slot.amount.ToString();
                return true;
            }
        }

        foreach (Slot slot in slots)
        {
            if (slot.isEmpty)
            {
                slot.item = item;
                slot.amount += amount;
                slot.isEmpty = false;
                slot.SetIcon(item.icon);
                slot.itemAmountText.text = amount.ToString();
                return true;
            }
        }
        
        Debug.Log("Инвентарь заполнен");
        return false;
    }
}
