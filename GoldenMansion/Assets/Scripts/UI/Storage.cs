using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour
{

    public GameObject storagePrefab;
    public GameObject guestPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetThisActive()
    {       
        StorageController.Instance.AddStorage();
        Instantiate(this);
        UpdateStorage();

    }

    public void UpdateStorage()
    {
        foreach (var guest in StorageController.Instance.guestStorage)
        {
            GameObject guestInStoragePrefab = Instantiate(guestPrefab);
            guestInStoragePrefab.GetComponent<GuestInStorage>().key = guest.GetComponent<GuestInApartment>().key;
            guestInStoragePrefab.GetComponent<GuestInStorage>().basicPrice = guest.GetComponent<GuestInApartment>().guestBasicPrice;
            guestInStoragePrefab.GetComponent<GuestInStorage>().extraPrice = guest.GetComponent<GuestInApartment>().guestExtraPrice;
            guestInStoragePrefab.GetComponent<GuestInStorage>().mbtiID = guest.GetComponent<GuestInApartment>().mbti;
            guestInStoragePrefab.GetComponent<GuestInStorage>().personaID = guest.GetComponent<GuestInApartment>().persona;

        }
    }

   
}
