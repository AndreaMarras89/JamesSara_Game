using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimateS : MonoBehaviour
{
    public static AnimateS instance;
    public Animator anim;
    public bool SaraIsChosen;
    void Start()
    {
        instance = this;
        anim = GetComponent<Animator>();
    }

    public void Animate()
    {
        anim.SetBool("CanShoot", true);
    }

    public void DontAnimate()
    {
        anim.SetBool("CanShoot", false);
    }

    public void ChooseSara()
    {
        SaraIsChosen = true;
    }
}
