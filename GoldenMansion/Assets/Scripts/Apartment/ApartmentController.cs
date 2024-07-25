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
        for (int i = 0; i < apartment.Count; i++)
        {
            GuestInApartment guestInApartment = apartment[i].GetComponentInChildren<GuestInApartment>();
            Apartment thisApartment = apartment[i].GetComponent<Apartment>();
            try
            {
                PayRent(guestInApartment, thisApartment);
                thisApartment.apartmentDays += 1;
            }
            catch
            {
                thisApartment.apartmentDays += 1;
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
        Debug.Log("����Ԥ��");
    }

    public void ApartmentEffect_LiveTwoGuest(Apartment apartment)
    {
        float dice = Random.Range(0, 1);
        if (dice > 0.5f)
        {
            apartment.roomGuestExtraLimit += 1;
        }
        Debug.Log("��ס2��");
    }

    public void ApartmentEffect_IncreaseRent(GuestInApartment guestInApartment, Apartment apartment)
    {
        if (guestInApartment.key == 8)
        {
            apartment.roomRent += 1;
        }
        Debug.Log("ú�ϰ���ס�����ӷ���");
    }


    public void PayRent(GuestInApartment guestInApartment, Apartment apartment)
    {
        guestInApartment.GuestEffect();
        apartment.ApartmentEffect();
        if (guestInApartment.guestBudget + guestInApartment.guestExtraBudget >= apartment.roomRent + apartment.roomExtraRent)
        {
            
            vaultMoney += apartment.roomRent + apartment.roomExtraRent;
            guestInApartment.guestExtraBudget = 0;
            apartment.roomExtraRent = 0;
            Debug.Log(string.Format("{0}��ס{1},�Ͻ�����{2},Ч��ID��{3}", GetComponentInChildren<GuestInApartment>().guestName, apartment.roomName, apartment.roomRent + apartment.roomExtraRent, GetComponentInChildren<GuestInApartment>().guestEffectID));

        }
        else
        {
            Debug.Log(string.Format("{0}��ס{1},��û������", GetComponentInChildren<GuestInApartment>().guestName, apartment.roomName));
            Debug.Log(string.Format("{0}�뿪��", GetComponentInChildren<GuestInApartment>().guestName));
            GuestController.Instance.GuestInApartmentPrefabStorage.Remove(guestInApartment.gameObject);
            Destroy(guestInApartment.gameObject);
        }
    }

}
