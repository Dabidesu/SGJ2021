using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameSupervisor : MonoBehaviour
{
    public void Start () {
        print("Start");
    }
    public void StartGame() {
        SceneManager.LoadScene("TheOnlyLevel");
    }
}
