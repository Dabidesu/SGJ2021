using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Transform userTransform;
    public float damage;
    public float DespawnDistance = 20f;

    void Update()
    {
        if (Vector3.Distance(userTransform.position, transform.position) > DespawnDistance) {
            Destroy(this.gameObject);
        }
    }
}
