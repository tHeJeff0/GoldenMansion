using DG.Tweening;
using ExcelData;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseCardPanelController : MonoBehaviour
{
    [SerializeField] Button showPanelButton;
    [SerializeField] private GameObject storagePanel;
    [SerializeField] private GameObject givePersonaPanel;
    [SerializeField] private TextMeshProUGUI vaultMoneyText;
    [SerializeField] private TextMeshProUGUI targetText;
    GameObject chooseGuestSlot;
    GameObject hideButton;
    GameObject showButton;
    GameObject skipButton;
    GameObject upgradeRentButton;
    List<Guest> guest = new List<Guest>();

    private Color temporColor;

    void Awake()
    {
        transform.localPosition = new Vector3(transform.parent.position.x, -999.0f, 0);
        transform.DOMoveY(0, 0.4f);
        SaveSystem.Instance.TranslateButtonText();
    }
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
        if (!GameManager.Instance.isEndlessMode)
        {
            string targetTextString;
            switch (GameManager.Instance.Language)
            {
                case 1:
                    targetTextString = "{0}天后上交租金:{1}";
                    break;
                case 2:
                    targetTextString = "{0}days left to pey rent:{1}";
                    break;
                case 3:
                    targetTextString = "{0}天后上交租金:{1}";
                    break;
                default:
                    targetTextString = "{0}天后上交租金:{1}";
                    break;
            }
            targetText.text = string.Format(targetTextString, Level.GetItem(GameManager.Instance.levelKey).days - GameManager.Instance.gameDays, Level.GetItem(GameManager.Instance.levelKey).target);
        }
        else
        {
            string targetTextString;
            switch (GameManager.Instance.Language)
            {
                case 1:
                    targetTextString = "{0}天后上交租金:{1}";
                    break;
                case 2:
                    targetTextString = "{0}days left to pey rent:{1}";
                    break;
                case 3:
                    targetTextString = "{0}天后上交租金:{1}";
                    break;
                default:
                    targetTextString = "{0}天后上交租金:{1}";
                    break;
            }
            targetText.text = string.Format(targetTextString, GameManager.Instance.endlessModeDays - GameManager.Instance.gameDays, GameManager.Instance.endlessModeTarget);
        }
        
        vaultMoneyText.text = ApartmentController.Instance.vaultMoney.ToString();
        if (GameManager.Instance.isChooseCardFinish)
        {
            GameManager.Instance.isChooseCardFinish = false;
            Destroy(gameObject);           
        }

        
    }

    public void HideChooseCardPanel()
    {
        transform.DOMoveY(-20, 0.4f);
        showPanelButton.gameObject.SetActive(true);
        
    }

    public void ShowChooseCardPanel()
    {
        transform.DOMoveY(0, 0.4f);
        showPanelButton.gameObject.SetActive(false);
    }

    public void SkipChooseCard()
    {
        GameManager.Instance.isChooseCardFinish = true;
        GameManager.Instance.gameDays += 1;        
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
        
        if (0 < GameManager.Instance.rerollTime)
        {
            foreach (var child in guest)
            {
                child.gameObject.SetActive(false);
                child.gameObject.SetActive(true);
            }
            GameObject.Find("RerollButton").GetComponent<Button>().interactable = false;
            GameObject.Find("RerollButton").GetComponent<Button>().interactable = true;
            Debug.Log("重滚了");
            GameManager.Instance.rerollTime -= 1;
        }
        else
        {
            GameObject.Find("RerollButton").GetComponent<Button>().interactable = false;
            GameObject.Find("RerollButton").GetComponent<Button>().interactable = true;
        }
        
    }
}
