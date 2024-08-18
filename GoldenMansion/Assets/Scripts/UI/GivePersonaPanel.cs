using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivePersonaPanel : MonoBehaviour
{
    public GameObject panelPrefab;
    public GameObject guestPrefab;
    public int personaKey;


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
            GameObject guestInStoragePrefab = Instantiate(guestPrefab, this.transform.Find("GuestSlot").transform);
            guestInStoragePrefab.GetComponent<GuestWhenGivePersona>().key = guest.GetComponent<GuestInApartment>().key;
            guestInStoragePrefab.GetComponent<GuestWhenGivePersona>().basicPrice = guest.GetComponent<GuestInApartment>().guestBasicPrice;
            guestInStoragePrefab.GetComponent<GuestWhenGivePersona>().extraPrice = guest.GetComponent<GuestInApartment>().guestExtraPrice;
            guestInStoragePrefab.GetComponent<GuestWhenGivePersona>().mbtiID = guest.GetComponent<GuestInApartment>().mbti;
            guestInStoragePrefab.GetComponent<GuestWhenGivePersona>().personaID = guest.GetComponent<GuestInApartment>().persona;
            guestInStoragePrefab.GetComponent<GuestWhenGivePersona>().elementCount = GuestController.Instance.GuestInApartmentPrefabStorage.IndexOf(guest);
        }
    }

    public void CloseThisPanel()
    {
        panelPrefab.transform.gameObject.SetActive(false);
        StorageController.Instance.guestStorage.Clear();
    }
}
