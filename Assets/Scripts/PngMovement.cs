using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PngMovement : MonoBehaviour
{

    public Rigidbody2D rb;
    public bool canMove = true;
    public bool moving = false;
    string[] valuesXY = new string[2] { "x", "y" };
    float[] sign = new float[2] { -1f, 1f };
    public string randomAxis;
    public float randomSign;
    public float movementSign;
    public int pathListCursor = 0;
    public bool movementPaused = false;
    public Animator anim;

    float pngLowerBound;
    float pngUpperBound;
    float pngLeftBound;
    float pngRightBound;
    float pngHeight;
    float pngWidth;


    [SerializeField]
    Vector2[] pathPoints;
    [SerializeField]
    bool randomWalkX;
    [SerializeField]
    bool randomWalkY;
    [SerializeField]
    float stepInterval;
    [SerializeField]
    float pauseInterval;
    [SerializeField]
    float stepCoverDistance;
    [SerializeField]
    bool[] setWalkingLimits = new bool[4];
    [SerializeField]
    float xUpperLimit;
    [SerializeField]
    float xLowerLimit;
    [SerializeField]
    float yUpperLimit;
    [SerializeField]
    float yLowerLimit;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (randomWalkX || randomWalkY) StartCoroutine("RandomWalk");
        else
        {
            StartCoroutine("PausePathWalking");
        }
        pngLowerBound = GetComponent<SpriteRenderer>().bounds.min.y;
        pngUpperBound = GetComponent<SpriteRenderer>().bounds.max.y;
        pngLeftBound = GetComponent<SpriteRenderer>().bounds.min.x;
        pngRightBound = GetComponent<SpriteRenderer>().bounds.max.x;
        pngHeight = System.Math.Abs(pngUpperBound) - System.Math.Abs(pngLowerBound);
        pngWidth = System.Math.Abs(pngRightBound) - System.Math.Abs(pngLeftBound);
        if(pathPoints.Length > 0)
        {
            moving = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!randomWalkX && !randomWalkY)
        {
            StartCoroutine("PausePathWalking");
        }
        if ((randomWalkX || randomWalkY) && canMove && moving)
        {
            // Esegue il random walk
            if (randomAxis == "x" && randomWalkX && CheckLimits(randomAxis))
            {
                Vector3 vector = new Vector3(randomSign, 0f) * stepCoverDistance * Time.deltaTime;
                transform.Translate(vector, Space.World);
                anim.SetFloat("MoveX", randomSign);
                anim.SetFloat("MoveY", 0f);
                anim.SetFloat("LastMoveX", randomSign);
                anim.SetFloat("LastMoveY", 0f);
            }
            else if (randomAxis == "y" && randomWalkY && CheckLimits(randomAxis))
            {
                Vector3 vector = new Vector3(0f, randomSign) * stepCoverDistance * Time.deltaTime;
                transform.Translate(vector, Space.World);
                anim.SetFloat("MoveX", 0f);
                anim.SetFloat("MoveY", randomSign);
                anim.SetFloat("LastMoveX", 0f);
                anim.SetFloat("LastMoveY", randomSign);
            }
            
            
        }
        else if( canMove & pathPoints.Length >= 2 && !randomWalkX && !randomWalkY)
        {
            // Il PNG si muove verso i punti configurati in modo dal primo all'ultimo, quindi riparte
            if (moving)
            {
                // Gestione del movimento
                transform.position = Vector2.MoveTowards(transform.position, pathPoints[pathListCursor], 1f * Time.deltaTime);
                float xMove = 0, yMove = 0;
                if (transform.position.y == pathPoints[pathListCursor].y)
                {
                    //i due punti sono allineati sulla y
                    if (transform.position.x > pathPoints[pathListCursor].x) movementSign = -1;
                    else movementSign = 1;
                    xMove = movementSign;
                    yMove = 0f;
                    anim.SetFloat("MoveX", movementSign);
                    anim.SetFloat("MoveY", 0f);
                }
                else if (transform.position.x == pathPoints[pathListCursor].x)
                {
                    //i due punti sono allineati sulla x
                    if (transform.position.y > pathPoints[pathListCursor].y) movementSign = -1;
                    else movementSign = 1;
                    xMove = 0f;
                    yMove = movementSign;
                    anim.SetFloat("MoveX", 0f);
                    anim.SetFloat("MoveY", movementSign);

                }
                anim.SetFloat("LastMoveX", xMove);
                anim.SetFloat("LastMoveY", yMove);
            }
            else
            {
                // gestione della pausa
                anim.SetFloat("MoveX", 0f);
                anim.SetFloat("MoveY", 0f);
            }
            
            // aggiornamento del punto di destinazione: se l'ho raggiunto passo al successivo
            if (Vector2.Distance(transform.position, pathPoints[pathListCursor]) < 0.00001)
            {
                pathListCursor++;
                if (pathListCursor == pathPoints.Length) pathListCursor = 0;
                
            }
        }
        // controllo di movimento se è attiva o non è attiva la dialogue box
        if (ConversationManager.instance.currentDialogue != null)
        {
            canMove = true;
        }
    }

    IEnumerator RandomWalk()
    {
        while (true)
        {
            randomAxis = valuesXY[Random.Range(0, 2)];
            if(randomWalkX && !randomWalkY)
            {
                randomAxis = "x";
            }
            else if(randomWalkY && !randomWalkX)
            {
                randomAxis = "y";
            }
            randomSign = sign[Random.Range(0, 2)];
            yield return new WaitForSeconds(stepInterval);
            moving = false;
            anim.SetFloat("MoveX", 0f);
            anim.SetFloat("MoveY", 0f);
            yield return new WaitForSeconds(pauseInterval);
            moving = true;
        }
    }

    bool CheckLimits(string axis)
    {
        // controlla il superamento dei limiti imposti per il random walk
        if (axis == "x" && setWalkingLimits[0] &&  transform.position.x <= xLowerLimit && randomSign < 0) return false;
        else if (axis == "x" && setWalkingLimits[1] && transform.position.x >= xUpperLimit && randomSign > 0) return false;
        else if (axis == "y" && setWalkingLimits[2] && transform.position.y <= yLowerLimit && randomSign < 0) return false;
        else if (axis == "y" && setWalkingLimits[3] && transform.position.y >= yUpperLimit && randomSign > 0) return false;
        else return true;
    }

    IEnumerator PausePathWalking()
    {
        if (pauseInterval == 0)
        {
           
            yield return null;
        }
        else
        {
            if (!movementPaused)
            {
                movementPaused = true;
                yield return new WaitForSeconds(pauseInterval);
                moving = !moving;
                movementPaused = false;
            }
        }
       
    }

    private void OnMouseDown()
    {
        if (PlayerInteraction.instance.mouseInDialogueArea && PlayerInteraction.instance.activeCursor == "dialoguebubble" && GetComponent<Dialogue>() != null)
        {
            if (canMove)
            {
                canMove = false;
                anim.SetFloat("MoveX", 0f);
                anim.SetFloat("MoveY", 0f);
            }
        }
    }

}
