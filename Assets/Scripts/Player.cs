using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviour, IDamagable<float>
{
    bool isAlive = true;
    [SerializeReference] float health = 100;
    [SerializeField] float mana = 100;
    [SerializeField] float stamina = 100;
    [SerializeField] float healthRegen = 2;
    [SerializeField] float manaRegen = 5;
    [SerializeField] float staminaRegen = 10;
    GameObject cursor;
    PlayerCursor cursorScript;
    GameObject weapon = null;
    public GameObject Weapon {
        get{return weapon;}
    }
    public GameObject PlayerCursorPrefab;
    public float Health {
        get {return health;}
        set {
            if (value < 0) {
                health = 0;
            } else if (value > 100) {
                health = 100;
            } else {
                health = value;
            }
        }
    }
    public float Mana {
        get {return mana;}
        set {
            if (value < 0) {
                mana = 0;
            } else if (value > 100) {
                mana = 100;
            } else {
                mana = value;
            }
        }
    }
    public float Stamina {
        get {return stamina;}
        set {
            if (value < 0) {
                stamina = 0;
            } else if (value > 100) {
                stamina = 100;
            } else {
                stamina = value;
            }
        }
    }
    public float HealthRegen {
        get {return healthRegen;}
    }
    public float ManaRegen {
        get {return manaRegen;}
    }
    public float StaminaRegen {
        get {return staminaRegen;}
    }

    void Start()
    {
        cursor = GameObject.Instantiate(PlayerCursorPrefab, Vector3.zero, Quaternion.identity);
        cursor.GetComponent<PlayerCursor>().player = this;
    }

    void Update()
    {
        if (weapon != null && Input.GetKeyDown(KeyCode.Mouse0)) {
            weapon.GetComponent<IWeapon>().Fire(this, transform.position, cursor.transform.position);
        }
        RegenerateResources();
    }

    void OnCollisionEnter2D(Collision2D col) {
        if ( weapon == null && col.gameObject.tag == "Weapon") {
            weapon = col.gameObject;
            weapon.transform.SetParent(transform);
            weapon.transform.position = transform.position + Vector3.back;
            weapon.GetComponent<Collider2D>().enabled = false;
            weapon.GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }

    public void Damage(float damageAmount = 50f){
        this.health -= damageAmount;
        if (this.health <= 0) {
            this.health = 0;
            this.Die();
        }
        Debug.Log($"Inflicted {damageAmount} damage! Current Health: {this.health}");
    }

    void RegenerateResources() {
        Health += HealthRegen * Time.deltaTime;
        Mana += ManaRegen * Time.deltaTime;
        Stamina += StaminaRegen * Time.deltaTime;
    }

    void Die(){
        this.isAlive = false;
        Debug.Log("Player Died!");
    }
}
