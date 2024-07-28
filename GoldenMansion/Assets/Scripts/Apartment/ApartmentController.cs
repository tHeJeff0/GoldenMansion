using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ApartmentController : MonoBehaviour
{

    //public int unLockedApartmentCount { get; set; } = 0;
    public List<GameObject> apartment = new List<GameObject>();
    public float vaultMoney { get; set; } = 0;
    public bool isBuildMode { get; set; }
    public int apartmentUpgradeKey { get; set; }

    public int coinGeneratedCount;
    public int coinMovedCount;
    public int guestCount;
    public bool startPlayAnim;
    private static ApartmentController instance;

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

    public void GuestPayRent()
    {
        List<Apartment> apartmentWithGuest = new List<Apartment>();
        List<Apartment> apartmentNoGuest = new List<Apartment>();
        for (int i = 0; i < apartment.Count; i++)
        {
            GuestInApartment guestInApartment = apartment[i].GetComponentInChildren<GuestInApartment>();
            Apartment thisApartment = apartment[i].GetComponent<Apartment>();
            if (guestInApartment != null)
            {
                apartmentWithGuest.Add(thisApartment);
                guestCount += 1;
            }
            else
            {
                apartmentNoGuest.Add(thisApartment);
            }
        }

        foreach (var apartment in apartmentWithGuest)
        {
            GuestInApartment guestInApartment = apartment.GetComponentInChildren<GuestInApartment>();
            PayRent(guestInApartment, apartment);
            StartCoroutine(apartment.PlayGenerateCoinAnim());
            StartCoroutine(apartment.PlayMoveCoinAnim());
            apartment.apartmentDays += 1;
        }

        foreach (var apartment in apartmentNoGuest)
        {
            apartment.apartmentDays += 1;
        }
        //for (int i = 0; i < apartment.Count; i++)
        //{
        //    GuestInApartment guestInApartment = apartment[i].GetComponentInChildren<GuestInApartment>();
        //    Apartment thisApartment = apartment[i].GetComponent<Apartment>();
        //    if (guestInApartment != null)
        //    {
        //        PayRent(guestInApartment, thisApartment);
        //        thisApartment.apartmentDays += 1;
        //    }
        //    else
        //    {
        //        thisApartment.apartmentDays += 1;
        //    }

        //} 
       
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
            apartment.roomRent += 1;
        }
        Debug.Log("煤老板入住，增加房租");
    }


    public void PayRent(GuestInApartment guestInApartment, Apartment apartment)
    {
        guestInApartment.GuestEffect();
        apartment.ApartmentEffect();
        if (guestInApartment.guestBudget + guestInApartment.guestExtraBudget >= apartment.roomRent + apartment.roomExtraRent)
        {
            vaultMoney += apartment.roomRent + apartment.roomExtraRent;
            guestInApartment.guestExtraBudget = 0;
            Debug.Log(string.Format("{0}入住{1},上交房租{2},效果ID是{3}", guestInApartment.guestName, apartment.roomName, apartment.roomRent + apartment.roomExtraRent, guestInApartment.guestEffectID));

        }
        else
        {
            guestCount -= 1;
            Debug.Log(string.Format("{0}入住{1},但没交房租", guestInApartment.guestName, apartment.roomName));
            Debug.Log(string.Format("{0}离开了", guestInApartment.guestName));
            guestInApartment.transform.SetParent(null);           
            GuestController.Instance.GuestInApartmentPrefabStorage.Remove(guestInApartment.gameObject);
            Destroy(guestInApartment.gameObject);
        }
    }

    public void PlusGameDays()
    {
        GameManager.Instance.gameDays += 1;
    }

    
}
