using ExcelData;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundEndPanelController : MonoBehaviour
{
    //[SerializeField] private GameObject quitBuildButton;
    GameObject commitRentGroup;
    //GameObject buildButton;
    GameObject nextLevelButton;
    GameObject winGroup;

    private void Start()
    {
        SaveSystem.Instance.TranslateButtonText();
    }
    private void Awake()
    {
        commitRentGroup = GameObject.Find("CommitRentGroup");
        //buildButton = GameObject.Find("BuildButton");
        nextLevelButton = GameObject.Find("NextLevelButton");
        winGroup = GameObject.Find("WinGroup");

        commitRentGroup.SetActive(true);
        //buildButton.SetActive(false);
        nextLevelButton.SetActive(false);
        winGroup.SetActive(false);
        
    }
    public void GoToNextLevel()
    {
        GameManager.Instance.isRoundEnd = false;
        Destroy(this.gameObject);
        GameManager.Instance.levelKey += 1;
    }

    public void CommitRent()
    {
        float vaultMoney = ApartmentController.Instance.vaultMoney;
        if (!GameManager.Instance.isEndlessMode)//如果不是无尽模式
        {
            float targetMoney = Level.GetItem(GameManager.Instance.levelKey).target;
            float moneyLeft = vaultMoney - targetMoney;
            if (moneyLeft >= 0)//如果金币足够交租
            {
                //buildButton.SetActive(true);
                if (GameManager.Instance.levelKey + 1 > 8)//如果是最后一关
                {
                    winGroup.SetActive(true);
                    switch (GameManager.Instance.Language)
                    {
                        case 1:
                            winGroup.transform.Find("StoryText").GetComponentInChildren<TextMeshProUGUI>().text = ButtonLanguageData.GetItem(winGroup.transform.Find("StoryText").GetComponentInChildren<TextMeshProUGUI>().text).CHN;
                            break;
                        case 2:
                            winGroup.transform.Find("StoryText").GetComponentInChildren<TextMeshProUGUI>().text = ButtonLanguageData.GetItem(winGroup.transform.Find("StoryText").GetComponentInChildren<TextMeshProUGUI>().text).ENG;
                            break;
                        case 3:
                            winGroup.transform.Find("StoryText").GetComponentInChildren<TextMeshProUGUI>().text = ButtonLanguageData.GetItem(winGroup.transform.Find("StoryText").GetComponentInChildren<TextMeshProUGUI>().text).TCHN;
                            break;
                    }
                    commitRentGroup.SetActive(false);
                    ApartmentController.Instance.vaultMoney = moneyLeft;
                    Debug.Log("赢了！");

                }
                else//如果不是最后一关
                {
                    nextLevelButton.SetActive(true);
                    commitRentGroup.transform.Find("CommitButton").gameObject.SetActive(false);
                    GameManager.Instance.storyID += 1;
                    switch (GameManager.Instance.Language)
                    {
                        case 1:
                            commitRentGroup.transform.Find("StoryText").GetComponentInChildren<TextMeshProUGUI>().text = LanguageData.GetItem(ChapterStoryData.GetItem(GameManager.Instance.storyID).languageID).CHN;
                            break;
                        case 2:
                            commitRentGroup.transform.Find("StoryText").GetComponentInChildren<TextMeshProUGUI>().text = LanguageData.GetItem(ChapterStoryData.GetItem(GameManager.Instance.storyID).languageID).ENG;
                            break;
                        case 3:
                            commitRentGroup.transform.Find("StoryText").GetComponentInChildren<TextMeshProUGUI>().text = LanguageData.GetItem(ChapterStoryData.GetItem(GameManager.Instance.storyID).languageID).TCHN;
                            break;
                    }
                    ApartmentController.Instance.vaultMoney = moneyLeft;
                }

            }
            else//如果金币不够交租
            {
                GameManager.Instance.isRoundEnd = false;
                Destroy(this.gameObject);
                commitRentGroup.SetActive(false);
                GameReset();
                SceneManager.LoadScene("StartScene", LoadSceneMode.Additive);
                Debug.Log("Game Over!");
            }
        }
        else//如果是无尽模式
        {
            float targetMoney = GameManager.Instance.endlessModeTarget;
            float moneyLeft = vaultMoney - targetMoney;
            if (moneyLeft >= 0)//如果金币足够交租
            {
                nextLevelButton.SetActive(true);
                commitRentGroup.transform.Find("CommitButton").gameObject.SetActive(false);
                GameManager.Instance.storyID += 1;
                GameManager.Instance.endlessModeTarget *= GameManager.Instance.endlessModeTargetTimes;
                GameManager.Instance.endlessModeDays += GameManager.Instance.endlessModeDaysPlus;
                switch (GameManager.Instance.Language)
                {
                    case 1:
                        commitRentGroup.transform.Find("StoryText").GetComponent<TextMeshProUGUI>().text = LanguageData.GetItem(ChapterStoryData.GetItem(GameManager.Instance.storyID).languageID).CHN;
                        break;
                    case 2:
                        commitRentGroup.transform.Find("StoryText").GetComponent<TextMeshProUGUI>().text = LanguageData.GetItem(ChapterStoryData.GetItem(GameManager.Instance.storyID).languageID).ENG;
                        break;
                    case 3:
                        commitRentGroup.transform.Find("StoryText").GetComponent<TextMeshProUGUI>().text = LanguageData.GetItem(ChapterStoryData.GetItem(GameManager.Instance.storyID).languageID).TCHN;
                        break;
                }
                ApartmentController.Instance.vaultMoney = moneyLeft;
            }
            else//如果金币不够交租
            {
                GameManager.Instance.isRoundEnd = false;
                Destroy(this.gameObject);
                commitRentGroup.SetActive(false);
                GameReset();
                SceneManager.LoadScene("StartScene", LoadSceneMode.Additive);
                Debug.Log("Game Over!");
            }
        }
        
    }

    private void GameReset()
    {
        GameManager.Instance.gameDays = 0;
        GameManager.Instance.levelKey = 1;
        GameManager.Instance.mediaDays = -1;       
        foreach (var apartment in ApartmentController.Instance.apartment)
        {
            apartment.GetComponent<Apartment>().apartmentDays = 0;
        }
        foreach (var guestInApartment in GuestController.Instance.GuestInApartmentPrefabStorage)
        {
            Destroy(guestInApartment);
        }
        ApartmentController.Instance.vaultMoney = 0;
        GuestController.Instance.GuestInApartmentPrefabStorage.Clear();
        GuestController.Instance.GenerateBasicGuest(3);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ContinuePlaying()
    {
        GameManager.Instance.isEndlessMode = true;
        GameManager.Instance.endlessModeTarget = Level.GetItem(5).target * GameManager.Instance.endlessModeTargetTimes;
        GameManager.Instance.endlessModeDays = Level.GetItem(5).days + GameManager.Instance.endlessModeDaysPlus;
        GoToNextLevel();
    }
}
