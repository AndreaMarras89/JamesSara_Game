using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public Rigidbody2D rb;
    public float speed = 5f;
    public Animator anim;
    public string areaTransitionName;
    public bool canMove = true;

    //Camera Controller
    private Vector3 bottomLeftLimit;
    private Vector3 topRightLimit;

    private void Start()
    {
        canMove = true;
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

        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        if (canMove && !UiFade.instance.dialogBoxOpen && !PlayerInteraction.instance.isPickingUp)
        {
            if (Input.GetAxisRaw("Horizontal") != 0f)
            {
                rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), 0f) * speed;
            }
            if (Input.GetAxisRaw("Vertical") != 0f)
            {
                rb.velocity = new Vector2(0f, Input.GetAxisRaw("Vertical")) * speed;
            }
            if (Input.GetAxisRaw("Horizontal") == 0f && Input.GetAxisRaw("Vertical") == 0f)
            {
                rb.velocity = new Vector2(0f, 0f);
            }
            //rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed;
            anim.SetFloat("MoveX", rb.velocity.x);
            anim.SetFloat("MoveY", rb.velocity.y);
            if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
                //Se l'asse orizzontale è 1 e quindi ci stiamo muovendo a destra, se è -1 a sinistra, se 1 in verticale in alto e se -1 in verticale significa in basso.
            {
                anim.SetFloat("LastMoveX", Input.GetAxisRaw("Horizontal"));
                anim.SetFloat("LastMoveY", Input.GetAxisRaw("Vertical"));
            }

        } else
        {
            rb.velocity = new Vector2(0f, 0f);//se canMove è false la rb.velocity sarà portata a zero su entrambi gli assi.
            anim.SetFloat("MoveX", rb.velocity.x);//la stessa cosa varrà per le variabili per le animazioni. 
            anim.SetFloat("MoveY", rb.velocity.y);
        }
        
    }
        public void SetBounds(Vector3 botLeft, Vector3 topRight)
        {
            bottomLeftLimit = botLeft + new Vector3(.5f, 1f, 0f);
            topRightLimit = topRight + new Vector3(-.5f, -1f, 0f);
        }
}
