using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; 
    public float followHeight = -15f; 

    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, followHeight);
    }

    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, followHeight);
    }
}