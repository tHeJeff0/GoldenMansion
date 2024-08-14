using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEventSystem : MonoBehaviour
{
    private static UIEventSystem instance;

    public static UIEventSystem Instance
    {
        get
        {
            if (instance != null)
            {
                instance = FindObjectOfType<UIEventSystem>();
                if (instance == null)
                {
                    GameObject uiEventSystem = new GameObject("UIEventSystem");
                    instance = uiEventSystem.AddComponent<UIEventSystem>();
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Execute()
    {

        EventQueueManager.Instance.RegisterEvent(UIController.Instance.GuestMoveIn);

        EventQueueManager.Instance.RegisterEvent(ApartmentController.Instance.GuestPayRent);
        //EventQueueManager.Instance.RegisterEvent(GameManager.Instance.PlusGameDays);
        EventQueueManager.Instance.RegisterEvent(UIController.Instance.StartInstantiateMenu);

        EventQueueManager.Instance.ExecuteEvents();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
