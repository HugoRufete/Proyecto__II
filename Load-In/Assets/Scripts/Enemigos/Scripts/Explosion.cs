using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    VidaPlayer player;

    public int damage;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<VidaPlayer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.PlayerTakeDamage(damage);
        }
    }
}
