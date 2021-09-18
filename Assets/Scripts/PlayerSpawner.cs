using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject PlayerPrefab;
    [SerializeField] GameObject[] spawners;
    GameObject randomSpawner;

    void Start () {
        spawners = GameObject.FindGameObjectsWithTag("Spawner");
        
        if (spawners.Length > 0) {
            do {
                randomSpawner = spawners[new System.Random().Next(spawners.Length)];
            } while (randomSpawner.GetComponent<Spawner>().isOccupied);
            PhotonNetwork.Instantiate(PlayerPrefab.name, randomSpawner.transform.position, Quaternion.identity);
        }
    }
}
