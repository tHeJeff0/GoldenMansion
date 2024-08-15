using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageController : MonoBehaviour
{
    private static StorageController instance;

    public List<GameObject> guestStorage = new List<GameObject>();
    public GameObject storagePrefab;
    public GameObject guestPrefab;

    public static StorageController Instance
    {
        get
        {
            if (instance != null)
            {
                instance = FindObjectOfType<StorageController>();
                if (instance == null)
                {
                    GameObject storageController = new GameObject("StorageController");
                    instance = storageController.AddComponent<StorageController>();
                }
            }
            return instance;
        }
        
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }
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
        Instantiate(this);
        AddStorage();
        UpdateStorage();
    }

    public void UpdateStorage()
    {
        foreach (var guest in guestStorage)
        {
            GameObject guestInStoragePrefab = Instantiate(guestPrefab, storagePrefab.transform);
            guestInStoragePrefab.GetComponent<GuestInStorage>().key = guest.GetComponent<GuestInApartment>().key;
            guestInStoragePrefab.GetComponent<GuestInStorage>().basicPrice = guest.GetComponent<GuestInApartment>().guestBasicPrice;
            guestInStoragePrefab.GetComponent<GuestInStorage>().extraPrice = guest.GetComponent<GuestInApartment>().guestExtraPrice;
            guestInStoragePrefab.GetComponent<GuestInStorage>().mbtiID = guest.GetComponent<GuestInApartment>().mbti;
            guestInStoragePrefab.GetComponent<GuestInApartment>().persona = guest.GetComponent<GuestInApartment>().persona;
            
        }
    }

    public void AddStorage()
    {
        foreach (var guest in GuestController.Instance.GuestInApartmentPrefabStorage)
        {
            guestStorage.Add(guest);
        }
    }

    public void RemoveStorage(GameObject guestCard)
    {
        guestStorage.Remove(guestCard);
    }
}
