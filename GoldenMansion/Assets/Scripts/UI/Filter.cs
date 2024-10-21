using DG.Tweening;
using ExcelData;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Filter : MonoBehaviour
{

    bool isInFilterMode = false;

    [SerializeField] GameObject filterFirstStage;
    [SerializeField] GameObject filterSelectionButton;

    private void Awake()
    {
               
        UIController.Instance.FirstFilterStageSelection.Add("职业");
        UIController.Instance.FirstFilterStageSelection.Add("人格");
        UIController.Instance.FirstFilterStageSelection.Add("清除");

        

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

    private void OnEnable()
    {
        if (filterFirstStage.transform.Find("Content").transform.childCount > 0)
        {
            for (int i = 0; i < 3; i++)
            {
                Destroy(filterFirstStage.transform.Find("Content").GetChild(i).gameObject);
            }
            filterFirstStage.SetActive(false);
            isInFilterMode = false;
        }
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
        if (filterFirstStage.transform.Find("Content").transform.childCount == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                GameObject button = Instantiate(filterSelectionButton, filterFirstStage.transform.Find("Content").transform);
                button.GetComponentInChildren<TextMeshProUGUI>().text = UIController.Instance.FirstFilterStageSelection[i];
                button.GetComponentInChildren<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
                button.GetComponent<Image>().color = new Color(0, 0, 0, 1);
                button.tag = new string("type" + i);
                yield return new WaitForSecondsRealtime(0.1f);
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                filterFirstStage.transform.Find("Content").transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitForSecondsRealtime(0.1f);
            }
        }
        
        
    }

    IEnumerator HideFilterSelection()
    {
        for (int i = 0; i < filterFirstStage.transform.Find("Content").childCount+2; i++)
        {
            filterFirstStage.transform.Find("Content").GetChild(0).gameObject.SetActive(false);
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
