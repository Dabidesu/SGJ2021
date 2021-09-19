using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursor : MonoBehaviour
{   
    Transform cursorTransform;
    Vector3 mouseScreenPos;
    Vector3 mouseWorldPos;
    public Player player;
    Camera PlayerCam;

    void Start () {
        PlayerCam = Camera.main;
    }
    void Update()
    {
        MoveCursor();
    }

    void MoveCursor() {
        mouseScreenPos = Input.mousePosition;
        mouseWorldPos = Vector3.Scale(PlayerCam.ScreenToWorldPoint(mouseScreenPos), new Vector3(1, 1, 0));
        this.transform.position = mouseWorldPos;
    }
}
