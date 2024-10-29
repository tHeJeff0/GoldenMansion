using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    private void Awake()
    {
        #if !UNITY_EDITOR
        SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("Camera", LoadSceneMode.Additive);
        SceneManager.LoadSceneAsync("UIScene", LoadSceneMode.Additive);      
        #endif
          
        Debug.Log(GameManager.Instance.gameDays);

    }
    public void StartNewGame()
    {
        SaveSystem.Instance.DeleteData(SaveSystem.Instance.saveName);
        SceneManager.UnloadSceneAsync("StartScene");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameScene"));
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("UIScene"));

    }

    public void ContinueGame()
    {
        for (int i = GuestController.Instance.GuestInApartmentPrefabStorage.Count-1; i >= 0; i--)
        {
            Destroy(GuestController.Instance.GuestInApartmentPrefabStorage[i]);
        }
        GuestController.Instance.GuestInApartmentPrefabStorage.Clear();
        StorageController.Instance.guestStorage.Clear();
        SceneManager.UnloadSceneAsync("StartScene");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameScene"));
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("UIScene"));
        var data = SaveSystem.Instance.LoadData<SaveData>(SaveSystem.Instance.saveName);
        SaveSystem.Instance.readData(data);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

}
