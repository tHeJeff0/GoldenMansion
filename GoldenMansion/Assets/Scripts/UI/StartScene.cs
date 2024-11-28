using ExcelData;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    [SerializeField] GameObject gameGroup;
    [SerializeField] GameObject chooseLanguageGroup;
    [SerializeField] GameObject settingPanel;

    private void Awake()
    {
        SaveSystem.Instance.LoadPlayerPrefs();
#if !UNITY_EDITOR
        SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Camera", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("UIScene", LoadSceneMode.Additive);      
#endif

        if (GameManager.Instance.Language == 0)
        {
            gameGroup.SetActive(false);
            chooseLanguageGroup.SetActive(true);
        }
        else
        {
            gameGroup.SetActive(true);
            chooseLanguageGroup.SetActive(false);
        }
        Debug.Log(GameManager.Instance.gameDays);
        
    }

    private void Start()
    {
        SaveSystem.Instance.TranslateButtonText();
        //GameObject.Find("Main Camera").GetComponent<AudioSource>().enabled = true;
        StartCoroutine(BGMFadeIn(0.3f));

    }
    public void StartNewGame()
    {
        SaveSystem.Instance.DeleteData(SaveSystem.Instance.saveName);
        SceneManager.UnloadSceneAsync("StartScene");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameScene"));
        //GameObject.Find("Main Camera").GetComponent<AudioSource>().enabled = true;
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("UIScene"));

    }

    public void ContinueGame()
    {
        for (int i = GuestController.Instance.GuestInApartmentPrefabStorage.Count-1; i >= 0; i--)
        {
            Destroy(GuestController.Instance.GuestInApartmentPrefabStorage[i]);
        }
        GuestController.Instance.GuestInApartmentPrefabStorage.Clear();
        StorageController.Instance.guestStorage.Clear();
        var data = SaveSystem.Instance.LoadData<SaveData>(SaveSystem.Instance.saveName);
        SaveSystem.Instance.readData(data);
        UIController.Instance.GenerateMenu();
        LoadGuestToChoose();
        LoadPersonaToChoose();
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameScene"));
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("UIScene"));
        SceneManager.UnloadSceneAsync("StartScene");
        //GameObject.Find("Main Camera").GetComponent<AudioSource>().enabled = true;
    }

    public void QuitGame()
    {

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void ChooseChinese()
    {
        GameManager.Instance.Language = 1;
        SaveSystem.Instance.SavePlayerPrefs();
        gameGroup.SetActive(true);
        chooseLanguageGroup.SetActive(false);
    }

    public void ChooseEnglish()
    {
        GameManager.Instance.Language = 2;
        SaveSystem.Instance.SavePlayerPrefs();
        gameGroup.SetActive(true);
        chooseLanguageGroup.SetActive(false);

    }

    public void ChooseRussian()
    {

    }

    public void ChooseSpanish()
    {

    }

    public void ChoosePortuguese()
    {

    }

    public void ChooseJapanese()
    {

    }

    public void ChooseGerman()
    {

    }

    public void ChooseFrench()
    {

    }

    public void ChooseTraditionalChinese()
    {
        GameManager.Instance.Language = 3;
        SaveSystem.Instance.SavePlayerPrefs();
        gameGroup.SetActive(true);
        chooseLanguageGroup.SetActive(false);
    }

    public void CallSettingPanel()
    {
        settingPanel.SetActive(true);
    }

    IEnumerator BGMFadeIn(float fadeInSpeed)
    {
        for (int i = 0; i < 10; i++)
        {
            GetComponent<AudioSource>().volume = i/10.0f;
            yield return new WaitForSecondsRealtime(fadeInSpeed);
        }             
    }

    void LoadGuestToChoose()
    {
        if (SaveSystem.Instance.temporGuestIDOne != 0)
        {
            GameObject.Find("ChooseCardPanel(Clone)").transform.Find("ChooseGuestSlot").Find("Guest1").GetComponent<Guest>().LoadGuestMessage(SaveSystem.Instance.temporGuestIDOne);
        }
        else
        {
            GameObject.Find("ChooseCardPanel(Clone)").transform.Find("ChooseGuestSlot").Find("Guest1").gameObject.SetActive(false);
        }
        if (SaveSystem.Instance.temporGuestIDTwo != 0)
        {
            GameObject.Find("ChooseCardPanel(Clone)").transform.Find("ChooseGuestSlot").Find("Guest1 (1)").GetComponent<Guest>().LoadGuestMessage(SaveSystem.Instance.temporGuestIDTwo);
        }
        else
        {
            GameObject.Find("ChooseCardPanel(Clone)").transform.Find("ChooseGuestSlot").Find("Guest1 (1)").gameObject.SetActive(false);
        }
        if (SaveSystem.Instance.temporGuestIDThree != 0)
        {
            GameObject.Find("ChooseCardPanel(Clone)").transform.Find("ChooseGuestSlot").Find("Guest1 (2)").GetComponent<Guest>().LoadGuestMessage(SaveSystem.Instance.temporGuestIDThree);
        }
        else
        {
            GameObject.Find("ChooseCardPanel(Clone)").transform.Find("ChooseGuestSlot").Find("Guest1 (2)").gameObject.SetActive(false);
        }
        GameObject.Find("ChooseCardPanel(Clone)").transform.Find("ChooseGuestSlot").Find("Guest1").GetComponent<Guest>().key = SaveSystem.Instance.temporGuestIDOne;
        GameObject.Find("ChooseCardPanel(Clone)").transform.Find("ChooseGuestSlot").Find("Guest1 (1)").GetComponent<Guest>().key = SaveSystem.Instance.temporGuestIDTwo;
        GameObject.Find("ChooseCardPanel(Clone)").transform.Find("ChooseGuestSlot").Find("Guest1 (2)").GetComponent<Guest>().key = SaveSystem.Instance.temporGuestIDThree;
    }

    void LoadPersonaToChoose()
    {
        if (SaveSystem.Instance.temporPersonaIDOne != 0)
        {
            GameObject.Find("ChooseCardPanel(Clone)").transform.Find("ChoosePersonaSlot").Find("Persona1").GetComponent<GievePersonaButton>().GenerateButtonPic(SaveSystem.Instance.temporPersonaIDOne);
        }
        else
        {
            GameObject.Find("ChooseCardPanel(Clone)").transform.Find("ChoosePersonaSlot").Find("Persona1").gameObject.SetActive(false);
        }
        if (SaveSystem.Instance.temporPersonaIDTwo != 0)
        {
            GameObject.Find("ChooseCardPanel(Clone)").transform.Find("ChoosePersonaSlot").Find("Persona2").GetComponent<GievePersonaButton>().GenerateButtonPic(SaveSystem.Instance.temporPersonaIDTwo);
        }
        else
        {
            GameObject.Find("ChooseCardPanel(Clone)").transform.Find("ChoosePersonaSlot").Find("Persona2").gameObject.SetActive(false);
        }
        GameObject.Find("ChooseCardPanel(Clone)").transform.Find("ChoosePersonaSlot").Find("Persona1").GetComponent<GievePersonaButton>().personaKey = SaveSystem.Instance.temporPersonaIDOne;
        GameObject.Find("ChooseCardPanel(Clone)").transform.Find("ChoosePersonaSlot").Find("Persona2").GetComponent<GievePersonaButton>().personaKey = SaveSystem.Instance.temporPersonaIDTwo;

    }
}
