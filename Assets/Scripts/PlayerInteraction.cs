using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    public static PlayerInteraction instance;
    public Interactible objects;

    public Vector2 playerPosition;
    public Vector2 relativeMousePosition;
    public bool mouseInSearchArea = false;
    public bool mouseInPickupArea = false;
    public bool mouseInDialogueArea = false;
    public bool weCanPickUp, isPickingUp;
    public bool objectChecked, weCheckTheFirstTime;
    float searchAreaBoundaryOffset = 3f; //4f
    float pickupAreaBoundaryOffset = 1.5f;//1.5f
    float dialogueAreaBoundaryOffset = 2f;

    public Collider2D previousHit;
    public string activeCursor;
    public GameObject mouseTarget;
    public float playerUpperBound;
    public float playerLowerBound;
    public float playerLeftBound;
    public float playerRightBound;
    public float playerHeight;
    public float playerWidth;

    [SerializeField]
    public Texture2D defaultCursor;
    [SerializeField]
    public Texture2D lente;
    [SerializeField]
    public Texture2D lenteSecondaVolta;
    [SerializeField]
    public Texture2D pickUpHand;
    [SerializeField]
    public Texture2D dialogueBubble;
    
    void Start()
    {
            if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        

        playerPosition = this.gameObject.transform.position;
        Cursor.SetCursor(defaultCursor, Vector3.zero, CursorMode.ForceSoftware);
        activeCursor = "default";
        playerUpperBound = GetComponent<SpriteRenderer>().bounds.min.y;
        playerLowerBound = GetComponent<SpriteRenderer>().bounds.max.y;
        playerRightBound = GetComponent<SpriteRenderer>().bounds.max.x;
        playerLeftBound = GetComponent<SpriteRenderer>().bounds.min.x;
        playerHeight = System.Math.Abs(playerUpperBound) - System.Math.Abs(playerLowerBound);
        playerWidth = System.Math.Abs(playerRightBound) - System.Math.Abs(playerLeftBound);
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = this.gameObject.transform.position;
        relativeMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (relativeMousePosition.x < (playerPosition.x + searchAreaBoundaryOffset) && relativeMousePosition.x > (playerPosition.x - searchAreaBoundaryOffset) &&
            relativeMousePosition.y < (playerPosition.y + searchAreaBoundaryOffset) && relativeMousePosition.y > (playerPosition.y - searchAreaBoundaryOffset))
        {
            mouseInSearchArea = true;
        }
        else
        {
            mouseInSearchArea = false;
        }

        if (relativeMousePosition.x < (playerPosition.x + pickupAreaBoundaryOffset) && relativeMousePosition.x > (playerPosition.x - pickupAreaBoundaryOffset) &&
            relativeMousePosition.y < (playerPosition.y + pickupAreaBoundaryOffset) && relativeMousePosition.y > (playerPosition.y - pickupAreaBoundaryOffset))
        {
            mouseInPickupArea = true;
        }
        else
        {
            mouseInPickupArea = false;
        }

        if (relativeMousePosition.x < (playerPosition.x + dialogueAreaBoundaryOffset) && relativeMousePosition.x > (playerPosition.x - dialogueAreaBoundaryOffset) &&
            relativeMousePosition.y < (playerPosition.y + dialogueAreaBoundaryOffset) && relativeMousePosition.y > (playerPosition.y - dialogueAreaBoundaryOffset))
        {
            mouseInDialogueArea = true;
        }
        else
        {
            mouseInDialogueArea = false;
        }

        SetMouseCursor();
        
        if(Input.GetMouseButtonDown(0) && weCanPickUp)
        {
            StartCoroutine("CanMove");
            if (GameManager.instance.isSara)
                {
                    StartCoroutine("SaraPickingUp");
                }
                else if (GameManager.instance.isJames)
                {
                    StartCoroutine("JamesPickingUp");
                }
        }

    }

    void SetMouseCursor()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null && hit.collider.tag == "interactible" && objects != null && !UiFade.instance.dialogBoxOpen && !UiFade.instance.evidenceUiOpen && !GameManager.instance.gameMenuOpen)
        {
            if (mouseInSearchArea && !mouseInPickupArea)
            { 
                objects.OnMouseOver();
            }
            else if (mouseInPickupArea)
            {
                if ((previousHit != null && hit.collider.tag != previousHit.tag) || (previousHit == null) || activeCursor == "default" || activeCursor == "dialogueBubble" || activeCursor == "pickuphand" || activeCursor == "magnifyingAgain")
                {
                        if (objects.scanned && objects.allowedToPick)
                        {
                            Debug.Log("Sono in pickup");
                            Cursor.SetCursor(pickUpHand, Vector3.zero, CursorMode.ForceSoftware);
                            activeCursor = "pickuphand";
                            //weCanPickUp = true;
                        }
                        else if (objects.scanned && !objects.allowedToPick)
                        {
                            Debug.Log("Sono in scanned");
                            Cursor.SetCursor(lenteSecondaVolta, Vector3.zero, CursorMode.ForceSoftware);
                            activeCursor = "magnifyingAgain";
                            weCanPickUp = false;
                        }
                }

            }

        }
        else if (hit.collider != null && hit.collider.tag == "character_interactible" && objects != null && objects.isACharacter && !UiFade.instance.dialogBoxOpen && !UiFade.instance.evidenceUiOpen && !GameManager.instance.gameMenuOpen)
        {
            if (mouseInDialogueArea)
            {
                if ((previousHit != null && hit.collider.tag != previousHit.tag) || (previousHit == null) || activeCursor == "default" || activeCursor == "magnifying" || activeCursor == "pickuphand" || activeCursor == "magnifyingAgain")
                {
                   Cursor.SetCursor(dialogueBubble, Vector3.zero, CursorMode.ForceSoftware);
                    activeCursor = "dialoguebubble";
                    weCanPickUp = false;
                }

            }
        }
        else
        {
            if ((previousHit != null && hit.collider == null) || (previousHit != null && hit.collider.tag != previousHit.tag))
            {
             objects = null;
             Cursor.SetCursor(defaultCursor, Vector3.zero, CursorMode.ForceSoftware);
             activeCursor = "default";
             weCanPickUp = false;
                
            }

        }
        previousHit = hit.collider;
    }


    //Attivare animazione Collecting//
    IEnumerator SaraPickingUp()
    {
        PlayerMovement.instance.anim.SetBool("isPickingUp", true);
        yield return new WaitForSeconds(0.5f);
        PlayerMovement.instance.anim.SetBool("isPickingUp", false);
    }

    IEnumerator JamesPickingUp()
    {
     PlayerMovement.instance.anim.SetBool("IsCollecting", true);
     yield return new WaitForSeconds(0.5f);
     PlayerMovement.instance.anim.SetBool("IsCollecting", false);
    }

    IEnumerator CanMove()
    {
        isPickingUp = true;
        yield return new WaitForSeconds(1f);
        isPickingUp = false;
    }


}
