using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FilterSelection : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler, IPointerClickHandler
{
    public bool isSelected { get; set; } = false;
    public int fieldKey { get; set; }
    public int personaKey { get; set; }
    public bool isIntern { get; set; }

    public string buttonName { get; set; }

    [SerializeField] GameObject JobFilterButton;
    [SerializeField] GameObject PersonaFilterButton;
    [SerializeField] GameObject SecondStepTransform;

    Filter filter = new Filter();


    private void Start()
    {
        if (CompareTag("type0"))
        {
            StartCoroutine(filter.ShowJobFilterSelection(JobFilterButton, SecondStepTransform.transform.Find("FilterScrollView").Find("Viewport").Find("Content").gameObject));
            SecondStepTransform.SetActive(false);
        }
        else if (CompareTag("type1"))
        {
            StartCoroutine(filter.ShowPersonaFilterSelection(PersonaFilterButton, SecondStepTransform.transform.Find("FilterScrollView").Find("Viewport").Find("Content").gameObject));
            SecondStepTransform.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isSelected)
        {
            GetComponentInChildren<TextMeshProUGUI>().color = new Color(0, 0, 0, 1);
            GetComponent<Image>().color = new Color(1, 1, 1, 1);
            SecondStepTransform.SetActive(true);
        }
        
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isSelected)
        {
            SecondStepTransform.SetActive(false);
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
