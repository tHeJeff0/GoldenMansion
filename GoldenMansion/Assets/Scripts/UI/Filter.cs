using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Filter : MonoBehaviour
{
    [SerializeField] GameObject filterFirstStage;
    [SerializeField] GameObject filterSecondStage;
    [SerializeField] GameObject filterSelectionButton;

    bool isInFilterMode = false;

    private void Awake()
    {
        UIController.Instance.FirstFilterStageSelection.Add("职业");
        UIController.Instance.FirstFilterStageSelection.Add("人格");
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
            GameObject button = Instantiate(filterSelectionButton,filterFirstStage.transform.Find("Content").transform);
            button.GetComponentInChildren<TextMeshProUGUI>().text = UIController.Instance.FirstFilterStageSelection[i];
            button.tag = "FirstStageFilterButton";
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
}
