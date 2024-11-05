using ExcelData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


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
        Debug.Log("保存成功");
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
            List<int> guestPersona = guest.GetComponent<GuestInApartment>().persona;
            string guestPersonaString = "";
            if (guestPersona.Count > 0)
            {
                guestPersonaString = string.Join("_", guestPersona);
            }
            else
            {
                guestPersonaString = "";
            }
            saveData.guestID.Add(guestID);
            saveData.guestKey.Add(guestKey);
            saveData.guestPersona.Add(guestPersonaString);
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
        Dictionary<string, string> guestPersona = new Dictionary<string, string>();
        for (int i = 0; i < saveData.guestID.Count; i++)
        {
            guestKey.Add(saveData.guestID[i]+"guestKey", saveData.guestKey[i]);
            if (saveData.guestPersona.Count > 0)
            {
                guestPersona.Add(saveData.guestID[i]+"guestPersona", saveData.guestPersona[i]);
            }
        }
        foreach (var guestID in saveData.guestID)
        {
            GameObject guest = Instantiate(GuestController.Instance.guestInApartmentPrefab.gameObject);
            guest.GetComponent<GuestInApartment>().guestElementID = guestID;
            guest.GetComponent<GuestInApartment>().key = guestKey[guestID+"guestKey"];
            if(guestPersona[guestID + "guestPersona"] != "")
            {
                List<int> persona = guestPersona[guestID + "guestPersona"].Split("_").Select(int.Parse).ToList<int>();
                guest.GetComponent<GuestInApartment>().persona = persona;
            }
            else
            {
                guest.GetComponent<GuestInApartment>().persona = new List<int>();
            }
            
            if (guest.GetComponent<GuestInApartment>().persona.Count > 0)
            {
                foreach (var persona in guest.GetComponent<GuestInApartment>().persona)
                {
                    guest.GetComponent<GuestInApartment>().GetPersonaSkill(persona);
                }
            }
            guest.GetComponent<GuestInApartment>().InitialGuest();
            GuestController.Instance.GuestInApartmentPrefabStorage.Add(guest);
        }
    }

    public void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt("Language", GameManager.Instance.Language);
        PlayerPrefs.Save();
    }

    public void LoadPlayerPrefs()
    {
        GameManager.Instance.Language = PlayerPrefs.GetInt("Language");
        TranslateButtonText();
    }

    public void TranslateButtonText()
    {
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);

            if (scene.isLoaded)
            {
                // 遍历场景中的所有根对象
                foreach (GameObject rootObject in scene.GetRootGameObjects())
                {
                    TextMeshProUGUI[] texts = rootObject.GetComponentsInChildren<TextMeshProUGUI>(true);
                    foreach (var text in texts)
                    {
                        if (text.CompareTag("Button"))
                        {
                            switch (GameManager.Instance.Language)
                            {
                                case 1: text.text = ButtonLanguageData.GetItem(text.text).CHN;
                                    break;
                                case 2: text.text = ButtonLanguageData.GetItem(text.text).ENG;
                                    break;
                                case 3:text.text = ButtonLanguageData.GetItem(text.text).TCHN;
                                    break;
                            }
                        }
                    }
                    // 获取该根对象及其子对象中的所有Button组件
                    Button[] buttons = rootObject.GetComponentsInChildren<Button>(true);

                    foreach (Button button in buttons)
                    {

                        if (button.GetComponentInChildren<TextMeshProUGUI>() != null&& button.CompareTag("Button"))
                        {
                            switch (GameManager.Instance.Language)
                            {
                                case 1:
                                    button.GetComponentInChildren<TextMeshProUGUI>().text = ButtonLanguageData.GetItem(button.GetComponentInChildren<TextMeshProUGUI>().text).CHN;
                                    break;
                                case 2: button.GetComponentInChildren<TextMeshProUGUI>().text = ButtonLanguageData.GetItem(button.GetComponentInChildren<TextMeshProUGUI>().text).ENG;
                                    break;
                                case 3: button.GetComponentInChildren<TextMeshProUGUI>().text = ButtonLanguageData.GetItem(button.GetComponentInChildren<TextMeshProUGUI>().text).TCHN;
                                    break;
                            }                           
                        }
                        else
                        {
                            Debug.LogWarning("Button " + button.name + " 没有找到TextMeshProUGUI组件");
                        }
                    }
                }
            }
        }
    }

}
