using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class Movement : MonoBehaviour
{
    PhotonView view;
    //Serialized Variables
    [SerializeField] private LayerMask platformLayerMask;
    
    public float speed;
    public float runningSpeed;
    public float jumpForce;
    public float jumpMultiplier;
    public float wallJumpTime = 0.2f;

    public bool facingRight = true;

    //Ground Checker
    public Transform groundCheck;
    public LayerMask groundLayer;

    //Wall Checker
    public Transform wallCheck;
    public LayerMask wallLayer;
    const float wallCheckRadius = 0.2f;
    [SerializeField] float slideFactor = 0.2f;

    private BoxCollider2D boxCollider2d;

    //Non-Serialized Variables
    private float moveInput;
    private float horizontalValue;
    private float xVal;

    private bool isGrounded;
    private bool isRunning;
    private bool isGrabbing;
    


    //Animator Component
    private Animator anim;

    Rigidbody2D rb;

    void Start()
    {
        //animatorzxc = gameObject.GetComponent<Animator>();
        view = GetComponent<PhotonView>();
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2d = transform.GetComponent<BoxCollider2D>();
    }

    void Update()
    {   
        // Put all movement shit in this if block
        if (view.IsMine) {
            horizontalValue = Input.GetAxisRaw("Horizontal");
            //animatorzxc.SetBool("isWalking", false);

            //Jump
            jumpMultiplier = 2.0f;
            jumpForce = 10f * jumpMultiplier;
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
                if (!(GameObject.Find("Player").GetComponent<GroundChecker>().isGrounded))
                {
                    anim.SetBool("isWalking", true);
                    if (!gameObject.GetComponent<GroundChecker>().isGrounded)
            {
                        //Debug.Log("xd");
                    }
                    else
                    {
                        Walk();
                    }
                    isRunning = false;
                }
                else
                {
                    if(!gameObject.GetComponent<GroundChecker>().isGrounded)
                    {
                        //Debug.Log("xd");
                    }
                    else
                    {
                        runningSpeed = speed * 2f;
                        Sprint();
                        anim.SetBool("isRunning", true);
                        anim.SetBool("isWalking", false);
                    }

                }
                isRunning = false;
            }
            //Sprint
            else
            {
                if(!gameObject.GetComponent<GroundChecker>().isGrounded)
                {
                    //Debug.Log("xd");
                }
                else
                {
                    runningSpeed = speed * 2f;
                    Sprint();
                    //animatorzxc.SetBool("isRunning", true);
                    //animatorzxc.SetBool("isWalking", false);
                }

            }

            WallCheck();
        }
    }

    void FixedUpdate()
    {
        if (!gameObject.GetComponent<GroundChecker>().isGrounded)
        {
            //Debug.Log("xd");
        }
        else
        {
            Move(horizontalValue);
        }

    }

    void Move(float dir)
    {
        if (!isRunning)
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
        rb.velocity = new Vector2(horizontalValue * xVal, rb.velocity.y);
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

    void WallCheck()
    {
        if(Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wallLayer) && Mathf.Abs(horizontalValue) > 0 && /*rb.velocity.y < 0 &&*/ !gameObject.GetComponent<GroundChecker>().isGrounded)
        {
            Debug.Log(isGrabbing);
            Vector2 v = rb.velocity;
            v.y = -slideFactor;
            rb.velocity = v;
            isGrabbing = true;
            if(isGrabbing)
            {
                rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed , jumpForce);
                //Revenant Climb
                if(isGrabbing)
                    rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, jumpForce);
            }
        }
        else
        {

        }
    }
}

