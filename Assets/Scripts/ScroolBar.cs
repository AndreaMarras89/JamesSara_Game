using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScroolBar : MonoBehaviour
{
    public Scrollbar scroolBar;
    public GameObject evidenceUI, evidenceRect;
    public float speed = 5f;
    void Start()
    {
        evidenceRect = evidenceUI.GetComponent<RectTransform>().gameObject;
    }

    
    void Update()
    {
        
    }

    public void Scroll()
    {
        if (Input.GetMouseButton(0))
        {
            //scroolBar.GetComponent<Scrollbar>().value = Mathf.MoveTowards(0, 1f, 0.2f * Time.deltaTime);
            evidenceRect.gameObject.transform.position = new Vector3(evidenceUI.transform.position.x, evidenceUI.transform.position.y * speed * Time.deltaTime, evidenceUI.transform.position.z);
        }

    }
}
