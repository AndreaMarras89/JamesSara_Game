using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EssentialLoader : MonoBehaviour
{
    public GameObject UIScreen;
    public GameObject player;
    public GameObject gameMan;
    public GameObject audioMan;
    public GameObject dialogMan;
    public EventSystem eventSystem;
    void Start()
    {
        CameraController.instance = GameObject.Find("Main Camera").GetComponent<CameraController>();
        Debug.Log(CameraController.instance.theMap);
        if (UiFade.instance == null)
        {
            UiFade.instance = Instantiate(UIScreen).GetComponent<UiFade>();
        }
        if (GameManager.instance == null)
        {
            GameManager.instance = Instantiate(gameMan).GetComponent<GameManager>();
        }
        if(ConversationManager.instance == null)
        {
            ConversationManager.instance = Instantiate(dialogMan).GetComponent<ConversationManager>();
        }
        if(EventSystem.current == null)
        {
            EventSystem.Instantiate(eventSystem); 
        }
        if(GameObject.Find("Development"))
        {
            if (PlayerMovement.instance == null)
            {
                PlayerMovement.instance = Instantiate(player).GetComponent<PlayerMovement>();
            }
        }else
        {
            if(GameObject.FindGameObjectWithTag("James") != null)
            {
                PlayerMovement.instance = GameObject.FindGameObjectWithTag("James").GetComponent<PlayerMovement>();
            }
            else if(GameObject.FindGameObjectWithTag("Sara") != null)
            {
                PlayerMovement.instance = GameObject.FindGameObjectWithTag("Sara").GetComponent<PlayerMovement>();
            }
            
        }
    }

    void Update()
    {
        
    }
}
