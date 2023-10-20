using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeProva : MonoBehaviour
{
    GameObject target;
    SpriteRenderer srColor;
    public bool touchingTheTarget;
    void Start()
    {
        target = PlayerMovement.instance.gameObject;
        srColor = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {
        if(touchingTheTarget)
        {
            srColor.color = new Color(srColor.color.r, srColor.color.g, srColor.color.b, .5f);
        }else
        {
            srColor.color = new Color(srColor.color.r, srColor.color.g, srColor.color.b, 1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "James" || other.gameObject.tag == "Sara")
        {
            touchingTheTarget = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "James" || other.gameObject.tag == "Sara")
        {
            touchingTheTarget = false;
        }
    }
}
