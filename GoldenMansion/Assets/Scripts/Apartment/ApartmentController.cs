using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApartmentController : MonoBehaviour
{

    public int unLockedApartmentCount { get; set; } = 0;
    public List<GameObject> apartment = new List<GameObject>();
    public float vaultMoney { get; set; } = 0;
    public bool isBuildMode { get; set; }
    private static ApartmentController instance;
    private int isPayedRoomCount = 0;

    public static ApartmentController Instance
    {
        get
        {
            if (instance != null)
            {
                instance = FindObjectOfType<ApartmentController>();
                if (instance == null)
                {
                    GameObject apartmentController = new GameObject("ApartmentController");
                    instance = apartmentController.AddComponent<ApartmentController>();
                }
            }
            return instance;
        }
    }

    public void CheckPayedCount()
    {
        for (int i = 0; i < apartment.Count; i++)
        {
            if (apartment[i].GetComponent<Apartment>().isPayed)
            {
                isPayedRoomCount += 1;
            }
        }
        if (isPayedRoomCount == apartment.Count)
        {
            for (int i = 0; i < apartment.Count; i++)
            {
                apartment[i].GetComponent<Apartment>().isPayed = false;
            }
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



}
