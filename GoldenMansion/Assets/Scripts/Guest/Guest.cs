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
    public int guestCost { get; set; }
    public int guestDays { get; set; }
    public string guestName { get; set; }
    public string guestDesc { get; set; }
    public int guestBudget { get; set; }

    private TextMeshProUGUI guestNameText;
    private TextMeshProUGUI guestDescText;
    private TextMeshProUGUI guestBudgetText;


    private void Awake()
    {
        key = GuestController.Instance.RandomKey();
        Debug.Log(key);
        guestName = CharacterData.GetItem(key).name;
        //guestDesc = LanguageData.GetItem(CharacterData.GetItem(key).field).CHN;
        guestBudget = CharacterData.GetItem(key).budget;
        guestPrice = CharacterData.GetItem(key).basicPrice;
        guestCost = CharacterData.GetItem(key).basicCost;
        guestNameText = guestCardNameTextPrefab.GetComponent<TextMeshProUGUI>();
        //guestDescText = guestCardDescTextPrefab.GetComponent<TextMeshProUGUI>();
        guestBudgetText = guestCardBudgetTextPrefab.GetComponent<TextMeshProUGUI>();

        guestNameText.text = guestName;
        //guestDescText.text = guestDesc;
        guestBudgetText.text = guestBudget.ToString();
    }
        
        



    public void addToStorage()
    {
        GuestController.Instance.temporKey = this.key;
        GameObject guestInvited = Instantiate(GuestController.Instance.guestInApartmentPrefab.gameObject);
        guestInvited.GetComponentInChildren<SpriteRenderer>().enabled = false;
        GuestController.Instance.GuestInApartmentPrefabStorage.Add(guestInvited);
        GameManager.Instance.isChooseCardFinish = true;
       
    }

    
}
