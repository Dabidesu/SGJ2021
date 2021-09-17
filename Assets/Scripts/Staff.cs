using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : Weapon, IRangedWeapon, IMagicWeapon
{   
    Color staffColor;
    Color fireballColor;
    public GameObject ProjectilePrefab;
    float projectileSpeed;
    float baseManaCost;
    public float ProjectileSpeed {
        get {return projectileSpeed;}
    }
    public float ManaCost {
        get {
            return baseManaCost * Rank;
        }
    }

    void Start () {
        projectileSpeed = 15f;
        Rank = 1;
        baseDamage = 5f;
        baseManaCost = 5f;
    }

    public void Fire(Player user, Vector3 userPosition, Vector3 targetPosition) {
        setRankColor(Rank);

        if (user.Mana >= ManaCost) {
            Vector3 forceVector3d = targetPosition - userPosition;
            Vector2 forceVector2d = new Vector2(forceVector3d.x, forceVector3d.y);
            GameObject proj = Instantiate(ProjectilePrefab, userPosition, Quaternion.identity);
            proj.GetComponent<Fireball>().userTransform = transform;
            proj.GetComponent<Fireball>().damage = Damage;
            proj.GetComponent<SpriteRenderer>().color = rankColor;
            proj.GetComponent<Rigidbody2D>().velocity = forceVector2d.normalized * ProjectileSpeed;
            
            user.Mana -= ManaCost;

            Debug.Log($"Mana Left: {user.Mana}");
        }
    }
}
