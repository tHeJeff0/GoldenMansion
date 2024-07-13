using ExcelData;
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
    [SerializeField] private TextMeshProUGUI targetText;
    [SerializeField] private GameObject chooseCardPanel;
    [SerializeField] private GameObject roundEndPanel;
    [SerializeField] private GameObject thisCanvas;

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
        targetText.text = string.Format("{0}天后上交租金:{1}", Level.GetItem(GameManager.Instance.levelKey).days - GameManager.Instance.gameDays, Level.GetItem(1).target);

        if (GameManager.Instance.isChooseCardFinish)
        {
            Destroy(GameObject.Find("ChooseCardPanel(Clone)"));
            GameManager.Instance.isChooseCardFinish = false;
        }
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
        
        if(Level.GetItem(GameManager.Instance.levelKey).days - GameManager.Instance.gameDays >= 0)
        {
            Instantiate(chooseCardPanel, thisCanvas.transform);
        }
        else
        {
            Instantiate(roundEndPanel, thisCanvas.transform);
        }
        

    }

    public void GetApartmentPosition()
    {
        
    }
}
