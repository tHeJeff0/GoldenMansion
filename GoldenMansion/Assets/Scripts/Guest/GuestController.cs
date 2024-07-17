using ExcelData;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
            //Debug.Log(string.Format("{0},{1}",starterGuest.GetComponent<Guest>().guestName, starterGuest.GetComponent<Guest>().transform.parent.name));
        }
    }

    public void GuestEffect_ChangeJob()
    {
        Debug.Log("�ı���ְҵ");
    }

    public void GuestEffect_IgnoreRoomRentLimit()
    {
        Debug.Log("���ӷ���");
    }

    public void GuestEffect_PayByNeighbour()
    {
        Debug.Log("�ھӴ���");
    }

    public void GuestEffect_RateMoveAway()
    {
        Debug.Log("�뿪");
    }

    public void GuestEffect_RentIncrease()
    {
        Debug.Log("��������");
    }

    public void GuestEffect_RandomBudget()
    {
        Debug.Log("Ԥ�����");
    }

    public void GuestEffect_MoveInNextBy()
    {
        Debug.Log("����ס��");
    }

    public void GuestEffect_RentIncreaseByNeighbour()
    {
        Debug.Log("�����ھ����ӷ���");
    }

    public void GuestEffect_GenerateGuest()
    {
        Debug.Log("�������");
    }
}
