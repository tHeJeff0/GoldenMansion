using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BGMVolumeChange()
    {
        GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = transform.Find("SoundSetting").Find("BGMSlider").GetComponent<Slider>().value;
    }

    public void SFVolumeChange()
    {
        GameManager.Instance.SFVolume = transform.Find("SoundSetting").Find("SFSlider").GetComponent<Slider>().value;
    }

    public void ChooseChinese()
    {
        GameManager.Instance.Language = 1;
        SaveSystem.Instance.SavePlayerPrefs();
        SaveSystem.Instance.LoadPlayerPrefs();
    }

    public void ChooseEnglish()
    {
        GameManager.Instance.Language = 2;
        SaveSystem.Instance.SavePlayerPrefs();
        SaveSystem.Instance.LoadPlayerPrefs();
    }

    public void ChooseTraditionalChinese()
    {
        GameManager.Instance.Language = 3;
        SaveSystem.Instance.SavePlayerPrefs();
        SaveSystem.Instance.LoadPlayerPrefs();
    }

    public void QuitGame()
    {
        SaveSystem.Instance.SaveData(SaveSystem.Instance.saveName);

        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void ChangeScreenSize()
    {
        int value = GameObject.Find("Pixel").GetComponent<TMP_Dropdown>().value;
        Debug.Log("value:" + value);
        switch (value)
        {
            
            case 0:
                Screen.SetResolution(462, 174, Screen.fullScreen);
                SaveSystem.Instance.LoadPlayerPrefs();
                break;
            case 1:
                Screen.SetResolution(715, 402, Screen.fullScreen);
                SaveSystem.Instance.LoadPlayerPrefs();
                break;
            case 2:
                Screen.SetResolution(1024, 600, Screen.fullScreen);
                SaveSystem.Instance.LoadPlayerPrefs();
                break;
            case 3:
                Screen.SetResolution(1024, 768, Screen.fullScreen);
                SaveSystem.Instance.LoadPlayerPrefs();
                break;
            case 4:
                Screen.SetResolution(1280, 800, Screen.fullScreen);
                SaveSystem.Instance.LoadPlayerPrefs();
                break;
            case 5:
                Screen.SetResolution(1280, 1024, Screen.fullScreen);
                SaveSystem.Instance.LoadPlayerPrefs();
                break;
            case 6:
                Screen.SetResolution(1366, 768, Screen.fullScreen);
                SaveSystem.Instance.LoadPlayerPrefs();
                break;
            case 7:
                Screen.SetResolution(1440, 1050, Screen.fullScreen);
                SaveSystem.Instance.LoadPlayerPrefs();
                break;
            case 8:
                Screen.SetResolution(1600, 900, Screen.fullScreen);
                SaveSystem.Instance.LoadPlayerPrefs();
                break;
            case 9:
                Screen.SetResolution(1600, 1200, Screen.fullScreen);
                SaveSystem.Instance.LoadPlayerPrefs();
                break;
            case 10:
                Screen.SetResolution(1680, 1050, Screen.fullScreen);
                SaveSystem.Instance.LoadPlayerPrefs();
                break;
            case 11:
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                SaveSystem.Instance.LoadPlayerPrefs();
                break;
            case 12:
                Screen.SetResolution(1920, 1200, Screen.fullScreen);
                SaveSystem.Instance.LoadPlayerPrefs();
                break;
        }       
    }

    public void FullScreen()
    {

        if (Screen.fullScreen)
        {
            Screen.fullScreen = GameObject.Find("FullScreenSelection").GetComponent<Toggle>().isOn;
        }
        else
        {
            Screen.fullScreen = GameObject.Find("FullScreenSelection").GetComponent<Toggle>().isOn;
        }
    }

    public void CloseSettingPanel()
    {
        gameObject.SetActive(false);
    }

}
