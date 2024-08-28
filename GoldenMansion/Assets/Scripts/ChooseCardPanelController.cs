using JetBrains.Annotations;
using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseCardPanelController : MonoBehaviour
{
    [SerializeField] Button showPanelButton;
    [SerializeField] private GameObject storagePanel;
    [SerializeField] private GameObject givePersonaPanel;
    GameObject chooseGuestSlot;
    GameObject hideButton;
    GameObject showButton;
    GameObject skipButton;
    GameObject upgradeRentButton;
    List<Guest> guest = new List<Guest>();

    private Color temporColor;


    // Start is called before the first frame update
    void Start()
    {
        temporColor = this.GetComponent<UnityEngine.UI.Image>().color;
        chooseGuestSlot = GameObject.Find("ChooseGuestSlot");
        hideButton = GameObject.Find("HideButton");
        showButton = GameObject.Find("ShowButton");
        skipButton = GameObject.Find("SkipButton");
        upgradeRentButton = GameObject.Find("UpgradeRentButton");
        guest.AddRange(chooseGuestSlot.GetComponentsInChildren<Guest>());
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isChooseCardFinish)
        {
            GameManager.Instance.isChooseCardFinish = false;
            Destroy(this.gameObject);           
        }
    }

    public void HideChooseCardPanel()
    {
        chooseGuestSlot.SetActive(false);
        hideButton.SetActive(false);
        skipButton.SetActive(false);
        upgradeRentButton.SetActive(false);

        this.GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 0f, 0f, 0f);
        showPanelButton.gameObject.SetActive(true);
        
    }

    public void ShowChooseCardPanel()
    {
        chooseGuestSlot.SetActive(true);
        hideButton.SetActive(true);
        skipButton.SetActive(true);
        upgradeRentButton.SetActive(true);

        this.GetComponent<UnityEngine.UI.Image>().color = temporColor;
        showPanelButton.gameObject.SetActive(false);
    }

    public void SkipChooseCard()
    {
        GameManager.Instance.isChooseCardFinish = true;
        GameManager.Instance.gameDays += 1;
        GameManager.Instance.extraRerollTime = 0;
        
    }

    public void ShowStoragePanel()
    {
        storagePanel.SetActive(true);
    }

    public void StartGivePersona()
    {
        ShowStoragePanel();
    }

    public void ShowGivePersonaPanel()
    {
        givePersonaPanel.SetActive(true);
    }

    public void ReRoll()
    {
        if (GameManager.Instance.basicRerollTime + GameManager.Instance.extraRerollTime > 0)
        {
            foreach (var child in guest)
            {
                child.gameObject.SetActive(false);
                child.gameObject.SetActive(true);
            }
            //chooseGuestSlot.SetActive(false);
            Debug.Log("÷ÿπˆ¡À");
            //chooseGuestSlot.SetActive(true);
            GameManager.Instance.extraRerollTime -= 1;
        }
        
    }
}
