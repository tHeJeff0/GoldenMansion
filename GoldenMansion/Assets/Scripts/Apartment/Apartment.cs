using DG.Tweening;
using ExcelData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using static UnityEngine.ParticleSystem;
using UnityEngine.UI;

public class Apartment : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] GameObject lockedApartment;
    [SerializeField] GameObject unLockedApartment;
    [SerializeField] GameObject upgradeSelection;
    [SerializeField] GameObject[] upgradeSelections;

    public GameObject coin;


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
        }

    }

    public IEnumerator PlayGenerateCoinAnim()
    {
        if (this.GetComponentInChildren<GuestInApartment>() != null)
        {
            GuestInApartment guestInApartment = this.GetComponentInChildren<GuestInApartment>();
            this.coin.SetActive(true);
            this.coin.GetComponentInChildren<TextMeshPro>().text = (guestInApartment.guestBudget + guestInApartment.guestExtraBudget).ToString();
            this.coin.transform.localPosition = new Vector3(0, 0, 0);
            yield return this.coin.transform.DOLocalMoveY(1, 0.5f).WaitForCompletion();
            ApartmentController.Instance.coinGeneratedCount += 1;
        }
        
    }

    public IEnumerator PlayMoveCoinAnim()
    {
        if (this.GetComponentInChildren<GuestInApartment>() != null)
        {
            yield return new WaitUntil(() => ApartmentController.Instance.guestCount == ApartmentController.Instance.coinGeneratedCount);
            yield return new WaitForSecondsRealtime(2.0f);
            yield return this.coin.transform.DOMove(new Vector3(-10, 8, 0), 0.5f).WaitForCompletion();           
            ApartmentController.Instance.coinMovedCount += 1;
            this.coin.SetActive(false);
            roomExtraRent = 0;
        }
        
    }

    


}
