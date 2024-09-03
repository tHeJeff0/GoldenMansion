using ExcelData;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class GuestController : MonoBehaviour
{

    private static GuestController instance;

    public int temporKey;
    public List<GameObject> GuestInApartmentPrefabStorage = new List<GameObject>();
    public List<GameObject> AnimToPlayStorage = new List<GameObject>();
    public Guest guestDetailPrefab;
    public GuestInApartment guestInApartmentPrefab;

    public int[] internID = { 8, 9, 23, 27, 31, 35, 41, 48, 51, 54, 58, 61 };
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
        randomKey = Random.Range(keyMin, keyMax + 1);
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


    
    public List<GameObject> GetAdjancentGuest(GuestInApartment guestInApartment)
    {
        List<GameObject> adjancentGuest = new List<GameObject>();
        Vector3 boxSize = guestInApartment.transform.parent.GetComponent<BoxCollider>().size;
        Collider[] colliders = Physics.OverlapBox(guestInApartment.transform.position, boxSize, Quaternion.identity);
        foreach (var collider in colliders)
        {
            if (collider!=guestInApartment.GetComponent<BoxCollider>() && collider.CompareTag("Guest"))
            {
                adjancentGuest.Add(collider.gameObject);
            }

        }
        return adjancentGuest;
    }

    public void GuestSkillTrigger_WhenMoveIn()
    {
        List<GameObject> temporList = new List<GameObject>();
        temporList.AddRange(GuestInApartmentPrefabStorage);
        foreach (var guest in temporList)
        {
            guest.GetComponent<GuestInApartment>().SkillTrigger_WhenMoveIn();
        }
    }

    public void AnimTrigger()
    {
        for (int i = 0; i < AnimToPlayStorage.Count; i++)
        {
            //调用动画协程
        }
    }

}
