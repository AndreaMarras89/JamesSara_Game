using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoosePlayer : MonoBehaviour
{
    public static ChoosePlayer instance;
    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        OnMouseOver();
    }

    private void OnMouseOver()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if(hit.collider != null)
        {
            if (hit.collider.tag == "NGJames")
            {
                Debug.Log("Touching James");
                AnimateJ.instance.Animate();
                AnimateS.instance.DontAnimate();
            }
            else if (hit.collider.tag == "NGSara")
            {
                Debug.Log("Touching Sara");
                AnimateS.instance.Animate();
                AnimateJ.instance.DontAnimate();
            }
        }
    }

}
