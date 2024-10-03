using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ExcelData;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

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

    //private TextMeshProUGUI guestNameText;
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
                inviteButton.SetActive(false);
            }

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != GetComponent<BoxCollider>()&&hit.transform.name!="InviteButton")
                {
                    isSelected = false;
                    inviteButton.SetActive(false);
                }
            }
        }
    }

    private void OnEnable()
    {
        key = GuestController.Instance.RandomKey();
        guestName = CharacterData.GetItem(key).name;
        guestBudget = CharacterData.GetItem(key).budget;
        guestPrice = CharacterData.GetItem(key).basicPrice;
        guestCost = CharacterData.GetItem(key).basicCost;
        guestDesc = guestCardDescPrefab.GetComponentInChildren<TextMeshProUGUI>().text;
        guestBudgetText = guestCardBudgetTextPrefab.GetComponent<TextMeshProUGUI>();
        guestPortrait.GetComponent<Image>().sprite = Resources.Load<Sprite>(CharacterData.GetItem(key).portraitRoute);

        //guestNameText.text = guestName;
        guestBudgetText.text = guestBudget.ToString();


        //INFP技能效果
        foreach (var guest in GuestController.Instance.GuestInApartmentPrefabStorage)
        {
            if (guest.GetComponent<GuestInApartment>().mbti == 1458)
            {
                guestExtraCost = guestCost * 0.5f;
                break;
            }
        }
    }
               

    public void addToStorage()
    {
        if (GameManager.Instance.isAllowBuy)
        {
            if (ApartmentController.Instance.vaultMoney >= guestCost)
            {
                ApartmentController.Instance.vaultMoney -= guestCost;
                //UIController.Instance.UpdateVaultMoneyText();
                GuestController.Instance.temporKey = this.key;
                GameObject guestInvited = Instantiate(GuestController.Instance.guestInApartmentPrefab.gameObject);
                guestInvited.GetComponentInChildren<SpriteRenderer>().enabled = false;
                GuestController.Instance.GuestInApartmentPrefabStorage.Add(guestInvited);
                this.gameObject.SetActive(false);
                SkillController.Instance.SkillTrigger_EShop("buy");
                if (key == 49 || key == 50 || key == 51)
                {
                    SkillController.Instance.Skill_MediaBanShop();
                }
            }
        }
        else
        {
            Debug.Log("不允许购买");
        }     

    }

    private void OnMouseEnter()
    {
        transform.DOShakePosition(0.1f,1.5f);
        guestCardDescPrefab.SetActive(true);
        int skillID = FieldData.GetItem(CharacterData.GetItem(key).field).skillID;
        int languageID = SkillData.GetItem(skillID).descID;
        guestCardDescPrefab.GetComponentInChildren<TextMeshProUGUI>().text = LanguageData.GetItem(languageID).CHN;
    }

    private void OnMouseExit()
    {
        guestCardDescPrefab.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (!isSelected)
        {
            inviteButton.SetActive(true);
            isSelected = true;
        }
        else
        {
            inviteButton.SetActive(false);
            isSelected = false;
        }
        
    }


}
