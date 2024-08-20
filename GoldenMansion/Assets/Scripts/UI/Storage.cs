using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            guestInStoragePrefab.GetComponent<GuestInStorage>().key = guest.GetComponent<GuestInApartment>().key;
            guestInStoragePrefab.GetComponent<GuestInStorage>().basicPrice = guest.GetComponent<GuestInApartment>().guestBasicPrice;
            guestInStoragePrefab.GetComponent<GuestInStorage>().extraPrice = guest.GetComponent<GuestInApartment>().guestExtraPrice;
            guestInStoragePrefab.GetComponent<GuestInStorage>().mbtiID = guest.GetComponent<GuestInApartment>().mbti;
            guestInStoragePrefab.GetComponent<GuestInStorage>().personaID = guest.GetComponent<GuestInApartment>().persona;
            guestInStoragePrefab.GetComponent<GuestInStorage>().elementCount = GuestController.Instance.GuestInApartmentPrefabStorage.IndexOf(guest);
        }
    }

    public void CloseThisPanel()
    {
        storagePrefab.transform.parent.gameObject.SetActive(false);
        StorageController.Instance.guestStorage.Clear();
    }
   
}
