using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IDamagable<float>
{
    [SerializeField] float health;

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
    }

    public void Damage (float DamageAmount) {
        Health -= DamageAmount;
    }
}
