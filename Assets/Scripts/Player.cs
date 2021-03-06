using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable<float>
{
    public int PlayerID;
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
        set{weapon = value;}
    }
    public GameObject RightHandPrefab;
    public GameObject PlayerCursorPrefab;
    public float Health {
        get {return health;}
        set {
            if (value < 0) {
                health = 0;
                isAlive = false;
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

    // public GameObject HealthBar;
    // public GameObject ManaBar;
    // public GameObject StaminaBar;
    public GameObject WallChecker;
    public GameObject CameraPrefab;

    void Start()
    {   
        cursor = GameObject.Instantiate(PlayerCursorPrefab, Vector3.zero, Quaternion.identity);
        cursor.GetComponent<PlayerCursor>().player = this;
        // camera.transform.SetParent(this.gameObject.transform);
        // camera.GetComponent<CameraFollow>().target = transform;
        // camera.GetComponent<CameraFollow>().Follow();
        // cursor.GetComponent<PlayerCursor>().player = this;
        // cursor.GetComponent<PlayerCursor>().camera = Camera.main;
    }

    void Update()
    {
        if (weapon != null) {

            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                weapon.GetComponent<IWeapon>().Fire(this, transform.position, cursor.transform.position);
            }

            if (Input.GetKeyDown(KeyCode.E)) {
                weapon.GetComponent<IWeapon>().DropItem(this, transform.position, cursor.transform.position);
            }

        }
        if (isAlive) {
            RegenerateResources();
        }
        // UpdateStatusBars();
    }

    // void UpdateStatusBars() {
    //     HealthBar.GetComponent<Transform>().localScale = new Vector3(Health/100,1,1);
    //     ManaBar.GetComponent<Transform>().localScale = new Vector3(Mana/100,1,1);
    //     StaminaBar.GetComponent<Transform>().localScale = new Vector3(Stamina/100,1,1);
    // }

    void OnCollisionEnter2D(Collision2D col) {
        if ( weapon == null && col.gameObject.tag == "Weapon") {
            weapon = col.gameObject;
            weapon.transform.position = RightHandPrefab.transform.position + Vector3.back;
            weapon.transform.SetParent(RightHandPrefab.transform);
            weapon.GetComponent<Collider2D>().enabled = false;
            weapon.GetComponent<Rigidbody2D>().isKinematic = true;
            weapon.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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
