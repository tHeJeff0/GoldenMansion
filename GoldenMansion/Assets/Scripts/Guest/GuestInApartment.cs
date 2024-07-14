using ExcelData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestInApartment : MonoBehaviour
{
    public string guestName { get; set; }
    public string guestDesc { get; set; }
    public int guestBudget { get; set; }
    public int key { get; set; }

    void Awake()
    {
        key = GuestController.Instance.temporKey;
        this.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>(CharacterData.GetItem(key).portraitRoute);
        this.gameObject.SetActive(true);
        this.guestName = CharacterData.GetItem(key).name;
        this.guestBudget = Random.Range(CharacterData.GetItem(key).budget[0], CharacterData.GetItem(key).budget[1]);
        Debug.Log(this.guestName);
        GuestController.Instance.GuestInApartmentPrefabStorage.Add(this.gameObject);
        this.gameObject.SetActive(false);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
