using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public bool isOccupied;

    void OnTriggerEnter2D (Collider2D col) {
        if (col.gameObject.CompareTag("Player")) {
            isOccupied = true;
        }
    }

    void OnTriggerExit2D (Collider2D col) {
        if (col.gameObject.CompareTag("Player")) {
            isOccupied = false;
        }
    }
}
