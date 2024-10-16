using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FilterSelection : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler, IPointerClickHandler
{
    public bool isSelected { get; set; }
    public int fieldKey { get; set; }
    public int personaKey { get; set; }
    public bool isIntern { get; set; }

    public string buttonName { get; set; }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isSelected)
        {
            GetComponentInChildren<TextMeshProUGUI>().color = new Color(0, 0, 0, 1);
            GetComponent<Image>().color = new Color(1, 1, 1, 1);            
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isSelected)
        {
            GetComponentInChildren<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
            GetComponent<Image>().color = new Color(0, 0, 0, 1);
        }
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        FilterSelect();
    }

    void FilterSelect()
    {
        if (!isSelected)
        {
            isSelected = true;
            GetComponentInChildren<TextMeshProUGUI>().color = new Color(0, 0, 0, 1);
            GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else
        {
            isSelected = false;
            GetComponentInChildren<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
            GetComponent<Image>().color = new Color(0, 0, 0, 1);
        }
    }
}
