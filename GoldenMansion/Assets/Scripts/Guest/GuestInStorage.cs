using ExcelData;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuestInStorage : MonoBehaviour
{
    public int key { get; set; }
    public int priceShown { get; set; }
    public int basicPrice { get; set; }
    public int extraPrice { get; set; }
    public string portraitRoute { get; set; }
    public int mbtiID { get; set; }


    private TextMeshProUGUI priceText;
    private Image guestPortrait;
    public GameObject guestInStoragePrefab;

    public List<int> personaID = new List<int>();

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        priceShown = basicPrice + extraPrice;

        guestPortrait = this.GetComponentsInChildren<Image>()[0];
        priceText = this.GetComponentInChildren<TextMeshProUGUI>();

        portraitRoute = CharacterData.GetItem(key).portraitRoute;
        guestPortrait.sprite = Resources.Load<Sprite>(portraitRoute);
        priceText.text = priceShown.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnSell()
    {
        ApartmentController.Instance.vaultMoney += priceShown;
    }

    

    
}
