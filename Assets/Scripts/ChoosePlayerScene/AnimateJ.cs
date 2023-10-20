using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimateJ : MonoBehaviour
{
    public static AnimateJ instance;
    public Animator anim;
    public bool JamesIsChosen;
    void Start()
    {
        instance = this;
        anim = GetComponent<Animator>();
    }

    public void Animate()
    {
        anim.SetBool("CanJamesShoot", true);
    }

    public void DontAnimate()
    {
        anim.SetBool("CanJamesShoot", false);
    }

    public void ChooseJames()
    {
        JamesIsChosen = true;
    }

}
