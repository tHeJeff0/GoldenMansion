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
            UpdateStorage(StorageController.Instance.guestStorage);
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
            GameObject guestInStoragePrefab = Instantiate(guestPrefab,guestSlot.transform);
            guestInStoragePrefab.GetComponent<GuestInfo>().key = guest.GetComponent<GuestInApartment>().key;
            guestInStoragePrefab.GetComponent<GuestInfo>().basicPrice = guest.GetComponent<GuestInApartment>().guestBasicPrice;
            guestInStoragePrefab.GetComponent<GuestInfo>().extraPrice = guest.GetComponent<GuestInApartment>().guestExtraPrice;
            guestInStoragePrefab.GetComponent<GuestInfo>().mbtiID = guest.GetComponent<GuestInApartment>().mbti;
            guestInStoragePrefab.GetComponent<GuestInfo>().basicBudget = guest.GetComponent<GuestInApartment>().guestBudget;
            guestInStoragePrefab.GetComponent<GuestInfo>().extraBudget = guest.GetComponent<GuestInApartment>().guestExtraBudget;
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
        
        List<GameObject> temporList = new List<GameObject>();
        temporList.AddRange(StorageController.Instance.guestSelected);
        for (int i = temporList.Count - 1; i >= 0 ; i--)
        {
            temporList[i].GetComponent<GuestInfo>().OnSell();
        }
        temporList.Clear();
        StorageController.Instance.guestSelected.Clear();
        
    }
   
    public List<GameObject> GuestFilter(List<int> jobKey)
    {
        List<GameObject> guestsMatchedLabel = new List<GameObject>();
        return guestsMatchedLabel;
    }

    public List<GameObject> GuestFilter(List<int> jobKey,List<int> personaKey)
    {
        List<GameObject> guestsMatchedLabel = new List<GameObject>();
        return guestsMatchedLabel;
    }

    public List<GameObject> GuestFilter(List<int> jobKey, List<int> personaKey,List<bool> isIntern)
    {
        List<GameObject> guestsMatchedLabel = new List<GameObject>();
        return guestsMatchedLabel;
    }
}
