using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private static UIController instance;

    public static UIController Instance
    {
        get
        {
            if (instance != null)
            {
                instance = FindObjectOfType<UIController>();
                if (instance == null)
                {
                    GameObject uiController = new GameObject("UIController");
                    instance = uiController.AddComponent<UIController>();

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

    public void CloseThisPanel(GameObject thisPanel)
    {
        thisPanel.SetActive(false);
    }

    public void StartBuildMode()
    {
        ApartmentController.Instance.isBuildMode = true;
    }

    public void GuestMoveIn()
    {
        for (int i = 0; i < ApartmentController.Instance.unLockedApartmentCount; i++)
        {
            Vector3 apartmentPosition = ApartmentController.Instance.apartmentPosition[i];
            Instantiate(GuestController.Instance.guestInApartmentPrefab, apartmentPosition, transform.rotation);
        }
    }

    public void GetApartmentPosition()
    {
        
    }
}
