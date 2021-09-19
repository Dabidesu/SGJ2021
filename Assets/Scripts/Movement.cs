using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Movement : MonoBehaviour
{
    PhotonView view;
    //Serialized Variables
    [Header("Platform Layer Mask")]
    [SerializeField] private LayerMask platformLayerMask;

    [Header("Movement")]
    public float speed;
    public float runningSpeed;
    public float jumpForce;
    public float dashForce;
    public float jumpMultiplier;
    public float wallJumpTime = 0.2f;

    public float startDashTimer;
    public float dashCooldown;
    public float slideFactor = 0.2f;

    [Header("Object Reference")]
    public GameObject hint;
    
    //Ground Checker
    public Transform groundCheck;
    public LayerMask groundLayer;

    //Wall Checker
    public Transform wallCheck;
    public LayerMask wallLayer;

    //Non-Serialized Variables
    private float moveInput;
    private float horizontalValue;
    private float xVal;
    private float currDashTimer;
    private float dashDirection;
    const float wallCheckRadius = 0.2f;
    private int dashCounter;

    [Header("Debug")]
    public bool isGrounded;
    public bool isRunning;
    public bool isGrabbing;
    public bool isDashing;
    public bool facingRight = true;

    private BoxCollider2D boxCollider2d;

    //Animator Component
    private Animator anim;

    Rigidbody2D rb;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
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
        if (view.IsMine)
        {
            horizontalValue = Input.GetAxisRaw("Horizontal");
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
            //Jump
            jumpForce = 10f * jumpMultiplier;
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (gameObject.GetComponent<GroundChecker>().isGrounded)
                {
                    Jump();
                }
            }

            //Walk
            if (horizontalValue != 0 && !Input.GetKey(KeyCode.LeftShift))
            {
                if (!gameObject.GetComponent<GroundChecker>().isGrounded)
                {
                    anim.SetBool("isWalking", false);
                }
                else
                {
                    Walk();
                    anim.SetBool("isWalking", true);
                }
                
                isRunning = false;
            }
            //Sprint
            else if(horizontalValue != 0 && Input.GetKey(KeyCode.LeftShift))
            {
                
                if (!gameObject.GetComponent<GroundChecker>().isGrounded)
                {


                }
                else if(gameObject.GetComponent<GroundChecker>().isGrounded)
                {
                    anim.SetBool("isRunning", true);
                    runningSpeed = speed * 2f;
                    Sprint();

                }
            }

            //Wall Climb
            WallCheck();
            
            //Dash
            if(Input.GetKeyDown(KeyCode.Space) && !isGrounded && horizontalValue != 0)
            {
                isDashing = true;
                currDashTimer = startDashTimer;
                rb.velocity = Vector2.zero;
                dashDirection = (int)horizontalValue;
                anim.SetBool("isMidAir", true);
                if(dashCounter < 3)
                {
                    Debug.Log("Dash Counter: " + (dashCounter /*+ 1*/));
                    dashCounter++;
                }
            }
            
            if(isDashing)
            {
                if(dashCounter < 3)
                {
                    rb.velocity = transform.right * dashDirection * dashForce;
                    currDashTimer -= Time.deltaTime;
                    
                    if (currDashTimer <= 0)
                    {
                        isDashing = false;
                    }

                }
                else if(dashCounter == 3)
                {
                    
                    hint.SetActive(true);
                    anim.SetBool("isStoppedMidAir", true);
                    StartCoroutine(DashCoolDown());
                    isDashing = false;
                    dashCooldown = dashCooldown + Time.deltaTime;
                    if (dashCooldown == 3f)
                    {
                        Debug.Log("Cooldown Finished");
                    }
                }

            }
            else
            {
                anim.SetBool("isMidAir", false);
                //anim.SetBool("isStoppedMidAir", false);
            }

        }
    }

    void FixedUpdate()
    {
        if (!gameObject.GetComponent<GroundChecker>().isGrounded)
        {
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
        rb.velocity = new Vector2(horizontalValue * runningSpeed, rb.velocity.y);
    }

    void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
    }

    void WallCheck()
    {
        if (Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wallLayer) && Mathf.Abs(horizontalValue) > 0 && /*rb.velocity.y < 0 &&*/ !gameObject.GetComponent<GroundChecker>().isGrounded)
        {
            Vector2 v = rb.velocity;
            v.y = -slideFactor;
            rb.velocity = v;
            isGrabbing = true;
            anim.SetBool("isClimbing", false);
            if (isGrabbing)
            {
                rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, jumpForce);
                //Revenant Climb
                if (isGrabbing)
                {
                    rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, jumpForce);

                    //Climb Animation
                    anim.SetBool("isClimbing", true);

                }

            }
        }
        else
        {
            anim.SetBool("isClimbing", false);
        }
    }

    IEnumerator DashCoolDown()
    {
        anim.SetBool("isStoppedMidAir", false);
        anim.SetBool("isMidAir", false);
        yield return new WaitForSeconds(3);
        
        dashCounter -= dashCounter;
        isDashing = false;
        hint.SetActive(false);
    }
}

