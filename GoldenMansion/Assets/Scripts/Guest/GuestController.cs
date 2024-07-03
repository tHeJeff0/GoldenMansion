using ExcelData;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class GuestController : MonoBehaviour
{
    
    private static GuestController instance;
    
    public List<int> GuestStorage = new List<int>();
    public Guest guestDetailPrefab;
    public Guest guestInApartmentPrefab;
    private int basicGuestCount { get; set; } = 3;
    public static GuestController Instance
    {
        get
        {
            if (instance != null)
            {
                instance = FindObjectOfType<GuestController>();
                if (instance == null)
                {
                    GameObject guestController = new GameObject("GuestController");
                    instance = guestController.AddComponent<GuestController>();
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

        GenerateBasicGuest(basicGuestCount);
    }

    public int RandomKey()
    {
        int randomKey = 0;
        int keyMin = 1;
        int keyMax = 1;
        var characterDataDict = CharacterData.GetDict();
        foreach (var kvp in characterDataDict)
        {
            int key = kvp.Key;
            if (key > keyMax)
            {
                keyMax = key;
            }
            if (key < keyMin)
            {
                keyMin = key;
            }
        }
        randomKey = Random.Range(keyMin, keyMax);
        return randomKey;
    }

    void GenerateBasicGuest(int generateCount)
    {
        for (int i = 0; i < generateCount; i++)
        {
            GuestStorage.Add(RandomKey());
        }
    }
}
