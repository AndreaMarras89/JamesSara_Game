using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitPoint : MonoBehaviour
{
    public string areaToLoad;
    public string areaTransitionName;
    //public EntryPoint theEntrance;

    public float waitToLoad = 1f;
    private bool shouldLoadAfterFade;
    void Start()
    {
        //theEntrance.transitionName = areaTransitionName; 
    }

    void Update()
    {
        if(shouldLoadAfterFade)
        {
            waitToLoad -= Time.deltaTime;
            if(waitToLoad < 0)
            {
                shouldLoadAfterFade = false;
                SceneManager.LoadScene(areaToLoad);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            shouldLoadAfterFade = true;
            GameManager.instance.fadingBetweenAreas = true;
            UiFade.instance.FadeToBlack();
            PlayerMovement.instance.areaTransitionName = areaTransitionName;
        }
    }
}
