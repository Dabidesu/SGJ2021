using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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

    public GameObject GemPrefab;

    void Start () {
        projectileSpeed = 15f;
        Rank = 1;
        baseDamage = 5f;
        baseManaCost = 5f;
        SetGemColor(Rank);
    }

    public void Fire(Player user, Vector3 userPosition, Vector3 targetPosition) {
        SetGemColor(Rank);

        if (user.Mana >= ManaCost) {
            Vector3 forceVector3d = targetPosition - GemPrefab.GetComponent<Transform>().position;
            Vector2 forceVector2d = new Vector2(forceVector3d.x, forceVector3d.y);

            
            GameObject proj = PhotonNetwork.Instantiate(ProjectilePrefab.name, GemPrefab.GetComponent<Transform>().position, Quaternion.identity);
            proj.transform.LookAt(Vector3.Scale(targetPosition, new Vector3(1, 1, 0)), proj.transform.position.normalized);
            proj.GetComponent<Fireball>().damage = Damage;
            proj.GetComponent<Fireball>().SourceID = user.GetComponent<PhotonView>().ViewID;
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
        yield return new WaitForSecondsRealtime(5f);
        weapon.GetComponent<Rigidbody2D>().isKinematic = false;
    }

    void SetGemColor(int rank) {
        setRankColor(Rank);
        GemPrefab.GetComponent<SpriteRenderer>().color = rankColor;
    }

}
