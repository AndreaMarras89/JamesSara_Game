using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(GameObject.Find("Sara_ffp2"));
        Destroy(GameObject.Find("Canvas(Clone)"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
