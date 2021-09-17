using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    float staminaCost;
    public float StaminaCost {get;}
    public void UpdateStats () {
        damage = BaseDamage * Rank;
        staminaCost = 5f * Rank;
        setRankColor(Rank);
    }
    // public Dictionary<string, float> Fire(Vector3 userPosition, Vector3 targetPosition) {
    //     return new Dictionary<string, float>(){
    //         {"StaminaCost", StaminaCost}
    //     };
    // }
}
