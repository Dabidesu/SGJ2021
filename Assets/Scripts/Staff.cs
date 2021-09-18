using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : Weapon, IRangedWeapon, IMagicWeapon
{   
    [SerializeField] float firingOffset = 0.1f;
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
            GameObject proj = Instantiate(ProjectilePrefab, user.WallChecker.transform.position + Vector3.right * firingOffset, Quaternion.identity);
            proj.GetComponent<Fireball>().userTransform = transform;
            proj.GetComponent<Fireball>().damage = Damage;
            proj.GetComponent<Fireball>().User = user;
            proj.GetComponent<SpriteRenderer>().color = rankColor;
            proj.GetComponent<Rigidbody2D>().velocity = forceVector2d.normalized * ProjectileSpeed;

            user.Mana -= ManaCost;
        }
    }

    public void DropItem(Player user, Vector3 userPosition, Vector3 targetPosition) {
        Vector3 forceVector3d = targetPosition - userPosition;
        Vector2 forceVector2d = new Vector2(forceVector3d.x, forceVector3d.y);
        GameObject weapon = user.Weapon;
        weapon.transform.parent = null;
        weapon.GetComponent<Rigidbody2D>().velocity = forceVector2d.normalized * ProjectileSpeed * 0.1f;
        StartCoroutine(DropWeapon(1f, weapon));
        
        user.Weapon = null;
    }

    IEnumerator DropWeapon (float secs, GameObject weapon) {
        yield return new WaitForSecondsRealtime(secs);
        weapon.GetComponent<Collider2D>().enabled = true;
        yield return new WaitForSecondsRealtime(2f);
        weapon.GetComponent<Rigidbody2D>().isKinematic = false;
    }
}
