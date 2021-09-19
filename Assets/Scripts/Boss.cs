using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IDamagable<float>
{

    [SerializeField] ParticleSystem ps;
    [SerializeField] bool isAlive;
    public float MaxHealth;
    [SerializeField] float health;
    [SerializeField] float moveSpeed;
    [SerializeField] float attackWindup;
    [SerializeField] bool playerWithinRange;
    [SerializeField] bool attacking;
    [SerializeField] GameObject leftHand;
    [SerializeField] GameObject rightHand;
    Collider2D leftHandCollider;
    Collider2D rightHandCollider;
    Rigidbody2D rb;
    Animator anim;
    GameObject player;

    public float Health {
        get { return health; }
        set {
            if (value <= 0) { 
                health = 0; 
                isAlive = false;
                disableSprites();
            }
            else if (value >= MaxHealth) { health = MaxHealth; }
            else { health = value; }
        }
    }

    void Start()
    {
        isAlive = true;
        playerWithinRange = false;
        attacking = false;
        health = MaxHealth;
        rb = GetComponent<Rigidbody2D>();
        leftHandCollider = leftHand.GetComponent<Collider2D>();
        rightHandCollider = rightHand.GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        anim.SetFloat("ChargeupTime", attackWindup);
        player = GameObject.FindGameObjectWithTag("Player");
        rightHandCollider.enabled = false;
        leftHandCollider.enabled = false;
    }

    public void Damage (float DamageAmount) {
        Health -= DamageAmount;
    }

    void Update () {
        if (!playerWithinRange) {
            MoveToPlayer();
        } else {
            if (!attacking) {
                StartCoroutine(Attack());
            }
        }
    }

    void MoveToPlayer () {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector3 targetVector = player.transform.position - transform.position + Vector3.up*2f;
        rb.velocity = targetVector.normalized * moveSpeed;
    }
    
    IEnumerator Attack() {
        attacking = true;
        rb.velocity = Vector3.zero;
        anim.SetBool("Charging", true);
        anim.SetBool("Attacking", true);
        rightHandCollider.enabled = true;
        leftHandCollider.enabled = true;
        yield return new WaitForSecondsRealtime(attackWindup);
        ps.Play();
        anim.SetBool("Charging", false);
        if (playerWithinRange) {
            player.GetComponent<Player>().Damage(30);
        }
        rightHandCollider.enabled = false;
        leftHandCollider.enabled = false;
        yield return new WaitForSecondsRealtime(1f);
        attacking = false;
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

    void disableSprites () {
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer sprite in sprites) {
            sprite.enabled = false;
        }
    }
}
