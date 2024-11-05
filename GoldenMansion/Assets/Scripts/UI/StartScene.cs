using System.Collections;
using System.Collections.Generic;
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
    public void StartNewGame()
    {
        SaveSystem.Instance.DeleteData(SaveSystem.Instance.saveName);
        SceneManager.UnloadSceneAsync("StartScene");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameScene"));
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
        SceneManager.UnloadSceneAsync("StartScene");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameScene"));
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("UIScene"));
        var data = SaveSystem.Instance.LoadData<SaveData>(SaveSystem.Instance.saveName);
        SaveSystem.Instance.readData(data);
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
}
