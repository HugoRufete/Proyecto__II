using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilGordito : MonoBehaviour
{
    public float velocidad = 5f;
    public int damage = 10;

    private Transform target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = (target.position - transform.position).normalized;
        float distanceThisFrame = velocidad * Time.deltaTime;
        transform.Translate(direction * distanceThisFrame, Space.World);

        Invoke("DestroyProjectile", 3f);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    public void DestroyProjectile()
    {
        Destroy(this.gameObject);   
    }
}
