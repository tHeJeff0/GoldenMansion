using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ExcelData;
using TMPro;

public class Guest : MonoBehaviour
{
    [SerializeField] GameObject guestCardNameTextPrefab;
    //[SerializeField] GameObject guestCardDescTextPrefab;
    [SerializeField] GameObject guestCardBudgetTextPrefab;
    

    public int key { get; set; }
    public int guestPrice { get; set; }
    public float guestCost { get; set; }
    public float guestExtraCost { get; set; }
    public int guestDays { get; set; }
    public string guestName { get; set; }
    public string guestDesc { get; set; }
    public int guestBudget { get; set; }

    private TextMeshProUGUI guestNameText;
    private TextMeshProUGUI guestDescText;
    private TextMeshProUGUI guestBudgetText;


    private void OnEnable()
    {
        key = GuestController.Instance.RandomKey();
        guestName = CharacterData.GetItem(key).name;
        guestBudget = CharacterData.GetItem(key).budget;
        guestPrice = CharacterData.GetItem(key).basicPrice;
        guestCost = CharacterData.GetItem(key).basicCost;
        guestNameText = guestCardNameTextPrefab.GetComponent<TextMeshProUGUI>();
        guestBudgetText = guestCardBudgetTextPrefab.GetComponent<TextMeshProUGUI>();

        guestNameText.text = guestName;
        guestBudgetText.text = guestBudget.ToString();


        //INFP����Ч��
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
        if (ApartmentController.Instance.vaultMoney >= guestCost)
        {
            ApartmentController.Instance.vaultMoney -= guestCost;
            UIController.Instance.UpdateVaultMoneyText();
            GuestController.Instance.temporKey = this.key;
            GameObject guestInvited = Instantiate(GuestController.Instance.guestInApartmentPrefab.gameObject);
            guestInvited.GetComponentInChildren<SpriteRenderer>().enabled = false;
            GuestController.Instance.GuestInApartmentPrefabStorage.Add(guestInvited);
            Destroy(gameObject);
            //GameManager.Instance.isChooseCardFinish = true;
        }


    }

    
}
