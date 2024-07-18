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
    public bool isUnlock { get; set; } = false;
    public bool isPayed { get; set; } = false;
    public bool isUpgradeMode { get; set; } = false;
    public int apartmentDays { get; set; } = 0;

    private void Awake()
    {
        
        roomKey = 1;//��ʱ�ģ�����Ҫ�ĳɸ������ѡ������
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
        if (this.transform.parent.name == "Row1")
        {
            this.isUnlock = true;
            this.roomKey = 1;
            //ApartmentController.Instance.unLockedApartmentCount += 1;
        }
        if (this.isUnlock)
        {
            Debug.Log("������һ��");
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
                    foreach (var selection in upgradeSelections)
                    {
                        selection.GetComponent<ApartmentUpgrade>().roomKeyIsSend = false;
                        selection.SetActive(false);                       
                    }
                    isUpgradeMode = false;
                    Debug.Log(string.Format("{0}������Ϊ��{1}", this.roomName, RoomData.GetItem(roomKey).name));
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (ApartmentController.Instance.isBuildMode==true && this.isUnlock == false)
        {
            this.isUnlock = true;
            this.roomKey = 1;
            //ApartmentController.Instance.unLockedApartmentCount += 1;
            ApartmentController.Instance.apartment.Add(this.gameObject);
            Debug.Log("�ѽ���"+this.roomName);
            //ApartmentController.Instance.apartment.Add(this.gameObject);
        }
        else if (ApartmentController.Instance.isBuildMode && this.isUnlock)
        {
            if (GameObject.FindGameObjectWithTag("UpgradeButton") != null)
            {
                GameObject.FindGameObjectWithTag("UpgradeButton").SetActive(false);
            }
            upgradeSelection.SetActive(true);
            this.isUpgradeMode = true;
            Debug.Log("������������");
        }

    }

    

    public void PayRent(GuestInApartment guestInApartment,Apartment apartment)
    {
        guestInApartment.GuestEffect();
        if (guestInApartment.guestBudget + guestInApartment.guestExtraBudget >= apartment.roomRent + apartment.roomExtraRent)
        {
            ApartmentController.Instance.vaultMoney += this.roomRent + roomExtraRent;
        }

    }


}
