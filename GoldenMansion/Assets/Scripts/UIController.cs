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

        if (Level.GetItem(GameManager.Instance.levelKey).days - GameManager.Instance.gameDays >= 0)
        {
            Instantiate(chooseCardPanel, thisCanvas.transform);
            Debug.Log("成功生成选卡panel");
        }
        else
        {
            Instantiate(roundEndPanel, thisCanvas.transform);
        }

        if (GuestController.Instance.GuestInApartmentPrefabStorage.Count < ApartmentController.Instance.unLockedApartmentCount)
        {
            int removeUnlockedApartmentCount = ApartmentController.Instance.unLockedApartmentCount - GuestController.Instance.GuestInApartmentPrefabStorage.Count;
            for (int i = 0; i < removeUnlockedApartmentCount; i++)
            {
                int randomRemoveID = Random.Range(0, ApartmentController.Instance.apartment.Count);
                temporApartmentObject.Add(ApartmentController.Instance.apartment[randomRemoveID]);
                ApartmentController.Instance.apartment.RemoveAt(randomRemoveID);
            }
        }
        for (int i = 0; i < ApartmentController.Instance.apartment.Count; i++)
        {
            if (i < GuestController.Instance.GuestInApartmentPrefabStorage.Count)
            {
                int randomGuest = Random.Range(0, GuestController.Instance.GuestInApartmentPrefabStorage.Count);
                Debug.Log(randomGuest);
                //Instantiate(GuestController.Instance.GuestInApartmentPrefabStorage[randomGuest], ApartmentController.Instance.apartment[i].transform);
                GuestController.Instance.GuestInApartmentPrefabStorage[randomGuest].transform.SetParent(ApartmentController.Instance.apartment[i].transform);
                GuestController.Instance.GuestInApartmentPrefabStorage[randomGuest].transform.localPosition = Vector3.zero;
                GuestController.Instance.GuestInApartmentPrefabStorage[randomGuest].SetActive(true);
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
