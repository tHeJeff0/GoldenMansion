using ExcelData;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundEndPanelController : MonoBehaviour
{
    //[SerializeField] private GameObject quitBuildButton;
    GameObject commitRentGroup;
    //GameObject buildButton;
    GameObject nextLevelButton;

    private void Awake()
    {
        commitRentGroup = GameObject.Find("CommitRentGroup");
        //buildButton = GameObject.Find("BuildButton");
        nextLevelButton = GameObject.Find("NextLevelButton");

        commitRentGroup.SetActive(true);
        //buildButton.SetActive(false);
        nextLevelButton.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void StartBuildMode()
    //{
    //    ApartmentController.Instance.isBuildMode = true;
    //    foreach (Transform child in transform)
    //    { 
    //        child.gameObject.SetActive(false);
    //    }
    //    quitBuildButton.SetActive(true);

    //}

    //public void QuitBuildMode()
    //{
    //    ApartmentController.Instance.isBuildMode = false;
    //    foreach (Transform child in transform)
    //    {
            
    //        child.gameObject.SetActive(true);
    //    }
    //    quitBuildButton.SetActive(false);
    //}

    public void GoToNextLevel()
    {
        GameManager.Instance.isRoundEnd = false;
        Destroy(this.gameObject);
        GameManager.Instance.levelKey += 1;
    }

    public void CommitRent()
    {
        float vaultMoney = ApartmentController.Instance.vaultMoney;
        float targetMoney = Level.GetItem(GameManager.Instance.levelKey).target;
        float moneyLeft = vaultMoney - targetMoney;
        if (moneyLeft >= 0)
        {
            //buildButton.SetActive(true);
            nextLevelButton.SetActive(true);
            commitRentGroup.SetActive(false);
        }
        else
        {
            GameManager.Instance.isRoundEnd = false;
            Destroy(this.gameObject);
            commitRentGroup.SetActive(false);
            GameReset();
            SceneManager.LoadScene("StartScene", LoadSceneMode.Additive);       
            //SceneManager.UnloadSceneAsync("GameScene",UnloadSceneOptions.None);
            //SceneManager.UnloadSceneAsync("UIScene", UnloadSceneOptions.None);       
            Debug.Log("Game Over!");
        }
    }

    private void GameReset()
    {
        GameManager.Instance.gameDays = 0;
        GameManager.Instance.levelKey = 1;
        GameManager.Instance.mediaDays = -1;       
        foreach (var apartment in ApartmentController.Instance.apartment)
        {
            apartment.GetComponent<Apartment>().apartmentDays = 0;
        }
        foreach (var guestInApartment in GuestController.Instance.GuestInApartmentPrefabStorage)
        {
            Destroy(guestInApartment);
        }
        GuestController.Instance.GuestInApartmentPrefabStorage.Clear();
        GuestController.Instance.GenerateBasicGuest(3);
    }
}
