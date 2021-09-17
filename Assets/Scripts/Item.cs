using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class Item : MonoBehaviour
{
    public enum InteractionType{ None, PickUp, Examine}
    public InteractionType type;

    [Header("Examine")]
    public string descText;
    public Sprite img;

    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;

        //Item Layer Num
        gameObject.layer = 10;
    }
    public void Interact()
    {
        switch(type)
        {
            case InteractionType.PickUp:
                GameObject item = gameObject;
                FindObjectOfType<Interaction>().PickUpItem(gameObject);
                gameObject.SetActive(false);
                break;
            case InteractionType.Examine:
                FindObjectOfType<Interaction>().ExamineItem(this);
                break;
            default:
                Debug.Log("No Assigned Type");
                break;
        }
    }
}
