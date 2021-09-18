using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Fireball : MonoBehaviourPun
{
    public Player User;
    // public Transform userTransform;
    public float damage;

    void Start () {
        float lifetime = 5f;
        StartCoroutine(DestroyAfter(lifetime));
    }

    IEnumerator DestroyAfter(float secs) {
        yield return new WaitForSecondsRealtime(secs);
        Destroy(this.gameObject);
        this.GetComponent<PhotonView>().RPC("destroy", RpcTarget.AllBuffered);
    }

    void Update()
    {
        // if (Vector3.Distance(userTransform.position, transform.position) > DespawnDistance) {
        //     Destroy(this.gameObject);
        // }
    }   

    void OnTriggerEnter2D (Collider2D col) {
        
        if (col.CompareTag("Player")) {
            print($"{User.GetComponent<PhotonView>().GetInstanceID()} -> {col.GetComponent<PhotonView>().GetInstanceID()}");
            if (User.GetComponent<PhotonView>().GetInstanceID() != col.GetComponent<PhotonView>().GetInstanceID()) {
                col.gameObject.GetComponent<PhotonView>().RPC("takeDamage", RpcTarget.AllBuffered, damage);
                col.gameObject.GetComponent<Player>().Health -= damage;
                this.GetComponent<PhotonView>().RPC("destroy", RpcTarget.AllBuffered);
            }
        } 
    }

    [PunRPC]
    public void destroy() {
        Destroy(this.gameObject);
    }

}
