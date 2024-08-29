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
    public int guestRemoveCount { get; set; }

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

    public void LoadSkill(int skillID)
    {

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
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
