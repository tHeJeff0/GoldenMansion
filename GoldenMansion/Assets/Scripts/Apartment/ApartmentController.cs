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

    public void GenerateBasicCoin()
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
            if (guestInApartment.field == 12)
            {
                SkillController.Instance.SkillMethod_Tour?.Invoke(SkillController.Instance.GetFieldCount(guestInApartment.field), guestInApartment);
            }
            if (guestInApartment.field != 13)
            {
                StartCoroutine(apartment.GenerateBasicCoin());
                
            }
            apartment.apartmentDays += 1;
        }

        foreach (var apartment in apartmentNoGuest)
        {
            apartment.apartmentDays += 1;
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
                //guestCount += 1;
            }
            else
            {
                apartmentNoGuest.Add(thisApartment);
            }

        }

        foreach (var apartment in apartmentWithGuest)
        {
            GuestInApartment guestInApartment = apartment.GetComponentInChildren<GuestInApartment>();
            if (guestInApartment.field == 12)
            {
                SkillController.Instance.SkillMethod_Tour?.Invoke(SkillController.Instance.GetFieldCount(guestInApartment.field), guestInApartment);
            }
            if (guestInApartment.field != 13)
            {
                PayRent(guestInApartment, apartment);
                //StartCoroutine(apartment.PlayGenerateCoinAnim());
                StartCoroutine(apartment.PlayMoveCoinAnim());
            }
            else
            {
                guestInApartment.saveAdjancentPrice();
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


    public void PayRent(GuestInApartment guestInApartment, Apartment apartment)
    {

        vaultMoney += guestInApartment.guestBudget + guestInApartment.guestExtraBudget;
    }


    
}
