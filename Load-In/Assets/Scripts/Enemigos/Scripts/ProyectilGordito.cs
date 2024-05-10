using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilGordito : MonoBehaviour
{
    public float speed = 10.0f;
    public float destroyTime = 3.0f;
    public Transform target;

    void Start()
    {
        if (target != null)
        {
            Vector3 targetDirection = (target.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().velocity = targetDirection * speed;
        }
        else
        {
            Destroy(gameObject);
        }

        Destroy(gameObject, destroyTime);
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 targetDirection = (target.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().velocity = targetDirection * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
