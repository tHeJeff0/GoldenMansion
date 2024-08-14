using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventQueueManager : MonoBehaviour
{
    private static EventQueueManager instance;
    private Queue<Action> eventQueue = new Queue<Action>();

    public static EventQueueManager Instance
    {
        get
        {
            if (instance != null)
            {
                instance = FindObjectOfType<EventQueueManager>();
                if (instance == null)
                {
                    GameObject eventQueueManager = new GameObject("EventQueueManager");
                    instance = eventQueueManager.AddComponent<EventQueueManager>();
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterEvent(Action action)
    {
        eventQueue.Enqueue(action);
    }

    public void ExecuteEvents()
    {
        StartCoroutine(ExecuteEventCoroutine());
    }

    private IEnumerator<WaitUntil> ExecuteEventCoroutine()
    {
        while (eventQueue.Count > 0)
        {
            Action currentEvent = eventQueue.Dequeue();
            currentEvent();
            
        }
        yield return new WaitUntil(() => eventQueue.Count == 0);
    }
}
