using ExcelData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.UIElements.UxmlAttributeDescription;


public class SaveSystem : MonoBehaviour
{
    private static SaveSystem instance;
    public string saveName = "save.sav";

    public int temporPersonaIDOne;
    public int temporPersonaIDTwo;
    public int temporGuestIDOne;
    public int temporGuestIDTwo;
    public int temporGuestIDThree;

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
        saveData.isEndlessMode = GameManager.Instance.isEndlessMode;
        saveData.endlessModeTarget = GameManager.Instance.endlessModeTarget;
        saveData.endlessModeTargetTimes = GameManager.Instance.endlessModeTargetTimes;
        saveData.endlessModeDays = GameManager.Instance.endlessModeDays;
        saveData.endlessModeDaysPlus = GameManager.Instance.endlessModeDaysPlus;
        saveData.rerollTime = GameManager.Instance.rerollTime;
        saveData.guestIDOne = GameObject.Find("ChooseCardPanel(Clone)").transform.Find("ChooseGuestSlot").Find("Guest1").GetComponent<Guest>().key;
        saveData.guestIDTwo = GameObject.Find("ChooseCardPanel(Clone)").transform.Find("ChooseGuestSlot").Find("Guest1 (1)").GetComponent<Guest>().key;
        saveData.guestIDThree = GameObject.Find("ChooseCardPanel(Clone)").transform.Find("ChooseGuestSlot").Find("Guest1 (2)").GetComponent<Guest>().key;
        saveData.personaIDOne = GameObject.Find("ChooseCardPanel(Clone)").transform.Find("ChoosePersonaSlot").Find("Persona1").GetComponent<GievePersonaButton>().personaKey;
        saveData.personaIDTwo = GameObject.Find("ChooseCardPanel(Clone)").transform.Find("ChoosePersonaSlot").Find("Persona2").GetComponent<GievePersonaButton>().personaKey;
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
            saveData.guestBasicCost.Add(guest.GetComponent<GuestInApartment>().guestBasicCost);
            saveData.guestExtraCost.Add(guest.GetComponent<GuestInApartment>().guestExtraCost);
            saveData.guestBasicPrice.Add(guest.GetComponent<GuestInApartment>().guestBasicPrice);
            saveData.guestExtraPrice.Add(guest.GetComponent<GuestInApartment>().guestExtraPrice);
            saveData.guestBudget.Add(guest.GetComponent<GuestInApartment>().guestBudget);
            saveData.guestExtraBudget.Add(guest.GetComponent<GuestInApartment>().guestExtraBudget);
            saveData.guestIsDestroyable.Add(guest.GetComponent<GuestInApartment>().isDestroyable);
            saveData.guestTourDays.Add(guest.GetComponent<GuestInApartment>().tourDays);
            saveData.guestDays.Add(guest.GetComponent<GuestInApartment>().guestDays);
            saveData.guestMBTI.Add(guest.GetComponent<GuestInApartment>().mbti);
            saveData.guestAdjancentPrice.Add(guest.GetComponent<GuestInApartment>().adjancentPrice);
        }
        string[] moneyLeft = new string[1];
        moneyLeft[0] = saveData.vaultMoney.ToString();
        WriteToCsv("C:/Users/8/Desktop/数值.csv", moneyLeft);
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
        GameManager.Instance.isEndlessMode = saveData.isEndlessMode;
        GameManager.Instance.endlessModeDays = saveData.endlessModeDays;
        GameManager.Instance.endlessModeDaysPlus = saveData.endlessModeDaysPlus;
        GameManager.Instance.endlessModeTarget = saveData.endlessModeTarget;
        GameManager.Instance.endlessModeTargetTimes = saveData.endlessModeTargetTimes;
        GameManager.Instance.rerollTime = saveData.rerollTime;
        temporGuestIDOne = saveData.guestIDOne;
        temporGuestIDTwo = saveData.guestIDTwo;
        temporGuestIDThree = saveData.guestIDThree;
        temporPersonaIDOne = saveData.personaIDOne;
        temporPersonaIDTwo = saveData.personaIDTwo;
        LoadGuestData(saveData);
        
        //StorageController.Instance.guestStorage = saveData.guestStorage;
    }

    

    public void LoadGuestData(SaveData saveData)
    {
        Dictionary<string, int> guestKey = new Dictionary<string, int>();
        Dictionary<string, string> guestPersona = new Dictionary<string, string>();
        Dictionary<string, int> guestBasicCost = new Dictionary<string, int>();
        Dictionary<string, int> guestExtraCost = new Dictionary<string, int>();
        Dictionary<string, int> guestBasicPrice = new Dictionary<string, int>();
        Dictionary<string, int> guestExtraPrice = new Dictionary<string, int>();
        Dictionary<string, int> guestBudget = new Dictionary<string, int>();
        Dictionary<string, int> guestExtraBudget = new Dictionary<string, int>();
        Dictionary<string, bool> guestIsDestroyable = new Dictionary<string, bool>();
        Dictionary<string, int> guestTourDays = new Dictionary<string, int>();
        Dictionary<string, int> guestDays = new Dictionary<string, int>();
        Dictionary<string, int> guestMBTI = new Dictionary<string, int>();
        Dictionary<string, int> guestAdjancentPrice = new Dictionary<string, int>();
        for (int i = 0; i < saveData.guestID.Count; i++)
        {
            guestKey.Add(saveData.guestID[i]+"guestKey", saveData.guestKey[i]);
            guestBasicCost.Add(saveData.guestID[i] + "guestBasicCost", saveData.guestBasicCost[i]);
            guestExtraCost.Add(saveData.guestID[i] + "guestExtraCost", saveData.guestExtraCost[i]);
            guestBasicPrice.Add(saveData.guestID[i] + "guestBasicPrice", saveData.guestBasicPrice[i]);
            guestExtraPrice.Add(saveData.guestID[i] + "guestExtraPrice", saveData.guestExtraPrice[i]);
            guestBudget.Add(saveData.guestID[i] + "guestBudget", saveData.guestBudget[i]);
            guestExtraBudget.Add(saveData.guestID[i] + "guestExtraBudget", saveData.guestExtraBudget[i]);
            guestIsDestroyable.Add(saveData.guestID[i] + "guestIsDestroyable", saveData.guestIsDestroyable[i]);
            guestTourDays.Add(saveData.guestID[i] + "guestTourDays", saveData.guestTourDays[i]);
            guestDays.Add(saveData.guestID[i] + "guestDays", saveData.guestDays[i]);
            guestMBTI.Add(saveData.guestID[i] + "guestMBTI", saveData.guestMBTI[i]);
            guestAdjancentPrice.Add(saveData.guestID[i] + "guestAdjancentPrice", saveData.guestAdjancentPrice[i]);
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
            guest.GetComponent<GuestInApartment>().guestBasicCost = guestBasicCost[guestID + "guestBasicCost"];
            guest.GetComponent<GuestInApartment>().guestExtraCost = guestExtraCost[guestID + "guestExtraCost"];
            guest.GetComponent<GuestInApartment>().guestBasicPrice = guestBasicPrice[guestID + "guestBasicPrice"];
            guest.GetComponent<GuestInApartment>().guestExtraPrice = guestExtraPrice[guestID + "guestExtraPrice"];
            guest.GetComponent<GuestInApartment>().guestBudget = guestBudget[guestID + "guestBudget"];
            guest.GetComponent<GuestInApartment>().guestExtraBudget = guestExtraBudget[guestID + "guestExtraBudget"];
            guest.GetComponent<GuestInApartment>().isDestroyable = guestIsDestroyable[guestID + "guestIsDestroyable"];
            guest.GetComponent<GuestInApartment>().tourDays = guestTourDays[guestID + "guestTourDays"];
            guest.GetComponent<GuestInApartment>().guestDays = guestDays[guestID + "guestDays"];
            guest.GetComponent<GuestInApartment>().mbti = guestMBTI[guestID + "guestMBTI"];
            guest.GetComponent<GuestInApartment>().adjancentPrice = guestAdjancentPrice[guestID + "guestAdjancentPrice"];
            if (guestPersona[guestID + "guestPersona"] != "")
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
            guest.GetComponent<GuestInApartment>().field = CharacterData.GetItem(guest.GetComponent<GuestInApartment>().key).field;
            guest.GetComponent<GuestInApartment>().fieldSkillID = FieldData.GetItem(guest.GetComponent<GuestInApartment>().field).skillID;
            guest.GetComponentInChildren<SpriteRenderer>().sprite = Resources.Load<Sprite>(CharacterData.GetItem(guest.GetComponent<GuestInApartment>().key).portraitRoute + "inapartment");
            guest.GetComponent<GuestInApartment>().guestName = CharacterData.GetItem(guest.GetComponent<GuestInApartment>().key).name;
            guest.SetActive(true);
            guest.GetComponentInChildren<SpriteRenderer>().enabled = false;
            guest.GetComponent<BoxCollider>().enabled = false;
            if (guest.GetComponent<GuestInApartment>().mbti != 0)
            {
                guest.GetComponent<GuestInApartment>().GetMBTISkill(guest.GetComponent<GuestInApartment>().mbti);
            }
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

    public void WriteToCsv(string filePath, string[] data)
    {
        // 检查文件是否存在
        bool fileExists = File.Exists(filePath);

        // 使用StreamWriter，指定UTF-8编码和BOM
        using (StreamWriter writer = new StreamWriter(filePath, append: true, encoding: new UTF8Encoding(true)))
        {
            // 如果文件不存在，写入标题行（可选）
            if (!fileExists)
            {
                writer.WriteLine("Column1,Column2,Column3"); // 根据数据结构自定义列名
            }

            // 写入数据
            writer.WriteLine(string.Join(",", data));
        }
    }

}
