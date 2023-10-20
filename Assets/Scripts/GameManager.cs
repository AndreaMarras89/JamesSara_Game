using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isJames, isSara;

    public bool gameMenuOpen, fadingBetweenAreas;//quest'ultimo si trova negli entry/exit point.
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        if(PlayerMovement.instance.tag == "James")
        {
            isJames = true;
            isSara = false;
        }else if(PlayerMovement.instance.tag == "Sara")
        {
            isJames = false;
            isSara = true;
        }
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M) && !gameMenuOpen && !UiFade.instance.dialogBoxOpen)
        {
            gameMenuOpen = true;
            UiFade.instance.menu.SetActive(true);
            PlayerMovement.instance.canMove = false;
        }
        else if (Input.GetKeyDown(KeyCode.M) && gameMenuOpen || UiFade.instance.dialogBoxOpen)
        {
            gameMenuOpen = false;
            UiFade.instance.menu.SetActive(false);
            PlayerMovement.instance.canMove = true;
        }
        if(gameMenuOpen || fadingBetweenAreas)
        {
            PlayerMovement.instance.canMove = false;
        }else
        {
            PlayerMovement.instance.canMove = true;
        }
       
    }
    public void SaveData()
    {
        PlayerPrefs.SetString("CurrentScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("Player_Position_x", PlayerMovement.instance.transform.position.x);
        PlayerPrefs.SetFloat("Player_Position_y", PlayerMovement.instance.transform.position.y);
        PlayerPrefs.SetFloat("Player_Position_z", PlayerMovement.instance.transform.position.z);
        PlayerPrefs.SetFloat("Camera_Position_x", CameraController.instance.transform.position.x);
        PlayerPrefs.SetFloat("Camera_Position_y", CameraController.instance.transform.position.y);
        PlayerPrefs.SetFloat("Camera_Position_z", CameraController.instance.transform.position.z);

    }
    public void LoadData()
    {
        //SceneManager.LoadScene(PlayerPrefs.GetString("CurrentScene"));
        PlayerMovement.instance.transform.position = new Vector3(PlayerPrefs.GetFloat("Player_Position_x"), PlayerPrefs.GetFloat("Player_Position_y"), PlayerPrefs.GetFloat("Player_Position_z"));
        CameraController.instance.transform.position = new Vector3(PlayerPrefs.GetFloat("Camera_Position_x"), PlayerPrefs.GetFloat("Camera_Position_y"), PlayerPrefs.GetFloat("Camera_Position_z"));
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
