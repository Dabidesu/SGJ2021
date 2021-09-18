using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Player User;
    public Transform userTransform;
    public float damage;
    public float DespawnDistance = 20f;

    void Start () {

    }
    void Update()
    {
        if (Vector3.Distance(userTransform.position, transform.position) > DespawnDistance) {
            Destroy(this.gameObject);
        }
    }   

    void OnTriggerEnter2D (Collider2D col) {
        Debug.Log("Collide");

        if (col.CompareTag("Player") && col.gameObject.GetComponent<Player>() != User) {
            col.gameObject.GetComponent<Player>().Health -= damage;
            Destroy(this.gameObject);
        } 
    }

}
