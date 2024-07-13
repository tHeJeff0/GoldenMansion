using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundEndPanelController : MonoBehaviour
{
    [SerializeField] private GameObject quitBuildButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBuildMode()
    {
        ApartmentController.Instance.isBuildMode = true;
        foreach (Transform child in transform)
        { 
            child.gameObject.SetActive(false);
        }
        quitBuildButton.SetActive(true);

    }

    public void QuitBuildMode()
    {
        ApartmentController.Instance.isBuildMode = false;
        foreach (Transform child in transform)
        {
            
            child.gameObject.SetActive(true);
        }
        quitBuildButton.SetActive(false);
    }

    public void GoToNextLevel()
    {
        Destroy(this.gameObject);
        GameManager.Instance.levelKey += 1;
    }
}
