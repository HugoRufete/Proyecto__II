using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly : MonoBehaviour
{
    private Transform player; 
    private EnemyFollow enemyFollow;
    private Animator animator;

    public int damage = 10;
    public VidaPlayer vidaPlayer;

    [SerializeField] private float radiousToExplode; 
    

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        enemyFollow = GetComponent<EnemyFollow>();
        animator = GetComponent<Animator>();
        
    }

    void Update()
    {
        if (player != null)
        {
            if (radiousToExplode >= Vector3.Distance(transform.position, player.position))
            {
                enemyFollow.followSpeed = 3;
                animator.Play("Exploding_Firefly");

            }
        }
    }

    public void FireflyDie()
    {
        Destroy(gameObject);
    }
}
