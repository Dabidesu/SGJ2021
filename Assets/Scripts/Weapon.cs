using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected Color rankColor;
    protected float baseDamage;
    protected float damage;
    public float BaseDamage {
        get {return baseDamage;}
    }
    public float Damage {
        get {return BaseDamage * Rank;}
    }
    [SerializeField] private int rank;
    public int Rank {
        get{return rank;} 
        set{
            if (value < 1) {
                rank = 1;
            } else if (value > 4) {
                rank = 4;
            } else {
                rank = value; 
            }
            setRankColor(rank);
        }
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Q)) {
            Rank -= 1;
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            Rank += 1;
        }
    }

    public void UpgradeRank(Player user) {
        user.Weapon.GetComponent<Weapon>().Rank += 1;
    }
    public void DowngradeRank(Player user) {
        user.Weapon.GetComponent<Weapon>().Rank -= 1;
    }

    protected void setRankColor(int rank) {
        switch (rank) {
            case 1:
                rankColor = Color.white;
                break;
            case 2:
                rankColor = Color.blue;
                break;
            case 3:
                rankColor = Color.magenta;
                break;
            case 4:
                rankColor = Color.red;
                break;
            default:
                throw new System.Exception("unavailable rarity");
        }
        this.GetComponent<SpriteRenderer>().color = rankColor;
    }

}