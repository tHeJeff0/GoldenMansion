using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageController : MonoBehaviour
{
    private static StorageController instance;

    public List<GameObject> guestStorage = new List<GameObject>();
    public List<GameObject> guestSelected = new List<GameObject>();

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
