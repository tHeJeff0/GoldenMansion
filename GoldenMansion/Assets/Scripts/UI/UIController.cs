using ExcelData;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    
    private static UIController instance;
    //[SerializeField] private TextMeshProUGUI vaultMoneyText;
    //[SerializeField] private TextMeshProUGUI targetText;
    [SerializeField] private GameObject chooseCardPanel;
    [SerializeField] private GameObject storagePanel;
    [SerializeField] private GameObject roundEndPanel;
    [SerializeField] private GameObject thisCanvas;
    [SerializeField] private GameObject progressBar;

    public List<string> FirstFilterStageSelection = new List<string>();

    int guestInApartmentPrefabCount;
    int unlockedApartmentCount;

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
        //vaultMoneyText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        
        //targetText.text = string.Format("{0}天后上交租金:{1}", Level.GetItem(GameManager.Instance.levelKey).days - GameManager.Instance.gameDays, Level.GetItem(GameManager.Instance.levelKey).target);
        //progressBar.GetComponent<Image>().fillAmount = ApartmentController.Instance.vaultMoney / Level.GetItem(GameManager.Instance.levelKey).target;
    }


    public void GuestMoveIn()
    {
        
        guestInApartmentPrefabCount = GuestController.Instance.GuestInApartmentPrefabStorage.Count;
        unlockedApartmentCount = ApartmentController.Instance.apartment.Count;

        
        List<int> randomGuestTagList = GenerateRandomGuestTagList();
        List<int> randomApartmentTagList = GenerateRandomApartmentTagList();
        

        if (guestInApartmentPrefabCount < unlockedApartmentCount)
        {
            
            for (int i = 0; i < guestInApartmentPrefabCount; i++)
            {
                GuestRandomMoveIntoApartment(randomGuestTagList[i], randomApartmentTagList[i]);
            }
        }
        else
        {
            for (int i = 0; i < unlockedApartmentCount; i++)
            {
                GuestRandomMoveIntoApartment(randomGuestTagList[i], randomApartmentTagList[i]);
            }
        }
        

    }

    List<int> GenerateRandomGuestTagList()
    {
        HashSet<int> randomGuestHashList = new HashSet<int>();
        while (randomGuestHashList.Count < guestInApartmentPrefabCount)
        {
            int id = Random.Range(0, guestInApartmentPrefabCount);
            randomGuestHashList.Add(id);
        }
        List<int> randomGuestList = new List<int>(randomGuestHashList);
        return randomGuestList;        
    }

    List<int> GenerateRandomApartmentTagList()
    {
        HashSet<int> randomApartmentHashList = new HashSet<int>();
        while (randomApartmentHashList.Count < unlockedApartmentCount)
        {
            int id = Random.Range(0, unlockedApartmentCount);
            randomApartmentHashList.Add(id);
        }
        List<int> randomApartmentTagList = new List<int>(randomApartmentHashList);
        return randomApartmentTagList;
    }

    void GuestRandomMoveIntoApartment(int guestListTag,int apartmentListTag)
    {
        
        GuestController.Instance.GuestInApartmentPrefabStorage[guestListTag].transform.SetParent(ApartmentController.Instance.apartment[apartmentListTag].transform);
        GuestController.Instance.GuestInApartmentPrefabStorage[guestListTag].transform.localPosition = new Vector3(0, 0, -0.1f);
        GuestController.Instance.GuestInApartmentPrefabStorage[guestListTag].GetComponentInChildren<SpriteRenderer>().enabled = true;
        GuestController.Instance.GuestInApartmentPrefabStorage[guestListTag].GetComponentInChildren<BoxCollider>().enabled = true;
        //GuestController.Instance.GuestInApartmentPrefabStorage[guestListTag].GetComponentInChildren<TextMeshPro>().enabled = true;
    }

    public void ShowStoragePanel()
    {
        storagePanel.SetActive(true);
    }

    public void StartInstantiateMenu()
    {
        StartCoroutine(InstantiateMenu());
    }
    public IEnumerator InstantiateMenu()
    {
        yield return new WaitUntil(()=>ApartmentController.Instance.guestCount == ApartmentController.Instance.coinMovedCount);
        //UpdateVaultMoneyText();
        yield return new WaitForSecondsRealtime(0.4f);
        if (Level.GetItem(GameManager.Instance.levelKey).days - GameManager.Instance.gameDays > 0)
        {
            Instantiate(chooseCardPanel, thisCanvas.transform);
        }
        else
        {
            GameManager.Instance.isRoundEnd = true;
            Instantiate(roundEndPanel, thisCanvas.transform);
        }
    }

    //public void UpdateVaultMoneyText()
    //{
    //    vaultMoneyText.text = ApartmentController.Instance.vaultMoney.ToString();
    //}

    


}
