using ExcelData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuestInStorage : MonoBehaviour
{
    public int key;
    public int priceShown { get; set; }
    public int basicPrice { get; set; }
    public int extraPrice { get; set; }
    public string portraitRoute { get; set; }
    public int mbtiID { get; set; }
    private Image guestPortrait;
    public GameObject guestInStoragePrefab;

    public List<int> personaID = new List<int>();

    private void Awake()
    {
        priceShown = basicPrice + extraPrice;
        //portraitRoute = CharacterData.GetItem(key).portraitRoute;
        //guestPortrait = this.GetComponentInChildren<Image>();
        //guestPortrait.sprite = Resources.Load<Sprite>(portraitRoute);
    }
    // Start is called before the first frame update
    void Start()
    {
        
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
