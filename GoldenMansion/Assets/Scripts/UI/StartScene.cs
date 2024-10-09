using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.UnloadSceneAsync("StartScene");
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("GameScene"));
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("UIScene"));
        //SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
        //SceneManager.LoadSceneAsync("UIScene", LoadSceneMode.Additive);

    }

    public void QuitGame()
    {
        EditorApplication.isPlaying = false;
        Application.Quit();
    }

}
