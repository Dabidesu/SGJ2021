using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] GameObject boss;
    [SerializeField] GameObject player;
    [SerializeField] GameObject bossHealthUI;
    [SerializeField] GameObject playerHealthUI;
    [SerializeField] GameObject playerManaUI;
    float bossHealth, playerHealth, playerMana, bossMaxHealth;
    Vector3 initialBossUIScale, initialPlayerUIScale, initialPlayerManaUIScale;
    void Start()
    {
        bossMaxHealth = boss.GetComponent<Boss>().MaxHealth;
        initialBossUIScale = bossHealthUI.GetComponent<RectTransform>().localScale;
        initialPlayerUIScale = playerHealthUI.GetComponent<RectTransform>().localScale;
        initialPlayerManaUIScale = playerManaUI.GetComponent<RectTransform>().localScale;
    }

    // Update is called once per frame
    void Update()
    {
        bossHealth = boss.GetComponent<Boss>().Health;
        playerHealth = player.GetComponent<Player>().Health;
        playerMana = player.GetComponent<Player>().Mana;
        bossHealthUI.GetComponent<RectTransform>().localScale = new Vector3(initialBossUIScale.x * (bossHealth/bossMaxHealth), initialBossUIScale.y, initialBossUIScale.z);
        playerHealthUI.GetComponent<RectTransform>().localScale = new Vector3(initialPlayerUIScale.x * (playerHealth/100), initialPlayerUIScale.y, initialPlayerUIScale.z);
        playerManaUI.GetComponent<RectTransform>().localScale = new Vector3(initialPlayerManaUIScale.x * (playerMana/100), initialPlayerManaUIScale.y, initialPlayerManaUIScale.z);
    }

}
