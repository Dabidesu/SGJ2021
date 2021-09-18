using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField] float weaponRespawnTime;
    public GameObject StaffPrefab;
    void Start()
    {
        SpawnWeapon();
        print("AAA");
    }
    
    void OnTriggerExit2D (Collider2D col) {
        if (col.CompareTag("NewWeapon")) {
            RespawnWeapon();
        }   
    }

    IEnumerator RespawnWeapon () {
        yield return new WaitForSecondsRealtime(weaponRespawnTime);
        SpawnWeapon();
    }

    void SpawnWeapon () {
        int[] ranks = new int[]{4, 3, 3, 2, 2, 2, 1};
        int randomRank = ranks[new System.Random().Next(ranks.Length)];
        GameObject weapon = PhotonNetwork.Instantiate(StaffPrefab.name, transform.position, Quaternion.identity);
        weapon.tag = "NewWeapon";
        weapon.GetComponent<Weapon>().Rank = randomRank;
        weapon.GetComponent<Weapon>().setRankColor(randomRank);
    }

}
