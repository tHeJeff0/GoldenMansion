using JetBrains.Annotations;
using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChooseCardPanelController : MonoBehaviour
{
    [SerializeField] Button showPanelButton;
    private Color temporColor;


    // Start is called before the first frame update
    void Start()
    {
        temporColor = this.GetComponent<UnityEngine.UI.Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isChooseCardFinish)
        {
            Destroy(this.gameObject);
            GameManager.Instance.isChooseCardFinish = false;
        }
    }

    public void HideChooseCardPanel()
    {
        GameObject.Find("ChooseGuestSlot").SetActive(false);
        GameObject.Find("HideButton").SetActive(false);
        GameObject.Find("SkipButton").SetActive(false);
        GameObject.Find("UpgradeRentButton").SetActive(false);

        this.GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 0f, 0f, 0f);
        showPanelButton.gameObject.SetActive(true);
        
    }

    public void ShowChooseCardPanel()
    {
       
    }

    public void SkipChooseCard()
    {
        Destroy(this.gameObject);
    }
}
