using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private static UIController instance;
    [SerializeField] private TextMeshProUGUI vaultMoneyText;

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
        vaultMoneyText.text = ApartmentController.Instance.vaultMoney.ToString();
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
        GameManager.Instance.gameDays += 1;
        List<Vector3> temporPosition = new List<Vector3>();
        List<GameObject> temporApartmentObject = new List<GameObject>();

        if (GuestController.Instance.GuestStorage.Count < ApartmentController.Instance.unLockedApartmentCount)
        {
            int removeUnlockedApartmentCount = ApartmentController.Instance.unLockedApartmentCount - GuestController.Instance.GuestStorage.Count;
            for (int i = 0; i < removeUnlockedApartmentCount; i++)
            {
                int randomRemoveID = Random.Range(0, ApartmentController.Instance.apartment.Count - 1);
                temporApartmentObject.Add(ApartmentController.Instance.apartment[randomRemoveID]);
                ApartmentController.Instance.apartment.RemoveAt(randomRemoveID);
            }
        }
        for (int i = 0; i < ApartmentController.Instance.unLockedApartmentCount; i++)
        {
            if (i < GuestController.Instance.GuestStorage.Count)
            {
                Instantiate(GuestController.Instance.guestInApartmentPrefab, ApartmentController.Instance.apartment[i].transform);
            }
            
        }

        ApartmentController.Instance.CheckPayedCount();
        ApartmentController.Instance.apartment.AddRange(temporApartmentObject);
        temporPosition.Clear();

    }

    public void GetApartmentPosition()
    {
        
    }
}
