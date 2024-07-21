using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApartmentController : MonoBehaviour
{

    //public int unLockedApartmentCount { get; set; } = 0;
    public List<GameObject> apartment = new List<GameObject>();
    public float vaultMoney { get; set; } = 0;
    public bool isBuildMode { get; set; }
    public int apartmentUpgradeKey { get; set; }
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


    public void ApartmentEffect_IgnoreBudget(GuestInApartment guestInApartment, Apartment apartment)
    {
        if (guestInApartment.key == 2)
        {
            apartment.roomExtraRent = guestInApartment.guestBudget + guestInApartment.guestExtraBudget - apartment.roomRent;
        }
        Debug.Log("无视预算");
    }

    public void ApartmentEffect_LiveTwoGuest(Apartment apartment)
    {
        float dice = Random.Range(0, 1);
        if (dice > 0.5f)
        {
            apartment.roomGuestExtraLimit += 1;
        }
        Debug.Log("可住2人");
    }

    public void ApartmentEffect_IncreaseRent(GuestInApartment guestInApartment, Apartment apartment)
    {
        if (guestInApartment.key == 8)
        {
            apartment.roomRent += 100;
        }
        Debug.Log("煤老板入住，增加房租");
    }

}
