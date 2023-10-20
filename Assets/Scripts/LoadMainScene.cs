using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadMainScene : MonoBehaviour
{
    public static LoadMainScene instance;
    public string jamesScene, saraScene;
    public Image fadeScreen;
    
    [SerializeField]
    public float fadeSpeed;
    public float waitToLoad = 1f;

    [SerializeField]
    public bool shouldFadeToBlack;
    public bool shouldFadeFromBlack;
    //public bool shouldLoadAfterFade;

    void Start()
    {
        instance = this;
    }

    
    void Update()
    {
        if (AnimateJ.instance.JamesIsChosen)//Se abbiamo cliccato su uno dei due pulsanti di James.
        {
                fadeScreen.gameObject.SetActive(true);//attiviamo l'immagine della canvas FadeImage
                FadeToBlack();//chiamiamo FadeToBlack.
                waitToLoad -= Time.deltaTime;//diminuiamo il valore di waitToLoad
                if(waitToLoad < 0)//se questo è minore di 0.
                {
                    //shouldLoadAfterFade = false;//shouldLoadAfterFade diventa falso.
                    SceneManager.LoadScene(jamesScene);//Carichiamo la scena.
                }
        }
        if (AnimateS.instance.SaraIsChosen)//Stessa cosa per Sara.
        {
                fadeScreen.gameObject.SetActive(true);
                FadeToBlack();
                waitToLoad -= Time.deltaTime;
                if (waitToLoad < 0)
                {
                    //shouldLoadAfterFade = false;
                    SceneManager.LoadScene(saraScene);
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
}
