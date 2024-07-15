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
        this.guestName = CharacterData.GetItem(key).name;
        this.guestBudget = Random.Range(CharacterData.GetItem(key).budget[0], CharacterData.GetItem(key).budget[1]);
        this.gameObject.SetActive(true);
        this.GetComponentInChildren<SpriteRenderer>().enabled = false;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isChooseCardFinish)
        {
            Debug.Log(this.guestName + "重设了父物件");
            this.transform.SetParent(null);
            this.transform.localPosition = Vector3.zero;
            this.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }
}
