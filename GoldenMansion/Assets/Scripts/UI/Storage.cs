using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Storage : MonoBehaviour
{

    public GameObject storagePrefab;
    public GameObject guestPrefab;
    public GameObject guestSlot;

    private void Awake()
    {
        OnActive();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GuestController.Instance.GuestInApartmentPrefabStorage.Count != StorageController.Instance.guestStorage.Count)
        {
            Debug.Log("¥•∑¢¡À");
            int childNum = guestSlot.transform.childCount;
            for (int i = 0; i < childNum; i++)
            {
                Destroy(guestSlot.transform.GetChild(i).gameObject);
            }
            StorageController.Instance.guestStorage.Clear();
            StorageController.Instance.AddStorage();
            UpdateStorage();
        }
    }

    public void OnActive()
    {
        StorageController.Instance.AddStorage();
        UpdateStorage();

    }

    public void UpdateStorage()
    {
        
        foreach (var guest in StorageController.Instance.guestStorage)
        {
            GameObject guestInStoragePrefab = Instantiate(guestPrefab,guestSlot.transform);
            guestInStoragePrefab.GetComponent<GuestInfo>().key = guest.GetComponent<GuestInApartment>().key;
            guestInStoragePrefab.GetComponent<GuestInfo>().basicPrice = guest.GetComponent<GuestInApartment>().guestBasicPrice;
            guestInStoragePrefab.GetComponent<GuestInfo>().extraPrice = guest.GetComponent<GuestInApartment>().guestExtraPrice;
            guestInStoragePrefab.GetComponent<GuestInfo>().mbtiID = guest.GetComponent<GuestInApartment>().mbti;
            guestInStoragePrefab.GetComponent<GuestInfo>().basicBudget = guest.GetComponent<GuestInApartment>().guestBudget;
            guestInStoragePrefab.GetComponent<GuestInfo>().extraBudget = guest.GetComponent<GuestInApartment>().guestExtraBudget;
            //guestInStoragePrefab.GetComponent<GuestInfo>().personaID = guest.GetComponent<GuestInApartment>().persona;
            guestInStoragePrefab.GetComponent<GuestInfo>().elementID = guest.GetComponent<GuestInApartment>().guestElementID;
        }
    }

    public void CloseThisPanel()
    {
        gameObject.SetActive(false);
        StorageController.Instance.guestStorage.Clear();
    }

    public void SellGuest()
    {
        
        //StorageController.Instance.temporGuestList.AddRange(GuestController.Instance.GuestInApartmentPrefabStorage);
        List<GameObject> temporList = new List<GameObject>();
        temporList.AddRange(StorageController.Instance.guestSelected);
        for (int i = temporList.Count - 1; i >= 0 ; i--)
        {
            temporList[i].GetComponent<GuestInfo>().OnSell();
            //StorageController.Instance.RemoveStorage(temporList[i]);
        }
        temporList.Clear();
        StorageController.Instance.guestSelected.Clear();
        //StorageController.Instance.temporGuestList.Clear();
        //foreach (var guest in temporList)
        //{
        //    Debug.Log(guest.GetComponent<GuestInfo>().elementCount);
        //    guest.GetComponent<GuestInfo>().OnSell();
        //    StorageController.Instance.RemoveStorage(guest);
        //    foreach (var guestremaining in temporList)
        //    {
        //        guestremaining.GetComponent<GuestInfo>().elementCount -= 1;
        //    }
        //    //UpdateStorage();
        //    //if (temporList.IndexOf(guest) > 1)
        //    //{
        //    //    guest.GetComponent<GuestInfo>().elementCount -= (temporList.Count-1);
        //    //    guest.GetComponent<GuestInfo>().OnSell();
        //    //    StorageController.Instance.RemoveStorage(guest);
        //    //}
        //    //else if(temporList.IndexOf(guest) == 1)
        //    //{
        //    //    guest.GetComponent<GuestInfo>().elementCount -= 1;
        //    //    guest.GetComponent<GuestInfo>().OnSell();
        //    //    StorageController.Instance.RemoveStorage(guest);
        //    //}
        //    //else
        //    //{
        //    //    guest.GetComponent<GuestInfo>().OnSell();
        //    //    StorageController.Instance.RemoveStorage(guest);
        //    //}

        //}
        //temporList.Clear();
        //StorageController.Instance.temporGuestList.Clear();
        //UpdateStorage();
    }
   
}
