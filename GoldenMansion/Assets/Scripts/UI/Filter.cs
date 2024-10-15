using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filter : MonoBehaviour
{
    [SerializeField] GameObject filterFirstStage;
    [SerializeField] GameObject filterSecondStage;

    bool isInFilterMode = false;
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
        yield return filterFirstStage.transform.Find("Persona").DOLocalMoveY(34.0f, 1f).WaitForCompletion();
        yield return filterFirstStage.transform.Find("Job").DOLocalMoveY(66.0f, 1f).WaitForCompletion();
        yield return filterFirstStage.transform.Find("BottomPic").DOScaleY(1.0f, 1f).WaitForCompletion();
    }

    IEnumerator HideFilterSelection()
    {
        yield return filterFirstStage.transform.Find("Job").DOLocalMoveY(0.0f, 1f).WaitForCompletion();
        yield return filterFirstStage.transform.Find("Persona").DOLocalMoveY(0.0f, 1f).WaitForCompletion();       
        yield return filterFirstStage.transform.Find("BottomPic").DOScaleY(0.0f, 1f).WaitForCompletion();
        filterFirstStage.SetActive(false);
        isInFilterMode = false;
    }
}
