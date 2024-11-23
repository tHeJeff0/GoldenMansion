using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public int gameDays { get; set; } = 0;
    public bool isChooseCardFinish { get; set; } = false;
    public bool isRoundEnd { get; set; } = false;
    public int levelKey { get; set; } = 1;
    public int basicRerollTime { get; set; } = 1;
    public int extraRerollTime { get; set; }
    public int rerollTime { get; set; } = 1;
    public int guestRemoveCount { get; set; }
    public bool isAllowSell { get; set; } = true;
    public bool isAllowBuy { get; set; } = true;
    public int mediaDays { get; set; } = -1;
    public int storyID { get; set; } = 0;
    public int Language { get; set; } = 0;
    public float SFVolume { get; set; } = 1;
    public bool isEndlessMode { get; set; } = false;
    public float endlessModeTarget { get; set; }
    public float endlessModeTargetTimes { get; set; } = 1.5f;
    public int endlessModeDays { get; set; }
    public int endlessModeDaysPlus { get; set; } = 3;

    public bool canQuit { get; set; } = true; 

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject gameManager = new GameObject("GameManager");
                    instance = gameManager.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        Application.wantsToQuit += HandleWantsToQuit;
        Application.quitting += OnQuit;

    }

    private void OnDestroy()
    {
        Application.wantsToQuit -= HandleWantsToQuit;
        Application.quitting -= OnQuit;
    }

    bool HandleWantsToQuit()
    {
        return canQuit;
    }

    void OnQuit()
    {
        SaveSystem.Instance.SaveData(SaveSystem.Instance.saveName);
    }
    


}
