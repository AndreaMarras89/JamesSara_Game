using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    public string transitionName;

    void Start()
    {
        if(transitionName == PlayerMovement.instance.areaTransitionName)
        {
            PlayerMovement.instance.transform.position = transform.position;
        }
        UiFade.instance.FadeFromBlack();
        GameManager.instance.fadingBetweenAreas = false;
    }

    void Update()
    {
        
    }
}
