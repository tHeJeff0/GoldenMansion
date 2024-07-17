using ExcelData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestInApartment : MonoBehaviour
{
    
    public int key { get; set; }
    public int guestDays { get; set; }
    public string guestName { get; set; }
    public string guestDesc { get; set; }
    public int guestBudget { get; set; }
    public int guestEffectID { get; set; }


    void Awake()
    {
        key = GuestController.Instance.temporKey;
        this.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>(CharacterData.GetItem(key).portraitRoute);
        this.guestName = CharacterData.GetItem(key).name;
        this.guestBudget = Random.Range(CharacterData.GetItem(key).budget[0], CharacterData.GetItem(key).budget[1]);
        this.guestEffectID = CharacterData.GetItem(key).effectID;
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
        if (guestDays - GameManager.Instance.gameDays == -1)
        {
            guestDays += 1;
        }
    }

    public void GuestEffect()
    {
        switch (guestEffectID)
        {
            case 1:
                GuestController.Instance.GuestEffect_ChangeJob();
                break;
            case 2:
                GuestController.Instance.GuestEffect_IgnoreRoomRentLimit();
                break;
            case 3:
                GuestController.Instance.GuestEffect_PayByNeighbour();
                break;
            case 4:
                GuestController.Instance.GuestEffect_RateMoveAway();
                break;
            case 5:
                GuestController.Instance.GuestEffect_RentIncrease();
                break;
            case 6:
                GuestController.Instance.GuestEffect_RandomBudget();
                break;
            case 7:
                GuestController.Instance.GuestEffect_MoveInNextBy();
                break;
            case 8:
                GuestController.Instance.GuestEffect_RentIncreaseByNeighbour();
                break;
            case 9:
                GuestController.Instance.GuestEffect_GenerateGuest();
                break;
            default:
                break;
        }
    }
}
