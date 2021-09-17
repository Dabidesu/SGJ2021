using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    [Header("Detection Parameter(s)")]
    public Transform detectionPoint;
    private const float detectionRadius = 0.2f;
    public LayerMask detectionLayer;
    public GameObject detectedObject;

    [Header("Examine Parameter(s)")]
    public GameObject examineWindow;
    public Image examineImage;
    public Text examineText;

    [Header("Item")]
    public List<GameObject> pItems = new List<GameObject>();

    private float delayinSeconds = 3;

    void Update()
    {
        if(DetectObject())
        {
            if(InteractInput())
            {
                detectedObject.GetComponent<Item>().Interact();
            }
        }

        if (examineWindow.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            examineWindow.SetActive(false);
        }
        else if(examineWindow.activeSelf)
        {
            if (delayinSeconds > 0)
                delayinSeconds -= Time.deltaTime;
 
            if (delayinSeconds == 0 || delayinSeconds < 0)
            {
                examineWindow.SetActive(false);
                delayinSeconds = 3;
            }
        }
    }

    bool InteractInput()
    {
        //Not yet familiar with the new Input System kaya lagay muna ako placeholder for now
        return Input.GetKeyDown(KeyCode.E);
    }

    bool DetectObject()
    {
        Collider2D obj = Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, detectionLayer);
        if(obj == null)
        {
            //detectedObject = null;
            return false;
        }
        else
        {
            detectedObject = obj.gameObject;
            return true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(detectionPoint.position, detectionRadius);
    }

    public void PickUpItem(GameObject item)
    {
        pItems.Add(item);
    }

    public void ExamineItem(Item item)
    {
        examineImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
        examineText.text = item.descText;
        examineWindow.SetActive(true);

        
            

    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
    }

}
