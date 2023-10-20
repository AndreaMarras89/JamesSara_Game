using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUi : MonoBehaviour
{
    public Button newGame, load, options, credits, exit;
    public GameObject loadButton;
    public string newGameScene, loadingScene;
    void Start()
    {
        if (PlayerPrefs.HasKey("CurrentScene"))
        {
            loadButton.SetActive(true);
        }else
        {
            loadButton.SetActive(false);
        }
    }

    
    void Update()
    {
        
    }

    public void NewGame()
    {
        SceneManager.LoadScene(newGameScene);
    }

    public void LoadData()
    {
        SceneManager.LoadScene(loadingScene);
    }
    
    public void Options()
    {

    }

    public void Credits()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }
}
