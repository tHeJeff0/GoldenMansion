using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class SaveSystem : MonoBehaviour
{
    private static SaveSystem instance;
    public string saveName = "save.sav";

    public static SaveSystem Instance
    {
        get
        {
            if (instance != null)
            {
                instance = FindObjectOfType<SaveSystem>();
                if (instance == null)
                {
                    GameObject saveSystem = new GameObject("SaveSystem");
                    instance = saveSystem.AddComponent<SaveSystem>();
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

    
    public void SaveData(string saveFileName)
    {
        SaveData saveData = savingData();
        var data = JsonUtility.ToJson(saveData);
        var path = Path.Combine(Application.persistentDataPath, saveFileName);
        Debug.Log(path);
        File.WriteAllText(path, data);
        Debug.Log("±£´æ³É¹¦");
        Debug.Log(saveData.gameDays);
    }

    public T LoadData<T>(string saveFileName)
    {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);
        string data = File.ReadAllText(path);
        var loadData = JsonUtility.FromJson<T>(data);
        return loadData;
    }

    public void DeleteData(string saveFileName)
    {
        var path = Path.Combine(Application.persistentDataPath, saveFileName);
        File.Delete(path);
    }

    SaveData savingData()
    {
        SaveData saveData = new SaveData();
        saveData.gameDays = GameManager.Instance.gameDays;
        saveData.isChooseCardFinish = GameManager.Instance.isChooseCardFinish;
        saveData.isRoundEnd = GameManager.Instance.isRoundEnd;
        saveData.vaultMoney = ApartmentController.Instance.vaultMoney;
        saveData.levelKey = GameManager.Instance.levelKey;
        saveData.basicRerollTime = GameManager.Instance.basicRerollTime;
        saveData.extraRerollTime = GameManager.Instance.extraRerollTime;
        saveData.guestRemoveCount = GameManager.Instance.guestRemoveCount;
        saveData.isAllowSell = GameManager.Instance.isAllowSell;
        saveData.isAllowBuy = GameManager.Instance.isAllowBuy;
        saveData.mediaDays = GameManager.Instance.mediaDays;
        foreach (var guest in GuestController.Instance.GuestInApartmentPrefabStorage)
        {
            string guestID = guest.GetComponent<GuestInApartment>().guestElementID;
            int guestKey = guest.GetComponent<GuestInApartment>().key;
            saveData.guestID.Add(guestID);
            saveData.guestKey.Add(guestKey);
        }
        //saveData.guestStorage = StorageController.Instance.guestStorage;
        return saveData;
    }

    public void readData(SaveData saveData)
    {
        for (int i = 0; i < 3; i++)
        {
            Destroy(GameObject.Find("GuestInApartment(Clone)"));
        }       
        GameManager.Instance.gameDays = saveData.gameDays;
        GameManager.Instance.isChooseCardFinish = saveData.isChooseCardFinish;
        GameManager.Instance.isRoundEnd = saveData.isRoundEnd;
        ApartmentController.Instance.vaultMoney = saveData.vaultMoney;
        GameManager.Instance.levelKey = saveData.levelKey;
        GameManager.Instance.basicRerollTime = saveData.basicRerollTime;
        GameManager.Instance.extraRerollTime = saveData.extraRerollTime;
        GameManager.Instance.guestRemoveCount = saveData.guestRemoveCount;
        GameManager.Instance.isAllowSell = saveData.isAllowSell;
        GameManager.Instance.isAllowBuy = saveData.isAllowBuy;
        GameManager.Instance.mediaDays = saveData.mediaDays;
        LoadGuestData(saveData);
        //StorageController.Instance.guestStorage = saveData.guestStorage;
    }

    

    public void LoadGuestData(SaveData saveData)
    {
        Dictionary<string, int> guestKey = new Dictionary<string, int>();
        for (int i = 0; i < saveData.guestID.Count; i++)
        {
            guestKey.Add(saveData.guestID[i], saveData.guestKey[i]);
        }
        foreach (var guestID in saveData.guestID)
        {
            GameObject guest = Instantiate(GuestController.Instance.guestInApartmentPrefab.gameObject);
            guest.GetComponent<GuestInApartment>().guestElementID = guestID;
            guest.GetComponent<GuestInApartment>().key = guestKey[guestID];
            guest.GetComponentInChildren<SpriteRenderer>().enabled = false;
            GuestController.Instance.GuestInApartmentPrefabStorage.Add(guest);
        }
    }

}
