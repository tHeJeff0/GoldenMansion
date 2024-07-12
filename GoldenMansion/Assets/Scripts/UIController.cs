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
        List<Vector3> temporPosition = new List<Vector3>();

        if (GuestController.Instance.GuestStorage.Count < ApartmentController.Instance.unLockedApartmentCount)
        {
            int removeUnlockedApartmentCount = ApartmentController.Instance.unLockedApartmentCount - GuestController.Instance.GuestStorage.Count;
            for (int i = 0; i < removeUnlockedApartmentCount; i++)
            {
                int randomRemoveID = Random.Range(0, ApartmentController.Instance.apartmentPosition.Count - 1);               
                temporPosition.Add(ApartmentController.Instance.apartmentPosition[randomRemoveID]);
                ApartmentController.Instance.apartmentPosition.RemoveAt(randomRemoveID);
            }
        }
        for (int i = 0; i < ApartmentController.Instance.unLockedApartmentCount; i++)
        {
            if (i < GuestController.Instance.GuestStorage.Count)
            {
                Vector3 apartmentPosition = ApartmentController.Instance.apartmentPosition[i];
                Instantiate(GuestController.Instance.guestInApartmentPrefab, apartmentPosition, transform.rotation);
                
            }
            
        }

        ApartmentController.Instance.apartmentPosition.AddRange(temporPosition);
        temporPosition.Clear();

    }

    public void GetApartmentPosition()
    {
        
    }
}
