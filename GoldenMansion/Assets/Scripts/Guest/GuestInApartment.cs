using ExcelData;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GuestInApartment : MonoBehaviour
{
    public int key { get; set; }
    public int guestDays { get; set; }
    public string guestName { get; set; }
    public string guestDesc { get; set; }
    public int guestBudget { get; set; }
    public int guestExtraBudget { get; set; }
    public int guestEffectID { get; set; }
    public bool isMoveIn { get; set; } = false;

    void Awake()
    {
        key = GuestController.Instance.temporKey;
        this.guestDays = GameManager.Instance.gameDays;
        this.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>(CharacterData.GetItem(key).portraitRoute);
        this.guestName = CharacterData.GetItem(key).name;
        this.guestBudget = Random.Range(CharacterData.GetItem(key).budget[0], CharacterData.GetItem(key).budget[1]);
        this.guestEffectID = CharacterData.GetItem(key).effectID;
        //this.guestEffectID = 1;
        this.gameObject.SetActive(true);
        this.GetComponentInChildren<SpriteRenderer>().enabled = false;
        this.GetComponent<BoxCollider>().enabled = false;

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (GameManager.Instance.isChooseCardFinish||GameManager.Instance.isRoundEnd)
        {
            Reset();
        }
        if (guestDays - GameManager.Instance.gameDays == -1)
        {
            guestDays += 1;
        }
    }

    public void GuestEffect()
    {
        switch (this.guestEffectID)
        {
            case 1:
                GuestController.Instance.GuestEffect_ChangeJob(this);
                break;
            case 2:
                GuestController.Instance.GuestEffect_IgnoreRoomRentLimit(this);
                break;
            case 3:
                GuestController.Instance.GuestEffect_PayByNeighbour(this);
                break;
            case 4:
                GuestController.Instance.GuestEffect_RateMoveAway(this);
                break;
            case 5:
                GuestController.Instance.GuestEffect_RentIncrease(this);
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

    public void Reset()
    {
        this.transform.SetParent(null);
        this.transform.localPosition = Vector3.zero;
        this.GetComponentInChildren<SpriteRenderer>().enabled = false;
        this.GetComponent<BoxCollider>().enabled = false;
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.black;
    //    Gizmos.DrawWireCube(this.transform.position, this.transform.parent.GetComponent<BoxCollider>().size);
    //}



}
