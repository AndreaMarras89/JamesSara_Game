using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiFade : MonoBehaviour
{
    public static UiFade instance;

    public Image fadeScreen;
    public Button resume, load, save, exit;
    public Button[] buttonsEvidence;
    public Text nome, altroTest, corpo;

    public GameObject dialogBox;
    public GameObject jEvidenceUi, sEvidenceUi;
    public GameObject menu;
    //public GameObject topDeadZone, bottomDeadZone;


    public float fadeSpeed;

    public bool dialogBoxOpen;
    public bool evidenceUiOpen;
    public bool shouldFadeToBlack;
    public bool shouldFadeFromBlack;

    private void Start()
    {
        dialogBox.gameObject.SetActive(false);
        jEvidenceUi.gameObject.SetActive(false);
        sEvidenceUi.gameObject.SetActive(false);
        instance = this;
        DontDestroyOnLoad(gameObject);
        
    }

    private void Update()
    {
        if(GameManager.instance.isJames)
        {
            if (Input.GetKeyDown(KeyCode.Tab) && !evidenceUiOpen)
            {
                evidenceUiOpen = true;
                jEvidenceUi.gameObject.SetActive(true);

            }
            else if (Input.GetKeyDown(KeyCode.Tab) && evidenceUiOpen)
            {
                evidenceUiOpen = false;
                jEvidenceUi.gameObject.SetActive(false);
            }
        }
        if(GameManager.instance.isSara)
        {
            if (Input.GetKeyDown(KeyCode.Tab) && !evidenceUiOpen)
            {
                evidenceUiOpen = true;
                sEvidenceUi.gameObject.SetActive(true);

            }
            else if (Input.GetKeyDown(KeyCode.Tab) && evidenceUiOpen)
            {
                evidenceUiOpen = false;
                sEvidenceUi.gameObject.SetActive(false);
            }
        }
        

        if (shouldFadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 1f)
            {
                shouldFadeToBlack = false;
            }
        }

        if (shouldFadeFromBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (fadeScreen.color.a == 0f)
            {
                shouldFadeFromBlack = false;
            }
        }
        
    }

    public void FadeToBlack()
    {
        shouldFadeToBlack = true;
        shouldFadeFromBlack = false;
    }

    public void FadeFromBlack()
    {
        shouldFadeToBlack = false;
        shouldFadeFromBlack = true;
    }

    public void OpenDialogBox()
    {
        dialogBox.SetActive(true);
        dialogBoxOpen = true;
    }
    public void CloseDialogBox()
    {
        dialogBox.SetActive(false);
        dialogBoxOpen = false;
    }

    public void Resume()
    {
        menu.SetActive(false);
        GameManager.instance.gameMenuOpen = false;
        PlayerMovement.instance.canMove = true;
    }


}
