using ExcelData;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GuestController : MonoBehaviour
{

    private static GuestController instance;

    public int temporKey;
    public List<GameObject> GuestInApartmentPrefabStorage = new List<GameObject>();
    public Guest guestDetailPrefab;
    public GuestInApartment guestInApartmentPrefab;
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

    private void Update()
    {
        
    }

    public int RandomKey()
    {
        int randomKey = 1;
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
            temporKey = RandomKey();
            GameObject starterGuest = Instantiate(guestInApartmentPrefab.gameObject);
            starterGuest.GetComponentInChildren<SpriteRenderer>().enabled = false;
            GuestInApartmentPrefabStorage.Add(starterGuest);
        }
    }

    public void GuestEffect_ChangeJob(GuestInApartment guestInApartment)
    {
        if (guestInApartment.guestDays == 3)
        {
            while (guestInApartment.key == 1)
            {
                guestInApartment.key = RandomKey();
            }
            
        }
        Debug.Log("改变了职业");
    }

    public void GuestEffect_IgnoreRoomRentLimit(GuestInApartment guestInApartment)
    {
        Apartment apartment = guestInApartment.transform.parent.GetComponent<Apartment>();
        apartment.roomExtraRent = guestInApartment.guestBudget + guestInApartment.guestExtraBudget - apartment.roomRent;
        Debug.Log("无视房租");
    }

    public void GuestEffect_PayByNeighbour(GuestInApartment guestInApartment)
    {
        List<GameObject> adjancentGuest = new List<GameObject>();
        adjancentGuest.AddRange(GetAdjancentGuest(guestInApartment));

        for (int i = 0; i < adjancentGuest.Count; i++)
        {
            Debug.Log(i);
            if (adjancentGuest[i].CompareTag("Guest") && adjancentGuest[i].transform!=guestInApartment.transform)
            {
                
                guestInApartment.guestExtraBudget += adjancentGuest[i].GetComponent<GuestInApartment>().guestBudget;

            }
            else
            {
                Debug.Log("没有邻居");
            }
        }
    }

    public void GuestEffect_RateMoveAway(GuestInApartment guestInApartment)
    {
        float leaveRate = Random.Range(0, 1);
        if (leaveRate > 0.8)
        {
            GuestInApartmentPrefabStorage.Remove(guestInApartment.gameObject);
            Destroy(guestInApartment.gameObject);
        }
        Debug.Log("离开");
    }

    public void GuestEffect_RentIncrease(GuestInApartment guestInApartment)
    {
        Apartment apartment = guestInApartment.transform.parent.GetComponent<Apartment>();
        apartment.roomExtraRent = guestInApartment.guestBudget + guestInApartment.guestExtraBudget - apartment.roomRent;
        apartment.roomRent += 1;
        Debug.Log("房租增加");
    }

    public void GuestEffect_RandomBudget()
    {
        Debug.Log("预算随机");
    }

    public void GuestEffect_MoveInNextBy()
    {
        Debug.Log("必须住入");
    }

    public void GuestEffect_RentIncreaseByNeighbour()
    {
        Debug.Log("根据邻居增加房租");
    }

    public void GuestEffect_GenerateGuest()
    {
        float generateRate = Random.Range(0, 1);
        if (generateRate >= 0.5)
        {           
            GuestInApartmentPrefabStorage.Add(Instantiate(guestInApartmentPrefab.gameObject));
        }
        Debug.Log("拉人入伙");
    }

    public List<GameObject> GetAdjancentGuest(GuestInApartment guestInApartment)
    {
        List<GameObject> adjancentGuest = new List<GameObject>();
        Vector3 boxSize = guestInApartment.transform.parent.GetComponent<BoxCollider>().size;
        Debug.Log(boxSize);
        Debug.Log("位置是"+guestInApartment.transform.position+"大小是:"+guestInApartment.transform.lossyScale);
        Collider[] colliders = Physics.OverlapBox(guestInApartment.transform.position, boxSize, Quaternion.identity);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Guest"))
            {
                adjancentGuest.Add(collider.gameObject);
            }

        }
        return adjancentGuest;
    }

}
