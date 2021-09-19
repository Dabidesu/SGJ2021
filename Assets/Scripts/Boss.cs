using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IDamagable<float>
{
    [SerializeField] float health;
    [SerializeField] float moveSpeed;
    [SerializeField] float attackWindup;
    [SerializeField] bool playerWithinRange;
    [SerializeField] GameObject leftHand;
    [SerializeField] GameObject rightHand;
    Collider2D leftHandCollider;
    Collider2D rightHandCollider;
    Rigidbody2D rb;
    Animator anim;

    public float Health {
        get { return health; }
        set {
            if (value <= 0) { 
                health = 0; 
                Destroy(this.gameObject);
            }
            else if (value >= 100) { health = 100; }
            else { health = value; }
        }
    }

    void Start()
    {
        Health = 100f;
        playerWithinRange = false;
        rb = GetComponent<Rigidbody2D>();
        leftHandCollider = leftHand.GetComponent<Collider2D>();
        rightHandCollider = rightHand.GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        anim.SetFloat("ChargeupTime", attackWindup);
    }

    public void Damage (float DamageAmount) {
        Health -= DamageAmount;
    }

    void FixedUpdate () {
        if (!playerWithinRange) {
            MoveToPlayer();
        } else {
            StartCoroutine(Attack());
        }
    }

    void MoveToPlayer () {
        rightHandCollider.enabled = false;
        leftHandCollider.enabled = false;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 targetVector = player.transform.position - transform.position + Vector3.up*2f;
        rb.velocity = targetVector.normalized * moveSpeed;
    }
    
    IEnumerator Attack() {
        rb.velocity = Vector3.zero;
        anim.SetBool("Attacking", true);
        yield return new WaitForSecondsRealtime(attackWindup);
        rightHandCollider.enabled = true;
        leftHandCollider.enabled = true;
        yield return new WaitForSecondsRealtime(1f);
    }

    void OnTriggerEnter2D (Collider2D col) {
        if (col.CompareTag("Player"))  {
            playerWithinRange = true;
        }
    }

    void OnTriggerStay2D (Collider2D col) {
        if (col.CompareTag("Player"))  {
            playerWithinRange = true;
            anim.SetBool("Attacking", true);
        }
    }

    void OnTriggerExit2D (Collider2D col) {
        if (col.CompareTag("Player"))  {
            playerWithinRange = false;
            anim.SetBool("Attacking", false);
        }
    }
}
