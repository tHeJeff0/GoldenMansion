using DG.Tweening;
using ExcelData;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Filter : MonoBehaviour
{

    bool isInFilterMode = false;

    private void Awake()
    {
        UIController.Instance.FirstFilterStageSelection.Add("职业");
        UIController.Instance.FirstFilterStageSelection.Add("人格");

        foreach (var key in FieldData.GetDict())
        {
            UIController.Instance.JobFilterSelection.Add(key.Key);
        }

        foreach (var key in GuestPersonalData.GetDict())
        {
            UIController.Instance.PersonaFilterSelection.Add(key.Key);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FilterSwitch()
    {
        if (!isInFilterMode)
        {
            UIController.Instance.filterFirstStage.SetActive(true);
            StartCoroutine(ShowFilterSelection());
            isInFilterMode = true;
        }
        else
        {
            StartCoroutine(HideFilterSelection());           
        }

    }

    IEnumerator ShowFilterSelection()
    {
        for (int i = 0; i < 2; i++)
        {
            GameObject button = Instantiate(UIController.Instance.filterSelectionButton,UIController.Instance.filterFirstStage.transform.Find("Content").transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = UIController.Instance.FirstFilterStageSelection[i];
            button.tag = "FirstStageFilterButton";
            yield return new WaitForSecondsRealtime(0.1f);
        }
        
    }

    IEnumerator HideFilterSelection()
    {
        for (int i = 0; i < UIController.Instance.filterFirstStage.transform.Find("Content").childCount+1; i++)
        {
            Destroy(UIController.Instance.filterFirstStage.transform.Find("Content").GetChild(0).gameObject);
            yield return new WaitForSecondsRealtime(0.1f);
        }
        UIController.Instance.filterFirstStage.SetActive(false);
        isInFilterMode = false;
    }

    public IEnumerator ShowJobFilterSelection(string filterType)
    {
        for (int i = 0; i < UIController.Instance.JobFilterSelection.Count; i++)
        {
            GameObject button = Instantiate(UIController.Instance.filterSelectionButton, UIController.Instance.filterSecondStage.transform.Find("FilterScrollView").Find("Viewport").Find("Content"));
            button.GetComponentInChildren<TextMeshProUGUI>().text = LanguageData.GetItem(FieldData.GetItem(UIController.Instance.JobFilterSelection[i]).nameID).CHN;
            yield return new WaitForSecondsRealtime(0.01f);
        }
    }
}
