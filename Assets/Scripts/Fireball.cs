using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Fireball : MonoBehaviourPun
{
    public float damage;

    void Start () {
        float lifetime = 5f;
        StartCoroutine(DestroyAfter(lifetime));
    }

    IEnumerator DestroyAfter(float secs) {
        yield return new WaitForSecondsRealtime(secs);
        Destroy(this.gameObject);
    }

    void Update()
    {
        // if (Vector3.Distance(userTransform.position, transform.position) > DespawnDistance) {
        //     Destroy(this.gameObject);
        // }
    }   

    // void OnCollisionEnter2D (Collision2D col) {
        
    //     if (col.gameObject.CompareTag("Enemy")) {
    //         Debug.Log("HIT");
    //         col.gameObject.GetComponent<IDamagable<float>>().Damage(damage);
    //     }
    // }

    void OnTriggerEnter2D (Collider2D col) {
        
        if (col.gameObject.CompareTag("Enemy")) {
            Debug.Log("HIT");
            col.gameObject.GetComponent<IDamagable<float>>().Damage(damage);
        }
    }

}
