using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ExcelData;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;

public class Guest : MonoBehaviour
{
    //[SerializeField] GameObject guestCardNameTextPrefab;
    [SerializeField] GameObject guestCardDescPrefab;
    [SerializeField] GameObject guestCardBudgetTextPrefab;
    [SerializeField] GameObject inviteButton;

    public int key { get; set; }
    public int guestPrice { get; set; }
    public float guestCost { get; set; }
    public float guestExtraCost { get; set; }
    public int guestDays { get; set; }
    public string guestName { get; set; }
    public string guestDesc { get; set; }
    public int guestBudget { get; set; }

    private bool isSelected { get; set; } = false;

    public GameObject guestPortrait;

    private TextMeshProUGUI guestDescText;
    private TextMeshProUGUI guestBudgetText;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray) != this.gameObject)
            {

                isSelected = false;
                transform.DOLocalMoveY(150, 0.05f);
                inviteButton.SetActive(false);
            }

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != GetComponent<BoxCollider>()&&hit.transform.name!="InviteButton")
                {
                    isSelected = false;
                    transform.DOLocalMoveY(150, 0.05f);
                    inviteButton.SetActive(false);
                }
            }
        }
    }

    private void OnEnable()
    {
        key = GuestController.Instance.RandomKey();
        LoadGuestMessage(key);
               
    }
               

    public void addToStorage()
    {
        if (GameManager.Instance.isAllowBuy)
        {
            if (ApartmentController.Instance.vaultMoney >= guestCost)
            {
                ApartmentController.Instance.vaultMoney -= guestCost;
                GuestController.Instance.temporKey = this.key;
                Vector3 instantiatePosition = new Vector3(3.55f,-7.35f,0);
                GameObject guestInvited = Instantiate(GuestController.Instance.guestInApartmentPrefab.gameObject, instantiatePosition,transform.rotation);
                guestInvited.GetComponentInChildren<SpriteRenderer>().enabled = false;
                GuestController.Instance.GuestInApartmentPrefabStorage.Add(guestInvited);
                inviteButton.SetActive(false);
                transform.DOLocalMoveY(150, 0.05f);
                isSelected = false;
                gameObject.SetActive(false);
                SkillController.Instance.SkillTrigger_EShop("buy");
                if (key == 49 || key == 50 || key == 51)
                {
                    SkillController.Instance.Skill_MediaBanShop();
                }
                key = 0;
            }           
        }
        else
        {
            Debug.Log("不允许购买");
        }     

    }

    

    private void OnMouseEnter()
    {
        transform.DOLocalMoveY(160, 0.05f);

        guestCardDescPrefab.SetActive(true);
        int fieldID = CharacterData.GetItem(key).field;
        int skillID = FieldData.GetItem(fieldID).skillID;
        int languageID = SkillData.GetItem(skillID).descID + 300;
        //string conditionValue = GuestController.Instance.GetLanguageConditionValue(fieldID).ToString();
        //string effectValue = GuestController.Instance.GetLanguageEffectValue(fieldID).ToString();
        switch (GameManager.Instance.Language)
        {
            case 1:
                guestCardDescPrefab.GetComponentInChildren<TextMeshProUGUI>().text = LanguageData.GetItem(languageID).CHN;
                //string.Format(LanguageData.GetItem(languageID).CHN, conditionValue,effectValue);
                //LanguageData.GetItem(languageID).CHN;
                break;
            case 2: guestCardDescPrefab.GetComponentInChildren<TextMeshProUGUI>().text = LanguageData.GetItem(languageID).ENG;
                break;
            case 3: guestCardDescPrefab.GetComponentInChildren<TextMeshProUGUI>().text = LanguageData.GetItem(languageID).TCHN;
                break;
            default: guestCardDescPrefab.GetComponentInChildren<TextMeshProUGUI>().text = LanguageData.GetItem(languageID).CHN;
                break;
        }
        
    }

    private void OnMouseExit()
    {
        if (isSelected == false)
        {
            transform.DOLocalMoveY(150, 0.05f);
        }   
        guestCardDescPrefab.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (!isSelected)
        {
            inviteButton.SetActive(true);
            transform.DOLocalMoveY(160, 0.05f);
            transform.DOShakePosition(0.04f, 10);
            isSelected = true;
        }
        else
        {
            inviteButton.SetActive(false);
            transform.DOLocalMoveY(150, 0.05f);
            isSelected = false;
        }
        
    }

    void TranslateInviteButton(int languageID)
    {
        switch (languageID)
        {
            case 1:
                transform.Find("InviteButton").GetComponentInChildren<TextMeshProUGUI>().text = "买 入";
                break;
            case 2:
                transform.Find("InviteButton").GetComponentInChildren<TextMeshProUGUI>().text = "BUY";
                break;
            case 3:
                transform.Find("InviteButton").GetComponentInChildren<TextMeshProUGUI>().text = "買 入";
                break;
        }

    }

    void TranslateBudgetTitle(int languageID)
    {
        switch (languageID)
        {
            case 1:
                transform.Find("GuestCard").Find("BudgetTitleText").GetComponent<TextMeshProUGUI>().text = "房租:";
                break;
            case 2:
                transform.Find("GuestCard").Find("BudgetTitleText").GetComponent<TextMeshProUGUI>().text = "Budget:";
                break;
            case 3:
                transform.Find("GuestCard").Find("BudgetTitleText").GetComponent<TextMeshProUGUI>().text = "房租:";
                break;
        }
    }

    public void LoadGuestMessage(int key)
    {
        guestName = CharacterData.GetItem(key).name;
        guestBudget = CharacterData.GetItem(key).budget;
        guestPrice = CharacterData.GetItem(key).basicPrice;
        guestCost = CharacterData.GetItem(key).basicCost;
        guestDesc = guestCardDescPrefab.GetComponentInChildren<TextMeshProUGUI>().text;
        guestBudgetText = guestCardBudgetTextPrefab.GetComponent<TextMeshProUGUI>();
        guestPortrait.GetComponent<Image>().sprite = Resources.Load<Sprite>(CharacterData.GetItem(key).portraitRoute);
        guestBudgetText.text = guestBudget.ToString();
        inviteButton.transform.Find("Cost").Find("BudgetTitleText").GetComponent<TextMeshProUGUI>().text = "-"+(CharacterData.GetItem(key).basicCost + guestExtraCost).ToString();

        //INFP技能效果
        foreach (var guest in GuestController.Instance.GuestInApartmentPrefabStorage)
        {
            if (guest.GetComponent<GuestInApartment>().mbti == 1458)
            {
                guestExtraCost = guestCost * 0.5f;
                break;
            }
        }

        TranslateBudgetTitle(GameManager.Instance.Language);
        TranslateInviteButton(GameManager.Instance.Language);
    }
}
