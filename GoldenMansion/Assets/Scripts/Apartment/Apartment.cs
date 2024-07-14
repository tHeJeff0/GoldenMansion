using ExcelData;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using static UnityEngine.ParticleSystem;

public class Apartment : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] GameObject lockedApartment;
    [SerializeField] GameObject unLockedApartment;


    public string roomName { get; set; }
    public int roomKey { get; set; }
    public int roomRent { get; set; }
    public int roomEffect { get; set; }
    public int roomUnlockCost { get; set; }
    public Guest roomGuest { get; set; }
    public int roomGuestLimit { get; set; } = 1;
    public bool isUnlock { get; set; } = false;
    public bool isPayed { get; set; } = false;
    public int apartmentDays { get; set; } = 0;

    private void Awake()
    {
        //if (this.transform.parent.name == "Row1")
        //{
        //    this.isUnlock = true;
        //    this.roomKey = 2;
        //    ApartmentController.Instance.unLockedApartmentCount += 5;
        //}
        //if (this.isUnlock)
        //{
        //    this.lockedApartment.SetActive(false);
        //    this.unLockedApartment.SetActive(true);
        //    ApartmentController.Instance.apartment.Add(this.gameObject);
        //}

        roomKey = 2;//暂时的，后面要改成根据玩家选择输入
        roomName = RoomData.GetItem(roomKey).name;
        roomRent = RoomData.GetItem(roomKey).basicRent;
        roomEffect = RoomData.GetItem(roomKey).effectID;
        roomUnlockCost = RoomData.GetItem(roomKey).unlockCost;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        if (this.transform.parent.name == "Row1")
        {
            this.isUnlock = true;
            this.roomKey = 2;
            ApartmentController.Instance.unLockedApartmentCount += 1;
        }
        if (this.isUnlock)
        {
            this.lockedApartment.SetActive(false);
            this.unLockedApartment.SetActive(true);
            ApartmentController.Instance.apartment.Add(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isUnlock)
        {
            lockedApartment.SetActive(false);
            unLockedApartment.SetActive(true);
        }
        else if (!this.isUnlock)
        {
            lockedApartment.SetActive(true);
            unLockedApartment.SetActive(false);
        }
        if (apartmentDays - GameManager.Instance.gameDays == -1)
        {
            try
            {
                GuestInApartment guestInApartment = gameObject.GetComponentInChildren<GuestInApartment>();
                PayRent(guestInApartment, this);
                apartmentDays += 1;

            }
            catch
            {
                this.isPayed = true;
                apartmentDays += 1;
            }
            
        }

        //switch (roomEffect)
        //{
        //    case 1:
        //        EliteRoomEffect();
        //        break;
        //    case 2:
        //        CageRoomEffect();
        //        break;
        //    case 3:
        //        LuckyRoomEffect();
        //        break;
        //    default:
        //        Debug.LogError("房间没效果");
        //        break;
        //}


    }

    private void EliteRoomEffect()
    {
        if (roomGuest.key == 1)
        {
            this.roomRent = this.roomGuest.guestBudget;
        }
    }

    private void CageRoomEffect()
    {
        float randomRate = Random.Range(0, 1);
        if (randomRate <= 0.5)
        {
            this.roomGuestLimit = 2;
        }
    }

    private void LuckyRoomEffect()
    {
        if (roomGuest.key == 8)
        {
            this.roomRent += 100;
        }
    }

    public void SwitchApartmentPic()
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (ApartmentController.Instance.isBuildMode==true && this.isUnlock == false)
        {
            this.isUnlock = true;
            this.roomKey = 1;
            ApartmentController.Instance.unLockedApartmentCount += 1;
            Debug.Log("已解锁"+this.roomName);
            //ApartmentController.Instance.apartment.Add(this.gameObject);
        }

    }

    public void PayRent(GuestInApartment guestInApartment,Apartment apartment)
    {
        if (guestInApartment.guestBudget >= apartment.roomRent)
        {
            ApartmentController.Instance.vaultMoney += this.roomRent;
        }

    }


    //private void UnlockRoom()
    //{
    //    if (this.isUnlock)
    //    {
    //        lockedApartment.SetActive(false);
    //        unLockedApartment.SetActive(true);
    //    }
    //    else if (!this.isUnlock)
    //    {
    //        lockedApartment.SetActive(true);
    //        unLockedApartment.SetActive(false);
    //    }
    //}


}
