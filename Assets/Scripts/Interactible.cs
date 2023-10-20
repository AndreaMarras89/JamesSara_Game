using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    public float interactibleLowerBound;
    public float interactibleUpperBound;
    public float interactibleLeftBound;
    public float interactibleRightBound;
    public float interactibleHeight;
    public float interactibleWidth;

    public bool canWePickItUp, scanned, allowedToPick, isACharacter;
    public int clickCounter;

    public Animator anim;
    public float initialCharAnimX, initialCharAnimY;

    [SerializeField]
    bool facePGDuringInteraction;

    
    void Start()
    {
        interactibleLowerBound = GetComponent<SpriteRenderer>().bounds.min.y;
        interactibleUpperBound = GetComponent<SpriteRenderer>().bounds.max.y;
        interactibleLeftBound = GetComponent<SpriteRenderer>().bounds.min.x;
        interactibleRightBound = GetComponent<SpriteRenderer>().bounds.max.x;
        interactibleHeight = System.Math.Abs(interactibleUpperBound - interactibleLowerBound);
        interactibleWidth = System.Math.Abs(interactibleRightBound - interactibleLeftBound);
        if (facePGDuringInteraction)
        { 
            anim = GetComponent<Animator>();
            //Salvare la posizione di anim all'inizio in modo da poter ricaricare i valori quando chiudiamo la dialog box.
            initialCharAnimX = anim.GetFloat("LastMoveX");
            initialCharAnimY = anim.GetFloat("LatMoveY");
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseOver()//Quando passiamo il mouse sopra qualcosa.
    {
        if (PlayerInteraction.instance.mouseInSearchArea && !isACharacter && !canWePickItUp && !UiFade.instance.dialogBoxOpen && !UiFade.instance.evidenceUiOpen && !GameManager.instance.gameMenuOpen)
            //se siamo nella search area e siamo sopra un oggetto che non è personaggio.
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            Cursor.SetCursor(PlayerInteraction.instance.lente, Vector3.zero, CursorMode.ForceSoftware);
            //Debug.Log("mi illumino dimmerda");
            PlayerInteraction.instance.mouseTarget = this.gameObject;
            PlayerInteraction.instance.objects = this;
            if (clickCounter == 1)
            {
                scanned = true;
            }
        }else if(PlayerInteraction.instance.mouseInDialogueArea && isACharacter && !UiFade.instance.dialogBoxOpen && !UiFade.instance.evidenceUiOpen && !GameManager.instance.gameMenuOpen)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            Cursor.SetCursor(PlayerInteraction.instance.dialogueBubble, Vector3.zero, CursorMode.ForceSoftware);
            //Debug.Log("mi illumino dimmerda");
            PlayerInteraction.instance.mouseTarget = this.gameObject;
            PlayerInteraction.instance.objects = this;
        }else if(PlayerInteraction.instance.mouseInSearchArea && !isACharacter && canWePickItUp && !UiFade.instance.dialogBoxOpen && !UiFade.instance.evidenceUiOpen && !GameManager.instance.gameMenuOpen)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            Cursor.SetCursor(PlayerInteraction.instance.lente, Vector3.zero, CursorMode.ForceSoftware);
            //Debug.Log("mi illumino dimmerda");
            PlayerInteraction.instance.mouseTarget = this.gameObject;
            PlayerInteraction.instance.objects = this;
            if (clickCounter == 1)
            {
                scanned = true;
            }
        }
    }
    private void OnMouseExit()
    {
        if (PlayerInteraction.instance.mouseInSearchArea)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            PlayerInteraction.instance.mouseTarget = null;
        }
    }

    private void OnMouseDown()
    {
        if (PlayerInteraction.instance.mouseTarget != null && ((gameObject.tag == "interactible" && PlayerInteraction.instance.mouseInPickupArea) || (gameObject.tag == "character_interactible" && PlayerInteraction.instance.mouseInDialogueArea)))
        {
            if (gameObject.transform.position.y + interactibleHeight / 2 < (PlayerInteraction.instance.transform.position.y))
            {
                // se l'interactible è sotto il personaggio, il personaggio si volta verso giù
                PlayerMovement.instance.anim.SetFloat("LastMoveX", 0f);
                PlayerMovement.instance.anim.SetFloat("LastMoveY", -1f);
                if (facePGDuringInteraction)
                {
                    anim.SetFloat("LastMoveX", 0f);
                    anim.SetFloat("LastMoveY", 1f);
                }
            }
            else if (gameObject.transform.position.y - interactibleHeight / 2 > PlayerInteraction.instance.transform.position.y)
            {
                // se l'interactible è sopra il personaggio, il personaggio si volta verso su
                PlayerMovement.instance.anim.SetFloat("LastMoveX", 0f);
                PlayerMovement.instance.anim.SetFloat("LastMoveY", 1f);
                if (facePGDuringInteraction)
                {
                    anim.SetFloat("LastMoveX", 0f);
                    anim.SetFloat("LastMoveY", -1f);
                }
            }
            else if(gameObject.transform.position.y + interactibleHeight / 2 >= PlayerInteraction.instance.transform.position.y && gameObject.transform.position.y - interactibleHeight / 2 <= PlayerInteraction.instance.transform.position.y)
            {
                // altrimenti se è nella fascia di y in cui possono essere definiti allineati...
                if(gameObject.transform.position.x - interactibleWidth / 2 > PlayerInteraction.instance.transform.position.x)
                {
                    // ... se l'interactible è a destra del personaggio, il personaggio di volta verso sinistra
                    PlayerMovement.instance.anim.SetFloat("LastMoveY", 0f);
                    PlayerMovement.instance.anim.SetFloat("LastMoveX", 1f);
                    if (facePGDuringInteraction)
                    {
                        anim.SetFloat("LastMoveY", 0f);
                        anim.SetFloat("LastMoveX", -1f);
                    }
                }
                else if (gameObject.transform.position.x + interactibleWidth / 2 < PlayerInteraction.instance.transform.position.x)
                {
                    // ... se l'interactible è a destra del personaggio, il personaggio di volta verso sinistra
                    PlayerMovement.instance.anim.SetFloat("LastMoveY", 0f);
                    PlayerMovement.instance.anim.SetFloat("LastMoveX", -1f);
                    if (facePGDuringInteraction)
                    {
                        anim.SetFloat("LastMoveY", 0f);
                        anim.SetFloat("LastMoveX", 1f);
                    }
                }
            }

        }
        if(!isACharacter)
        {
            clickCounter++;
            if (clickCounter == 1 && !allowedToPick)
            {
                scanned = true;
            }
            else if (clickCounter == 1 && canWePickItUp)
            {
                scanned = false;
            }
            if (canWePickItUp && clickCounter == 1)
            {
                allowedToPick = true;
            }
            else if (!canWePickItUp && clickCounter > 1)
            {
                //scanned = true;
                allowedToPick = false;
            }else if(canWePickItUp && clickCounter == 2)
            {
                PlayerInteraction.instance.weCanPickUp = true;
            }
        }
        //Riportare il PNG alla posizione di partenza.
        if (UiFade.instance.dialogBoxOpen && ConversationManager.instance.dialogueCursor >= ConversationManager.instance.currentDialogue.Length)
        {
            anim.SetFloat("LastMoveX", initialCharAnimX);
            anim.SetFloat("LastMoveY", initialCharAnimY);
        }


    }

    
            
        
        
}

