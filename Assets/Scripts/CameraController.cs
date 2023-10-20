using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Transform target;
    public Tilemap theMap;//riferimento alla TileMap che utilizziamo per creare i limiti di quanto si muoverà la camera.

    //i limiti della mappa.
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    
    private float halfHeight;
    private float halfWidth;

    public int musicToPlay;
    private bool musicStarted;

    void Start()
    {
        instance = this;
        target = PlayerMovement.instance.transform;
        //target = FindObjectOfType<PlayerMovement>().transform;

        //ortographicSize indica qual è l'attuale altezza della main Camera presente in scena.
        halfHeight = Camera.main.orthographicSize - 2.5f;
        halfWidth = halfHeight * Camera.main.aspect - 2.5f;

        theMap.CompressBounds();
        
        bottomLeftLimit = theMap.localBounds.min + new Vector3(halfWidth, halfHeight, 0f);
        topRightLimit = theMap.localBounds.max + new Vector3(-halfWidth, -halfHeight, 0f);

        PlayerMovement.instance.SetBounds(theMap.localBounds.min, theMap.localBounds.max);
    }


    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        //keep the camera inside the bounds
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, bottomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, bottomLeftLimit.y, topRightLimit.y), transform.position.z);
        //mantieni la posizione la tua posizione "transform.position.x" tra bottomLeftLimit.x e topRightLimit.x (punto minimo, punto massimo), stessa cosa per la Y e mantieni la stessa posizione per la z.

        if (!musicStarted)
        {
            musicStarted = true;
            //AudioManager.instance.PlayBGM(musicToPlay);
        }
    }
}
