using ExcelData;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneStoryText : MonoBehaviour
{
    int languageID = 0;
    int storyID = 0;

    public void GetSceneStoryText(int storyID)
    {
        languageID = GameManager.Instance.Language;
        storyID = SceneStoryData.GetItem(storyID).languageID;
        switch (languageID)
        {
            case 1:
                GetComponent<TextMeshProUGUI>().text = LanguageData.GetItem(storyID).CHN;
                break;
            case 2:
                GetComponent<TextMeshProUGUI>().text = LanguageData.GetItem(storyID).ENG;
                break;
            case 3:
                GetComponent<TextMeshProUGUI>().text = LanguageData.GetItem(storyID).TCHN;
                break;
        }
        
    }
}
