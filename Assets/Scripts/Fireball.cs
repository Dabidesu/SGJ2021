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
        
        if (col.CompareTag("Player")) {
            Debug.Log($"hitid: {col.gameObject.GetComponent<Player>().PlayerID}, sourceid: {User.PlayerID}");
        }

        if (col.CompareTag("Player") && col.gameObject.GetComponent<Player>().PlayerID != User.PlayerID) {
            col.gameObject.GetComponent<Player>().Health -= damage;
            Destroy(this.gameObject);
        } 
    }

}
