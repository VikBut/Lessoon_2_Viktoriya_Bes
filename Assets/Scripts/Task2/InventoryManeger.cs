using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManeger  : MonoBehaviour
{
    public FirstPersonMovement playerMovement;   // Тут все поля публичные по возможности надо их сделать приватными, которые не используются за пределами класса
    public GameObject uiPanel;
    public Transform inventoryPanel;
    public List<Slot> slots = new List<Slot>();
    public bool isOpened;

    private void Awake()
    {
        uiPanel.SetActive(true); // UI при пробуждении включается, и при старте сразу отключается
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
        if (Input.GetKeyDown(KeyCode.E)) // Интпут система старая
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
        
        if (Input.GetKeyDown(KeyCode.R)) // тоже инпут
        {
            int weaponInt = 0;  // названия, наверное weaponCount, ну и остальные чуть ниже
            int foodInt = 0;
            int potionInt = 0;
            int count = 0;  // тут наверное стоит тоже сделать что-то типо вместительности(capacity), а не количество
            int countIn = 0;
            
            int weaponUnique = 0;
            int foodUnique = 0;
            int potionUnique = 0;

            foreach (Slot slot in slots)
            {
                if (slot.isEmpty == false && slot.item != null)  // Можно заменить (slot.isEmpty == false) на (!slot.isEmpty)
                {
                    if (slot.item.itemType == ItemType.Food)
                    {
                        foodInt += slot.amount;
                        foodUnique = 1;
                    }
                    else if (slot.item.itemType == ItemType.Weapon)
                    {
                        weaponInt += slot.amount;
                        weaponUnique = 1;
                    }
                    else if (slot.item.itemType == ItemType.Potions)
                    {
                        potionInt += slot.amount;
                        potionUnique = 1;
                    }
                    
                    countIn++;
                } 
                
                count++;
            }

            print("Кол-во еды в инвентаре: " + foodInt); // Debug.Log($"")
            print("Кол-во оружия в инвентаре: " + weaponInt);
            print("Кол-во зелий в инвентаре: " + potionInt);
            print("Занято " + countIn + " ячейки/ячеек из " + count);

            if (potionUnique == 1 || weaponUnique == 1 || foodUnique == 1)
            {
                int allUnique = 0;
                allUnique = potionUnique + weaponUnique + foodUnique; // можно было не делать второе присвоение и перенести это присвоение это на строчку выше при инициализации
                print("Уникальных предметов: " + allUnique); // Debug.Log($"")
                
            }
        }
    }
    
    public bool AddItem(ItemScriptableObject _item, int _amount) // префикс _
    {
        foreach (Slot slot in slots)
        {
            if (slot.item == _item && (slot.amount + _amount) <= _item.maxAmount)
            {
                slot.amount += _amount;
                slot.itemAmountText.text = slot.amount.ToString();
                return true;
            }
        }

        foreach (Slot slot in slots)
        {
            if (slot.isEmpty)
            {
                slot.item = _item;
                slot.amount += _amount;
                slot.isEmpty = false;
                slot.SetIcon(_item.icon);
                slot.itemAmountText.text = _amount.ToString();
                return true;
            }
        }
        
        Debug.Log("Инвентарь заполнен");
        return false;
    }
}
