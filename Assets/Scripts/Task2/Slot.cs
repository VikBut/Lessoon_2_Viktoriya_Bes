using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, 
    IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
{
    private Color m_defaultColor;
    
    public ItemScriptableObject item;
    public int amount;
    public bool isEmpty = true;
    public TMP_Text itemAmountText;
    public GameObject iconGameObject;
    
    private void Awake()
    {
        iconGameObject = transform.GetChild(0).gameObject;
        itemAmountText = transform.GetChild(1).GetComponent<TMP_Text>();
    }
    
    public void SetIcon(Sprite icon)
    {
        m_defaultColor = iconGameObject.GetComponent<Image>().color;
        iconGameObject.GetComponent<Image>().sprite = icon;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && isEmpty)
        {
            Debug.Log("Ячейка пуста");
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            amount -= 1;

            if (amount <= 0)
            {
                amount = 0;
                item = null;
                isEmpty = true;
                itemAmountText.text = "";
            
                Image icon = iconGameObject.GetComponent<Image>(); // Стоит уточнить переменную, например iconImage
                icon.sprite = null;
                icon.color = m_defaultColor;
                
                Debug.Log("Все предмемты удалены");
            }
            else
            {
                itemAmountText.text = amount.ToString();
                Debug.Log("Предмет удален, осталось: " + amount); // $""
            }
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (iconGameObject.GetComponent<Image>().sprite != null)
        {
            iconGameObject.GetComponent<Image>().color = Color.darkRed;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        iconGameObject.GetComponent<Image>().color = m_defaultColor;
    }
}

    

