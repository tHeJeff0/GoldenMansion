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

    // Update is called once per frame
    void Update()
    {
        if (StorageController.Instance.filterSelectedCount == 0)
        {
            StorageController.Instance.isFilterMode = false;
        }
        else
        {
            StorageController.Instance.isFilterMode = true;
        }

        if (!StorageController.Instance.isFilterMode)
        {
            if (GuestController.Instance.GuestInApartmentPrefabStorage.Count != guestSlot.transform.childCount)
            {
                Debug.Log("触发了刷新仓库");
                int childNum = guestSlot.transform.childCount;
                for (int i = 0; i < childNum; i++)
                {
                    Destroy(guestSlot.transform.GetChild(i).gameObject);
                }
                StorageController.Instance.guestStorage.Clear();
                StorageController.Instance.AddStorage();
                UpdateStorage(StorageController.Instance.guestStorage);
            }
        }
        else
        {
            if (StorageController.Instance.filterWaitingUpdate)
            {
                Debug.Log("筛选状态下触发了刷新仓库");
                int childNum = guestSlot.transform.childCount;
                for (int i = 0; i < childNum; i++)
                {
                    Destroy(guestSlot.transform.GetChild(i).gameObject);
                }
                UpdateStorage(StorageController.Instance.guestFilteredStorage);
                StorageController.Instance.filterWaitingUpdate = false;
            }
        }
        

        
    }

    public void OnActive()
    {
        StorageController.Instance.AddStorage();
        UpdateStorage(StorageController.Instance.guestStorage);

    }

    public void UpdateStorage(List<GameObject> storageShown)
    {
        
        foreach (var guest in storageShown)
        {
            if (guest != null && guest.GetComponent<GuestInApartment>()!=null)
            {
                GameObject guestInStoragePrefab = Instantiate(guestPrefab, guestSlot.transform);
                guestInStoragePrefab.GetComponent<GuestInfo>().key = guest.GetComponent<GuestInApartment>().key;
                guestInStoragePrefab.GetComponent<GuestInfo>().basicPrice = guest.GetComponent<GuestInApartment>().guestBasicPrice;
                guestInStoragePrefab.GetComponent<GuestInfo>().extraPrice = guest.GetComponent<GuestInApartment>().guestExtraPrice;
                guestInStoragePrefab.GetComponent<GuestInfo>().mbtiID = guest.GetComponent<GuestInApartment>().mbti;
                guestInStoragePrefab.GetComponent<GuestInfo>().basicBudget = guest.GetComponent<GuestInApartment>().guestBudget;
                guestInStoragePrefab.GetComponent<GuestInfo>().extraBudget = guest.GetComponent<GuestInApartment>().guestExtraBudget;
                guestInStoragePrefab.GetComponent<GuestInfo>().elementID = guest.GetComponent<GuestInApartment>().guestElementID;
                guestInStoragePrefab.GetComponent<GuestInfo>().personaID = guest.GetComponent<GuestInApartment>().persona;
            }
            
        }
    }

    public void CloseThisPanel()
    {
        StorageController.Instance.guestStorage.Clear();
        StorageController.Instance.guestFilteredStorage.Clear();
        StorageController.Instance.filterSelectedCount = 0;
        StorageController.Instance.isFilterMode = false;
        gameObject.SetActive(false);
    }

    public void SellGuest()
    {
        StartCoroutine(SellGuestCoroutine());
        

    }

    IEnumerator SellGuestCoroutine()
    {
        List<GameObject> temporList = new List<GameObject>();
        temporList.AddRange(StorageController.Instance.guestSelected);
        for (int i = temporList.Count-1; i >= 0; i--)
        {
            temporList[i].GetComponent<GuestInfo>().OnSell();                     
            
            Debug.Log(i);
        }
        StorageController.Instance.filterWaitingUpdate = true;
        temporList.Clear();
        StorageController.Instance.guestSelected.Clear();
        yield return true;
    }

   
}
