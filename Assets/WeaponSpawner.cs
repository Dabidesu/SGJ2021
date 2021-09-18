using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField] float respawnWeaponTime;
    public GameObject WandPrefab;
    void Start()
    {
        SpawnWeapon();
    }

    void OnTriggerExit2D (Collider2D col) {
        if (col.CompareTag("NewWeapon")) {

        }
    }
    

    void SpawnWeapon () {
        int[] ranks = new int[]{4, 3, 3, 2, 2, 2, 1, 1, 1, 1};
        int randomRank = ranks[new System.Random().Next(ranks.Length)];
        GameObject weapon = PhotonNetwork.Instantiate(WandPrefab.name, transform.position, Quaternion.identity);
        weapon.GetComponent<Weapon>().Rank = randomRank;
        weapon.GetComponent<Weapon>().setRankColor(randomRank);
    }

    IEnumerator RespawnWeapon() {
        yield return new WaitForSecondsRealtime(respawnWeaponTime);
        SpawnWeapon();
    }
}
