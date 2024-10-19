using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class NewGivePersona : MonoBehaviour
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
            if (GuestController.Instance.GuestInApartmentPrefabStorage.Count != StorageController.Instance.guestStorage.Count)
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
                guestInStoragePrefab.GetComponent<GuestInfoWhenGivePersona>().key = guest.GetComponent<GuestInApartment>().key;
                guestInStoragePrefab.GetComponent<GuestInfoWhenGivePersona>().basicPrice = guest.GetComponent<GuestInApartment>().guestBasicPrice;
                guestInStoragePrefab.GetComponent<GuestInfoWhenGivePersona>().extraPrice = guest.GetComponent<GuestInApartment>().guestExtraPrice;
                guestInStoragePrefab.GetComponent<GuestInfoWhenGivePersona>().mbtiID = guest.GetComponent<GuestInApartment>().mbti;
                guestInStoragePrefab.GetComponent<GuestInfoWhenGivePersona>().basicBudget = guest.GetComponent<GuestInApartment>().guestBudget;
                guestInStoragePrefab.GetComponent<GuestInfoWhenGivePersona>().extraBudget = guest.GetComponent<GuestInApartment>().guestExtraBudget;
                guestInStoragePrefab.GetComponent<GuestInfoWhenGivePersona>().elementID = guest.GetComponent<GuestInApartment>().guestElementID;
            }
            
        }
    }

    public void CloseThisPanel()
    {
        gameObject.SetActive(false);
        StorageController.Instance.guestStorage.Clear();
    }
   
}
