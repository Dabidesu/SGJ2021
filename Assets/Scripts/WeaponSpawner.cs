using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WeaponSpawner : MonoBehaviour
{
    public GameObject StaffPrefab;
    void Start()
    {
        SpawnWeapon();
    }


    void SpawnWeapon () {
        int[] ranks = new int[]{4, 3, 3, 2, 2, 2, 1, 1, 1, 1};
        int randomRank = ranks[new System.Random().Next(ranks.Length)];
        GameObject weapon = PhotonNetwork.Instantiate(StaffPrefab.name, transform.position, Quaternion.identity);
        weapon.GetComponent<Weapon>().Rank = randomRank;
        weapon.GetComponent<Weapon>().setRankColor(randomRank);
    }
    
    void Update()
    {
        
    }
}
