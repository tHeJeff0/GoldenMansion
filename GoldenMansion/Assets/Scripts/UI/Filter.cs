using DG.Tweening;
using ExcelData;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Filter : MonoBehaviour
{

    bool isInFilterMode = false;

    [SerializeField] GameObject filterFirstStage;
    [SerializeField] GameObject filterSelectionButton;

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
            filterFirstStage.SetActive(true);
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
            GameObject button = Instantiate(filterSelectionButton, filterFirstStage.transform.Find("Content").transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = UIController.Instance.FirstFilterStageSelection[i];
            button.tag = new string("type" + i);
            yield return new WaitForSecondsRealtime(0.1f);
        }
        
    }

    IEnumerator HideFilterSelection()
    {
        for (int i = 0; i < filterFirstStage.transform.Find("Content").childCount+1; i++)
        {
            Destroy(filterFirstStage.transform.Find("Content").GetChild(0).gameObject);
            yield return new WaitForSecondsRealtime(0.1f);
        }
        filterFirstStage.SetActive(false);
        isInFilterMode = false;
    }

    public IEnumerator ShowJobFilterSelection(GameObject jobFilterSelection,GameObject parent)
    {
        for (int i = 0; i < UIController.Instance.JobFilterSelection.Count; i++)
        {
            GameObject button = Instantiate(jobFilterSelection, parent.transform);
            button.GetComponent<JobFilterSelection>().key = UIController.Instance.JobFilterSelection[i];
            yield return new WaitForSecondsRealtime(0.001f);
        }        
    }

    public IEnumerator ShowPersonaFilterSelection(GameObject personaFilterSelection,GameObject parent)
    {
        for (int i = 0; i < UIController.Instance.PersonaFilterSelection.Count; i++)
        {
            GameObject button = Instantiate(personaFilterSelection, parent.transform);
            button.GetComponent<PersonaFilterSelection>().key = UIController.Instance.PersonaFilterSelection[i];
            yield return new WaitForSecondsRealtime(0.001f);
        }
    }

   
}
