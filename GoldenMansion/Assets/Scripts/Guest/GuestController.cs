using ExcelData;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class GuestController : MonoBehaviour
{
    private static GuestController instance;
    public List<int> GuestStorage = new List<int>();

    public static GuestController Instance
    {
        get
        {
            if (instance != null)
            {
                instance = FindObjectOfType<GuestController>();
                if (instance == null)
                {
                    GameObject guestController = new GameObject("GuestController");
                    instance = guestController.AddComponent<GuestController>();
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
}
