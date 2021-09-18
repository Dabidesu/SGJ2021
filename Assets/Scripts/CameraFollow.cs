using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;
    [SerializeField] public Vector3 offset;

    [Range(1,10)]
    public float smoothFactor;

    void Start () {
        offset = new Vector3(0f, 0f, -10f);
    }

    private void FixedUpdate()
    {
        Follow();
    }

    public void Follow()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor*Time.fixedDeltaTime);
        transform.position = targetPosition;
    }
}
