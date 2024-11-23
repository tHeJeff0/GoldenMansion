using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    Button continueButton;

    private void Awake()
    {
        continueButton = GetComponent<Button>();
        continueButton.interactable = false;
        continueButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0, 0, 0, 0.5f);    
        if(File.Exists(Path.Combine(Application.persistentDataPath, "save.sav")))
        {
            continueButton.interactable = true;
            continueButton.GetComponentInChildren<TextMeshProUGUI>().color = new Color(0, 0, 0, 255);
        }
        
    }
    
}
