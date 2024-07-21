using ExcelData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using static UnityEngine.ParticleSystem;

public class Apartment : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] GameObject lockedApartment;
    [SerializeField] GameObject unLockedApartment;
    [SerializeField] GameObject upgradeSelection;
    [SerializeField] GameObject[] upgradeSelections;


    public string roomName { get; set; }
    public string roomPicRoute { get; set; }
    public int roomKey { get; set; }
    public int roomRent { get; set; }
    public int roomExtraRent { get; set; }
    public int roomEffect { get; set; }
    public int roomUnlockCost { get; set; }
    public Guest roomGuest { get; set; }
    public int roomGuestLimit { get; set; } = 1;
    public int roomGuestExtraLimit { get; set; }
    public bool isUnlock { get; set; } = true;
    public bool isPayed { get; set; } = false;
    public bool isUpgradeMode { get; set; } = false;
    public int apartmentDays { get; set; } = 0;

    private void Awake()
    {
        
        roomKey = 1;//暂时的，后面要改成根据玩家选择输入
        roomName = RoomData.GetItem(roomKey).name;
        roomRent = RoomData.GetItem(roomKey).basicRent;
        roomEffect = RoomData.GetItem(roomKey).effectID;
        roomUnlockCost = RoomData.GetItem(roomKey).unlockCost;
        upgradeSelection.SetActive(false);
        //unLockedApartment.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(RoomData.GetItem(roomKey).roomPicRoute);

    }

    // Start is called before the first frame update
    void Start()
    {
        ApartmentController.Instance.apartment.Add(this.gameObject);

        //if (this.transform.parent.name == "Row1")
        //{
        //    this.isUnlock = true;
        //    this.roomKey = 1;
        //    //ApartmentController.Instance.unLockedApartmentCount += 1;
        //}
        //if (this.isUnlock)
        //{
        //    Debug.Log("解锁第一行");
        //    this.lockedApartment.SetActive(false);
        //    this.unLockedApartment.SetActive(true);
        //    
        //}
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
                this.isPayed = true;
                apartmentDays += 1;

            }
            catch
            {
                this.isPayed = true;
                apartmentDays += 1;
            }
            
        }

        if (this.isUpgradeMode)
        {
            foreach (var upgradeSelection in upgradeSelections)
            {
                if (upgradeSelection.GetComponent<ApartmentUpgrade>().roomKeyIsSend)
                {
                    this.roomKey = ApartmentController.Instance.apartmentUpgradeKey;
                    roomName = RoomData.GetItem(roomKey).name;
                    roomRent = RoomData.GetItem(roomKey).basicRent;
                    roomEffect = RoomData.GetItem(roomKey).effectID;
                    ApartmentController.Instance.apartment.Remove(this.gameObject);
                    ApartmentController.Instance.apartment.Add(this.gameObject);
                    foreach (var selection in upgradeSelections)
                    {
                        selection.GetComponent<ApartmentUpgrade>().roomKeyIsSend = false;
                        selection.SetActive(false);                       
                    }
                    isUpgradeMode = false;
                    Debug.Log(string.Format("{0}被升级为了{1}", this.roomName, RoomData.GetItem(roomKey).name));
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject != this.gameObject && !upgradeSelections.Contains(hit.transform.gameObject))
                {
                    isUpgradeMode = false;
                    upgradeSelection.SetActive(false);
                }                
            }
        }

    }


    public void SwitchApartmentPic()
    {

    }

    public void ApartmentEffect()
    {
        switch (roomKey)
        {
            case 2:
                ApartmentController.Instance.ApartmentEffect_IgnoreBudget(GetComponentInChildren<GuestInApartment>(),this);
                break;
            case 3:
                ApartmentController.Instance.ApartmentEffect_LiveTwoGuest(this);
                break;
            case 4:
                ApartmentController.Instance.ApartmentEffect_IncreaseRent(GetComponentInChildren<GuestInApartment>(),this);
                break;
            default:
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (ApartmentController.Instance.isBuildMode==true && this.isUnlock == false)
        {
            this.isUnlock = true;
            this.roomKey = 1;
            ApartmentController.Instance.apartment.Add(this.gameObject);
            Debug.Log("已解锁"+this.roomName);
        }
        else if (ApartmentController.Instance.isBuildMode && this.isUnlock)
        {
            if (GameObject.FindGameObjectWithTag("UpgradeButton") != null)
            {
                GameObject.FindGameObjectWithTag("UpgradeButton").SetActive(false);
            }
            upgradeSelection.SetActive(true);
            this.isUpgradeMode = true;
            Debug.Log("调出升级房屋");
        }

    }

    

    public void PayRent(GuestInApartment guestInApartment,Apartment apartment)
    {
        StartCoroutine(WaitGuestMoveIn());
        Debug.Log("结束等待");
        guestInApartment.GuestEffect();
        apartment.ApartmentEffect();
        if (guestInApartment.guestBudget + guestInApartment.guestExtraBudget >= apartment.roomRent + apartment.roomExtraRent)
        {
            ApartmentController.Instance.vaultMoney += this.roomRent + this.roomExtraRent;
            Debug.Log(string.Format("{0}入住{1},上交房租{2},效果ID是{3}", GetComponentInChildren<GuestInApartment>().guestName, this.roomName, this.roomRent + this.roomExtraRent, GetComponentInChildren<GuestInApartment>().guestEffectID));
        }
        else
        {
            Debug.Log(string.Format("{0}入住{1},但没交房租", GetComponentInChildren<GuestInApartment>().guestName, this.roomName));
        }
        
        this.roomExtraRent = 0;
    }

    IEnumerator WaitGuestMoveIn()
    {
        Debug.Log("开始等待");
        yield return new WaitUntil(() => GuestController.Instance.isAllGuestMoveIn);
    }
}
