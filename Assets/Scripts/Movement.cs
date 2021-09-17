using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 !!! Ground Check Field in Inspector Should be the Parent Player Object
 */

public class Movement : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;

    public float speed;
    public float runningSpeed;
    public float jumpForce;
    public float jumpMultiplier;

    public bool facingRight = true;

    [SerializeField] public Transform wallCheck;
    

    private bool canGrab, isGrabbing;

}

    public Transform groundCheck;
    public LayerMask groundLayer;

    
    private BoxCollider2D boxCollider2d;

    //Non-Serialized Variables
    private float moveInput;
    private float horizontalValue;
    private float xVal;

    private bool isGrounded;
    private bool isRunning;

    //private Animator animatorzxc;
    
    Rigidbody2D rb;

    void Start()
    {
        //animatorzxc = gameObject.GetComponent<Animator>();
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (CanMove() == false)
        {
            horizontalValue = 0f;
            //animatorzxc.SetBool("isWalking", false);
            return;
        }
        else if (CanMove() == true)
        {
            horizontalValue = Input.GetAxisRaw("Horizontal");
            //animatorzxc.SetBool("isWalking", false);

            //Jump
            jumpMultiplier = 2.0f;
            jumpForce = 5f * jumpMultiplier;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (GameObject.Find("Player").GetComponent<GroundChecker>().isGrounded)
                {   
                    Jump();
                }    
            }   

            //Walk
            if (horizontalValue != 0 && !Input.GetKey(KeyCode.LeftShift))
            {
                //animatorzxc.SetBool("isWalking", true);
                Walk();
                isRunning = false;
            }

            //Sprint
            else
            {
                runningSpeed = speed * 2f;
                Sprint();
                //animatorzxc.SetBool("isRunning", true);
                //animatorzxc.SetBool("isWalking", false);
            }


        }

        //Wall Jump
        canGrab = Physics2D.OverlapCircle(wallCheck.position, 0.2f, groundLayer);

        isGrabbing == false;

        if(canGrab && !isGrounded)
        {
            if((transform.localScale.x == 1f && Input.SetAxisRaw("Horizontal") > 0) || (transform.localScale.x == -1f && Input.SetAxisRaw("Horizontal") < 0))
            {
                isGrabbing == true;
            }
            
        }


        if(isGrabbing)
        {
            rb.gravityScale = 0f;
        }
    }


    bool CanMove()
    {

        bool can = true;
        return can;
    }

    void FixedUpdate()
    {
        Move(horizontalValue);
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer); 
    }

    void Move(float dir)
    {
        if(!isRunning)
        {  
            xVal = dir * speed;
        }
        else
        {
            xVal = dir * runningSpeed;
        }
        Vector2 targetVelocity = new Vector2(xVal, rb.velocity.y);
        rb.velocity = targetVelocity;

        //Store Current Scale Value
        Vector3 currentScale = transform.localScale;

        //Flip Left
        if (facingRight && dir < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingRight = false;
        }

        //Flip Right
        else if (!facingRight && dir > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingRight = true;
        }

    }

    void Walk()
    {
        //Debug.Log(horizontalValue);
        
        rb.velocity = new Vector2(horizontalValue * speed, rb.velocity.y);
    }
    
    void Sprint()
    {
        isRunning = true;
        //animatorzxc.SetBool("isFastasFuckBoi", true);
        rb.velocity = new Vector2(horizontalValue * runningSpeed, rb.velocity.y);
    }

    void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
    }
}

