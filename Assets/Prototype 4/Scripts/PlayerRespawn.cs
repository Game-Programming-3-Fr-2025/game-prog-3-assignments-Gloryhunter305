using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Vector3 respawnPoint;

    private PlayerScript player;

    // Add these
    public GameObject corpsePrefab; // prefab that contains a Rigidbody2D and sprite
    public Rigidbody2D playerRb;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        if (player != null)
            playerRb = player.GetComponent<Rigidbody2D>();
    }

    public void RespawnNow()
    {
        // Spawn a corpse that keeps physics/momentum
        if (corpsePrefab != null && player != null)
        {
            GameObject corpse = Instantiate(corpsePrefab, player.transform.position, player.transform.rotation);
            Debug.Log("Spawned corpse at " + corpse.transform.position);
            Rigidbody2D corpseRb = corpse.GetComponent<Rigidbody2D>();
            if (corpseRb != null && playerRb != null)
            {
                corpseRb.linearVelocity = Vector2.zero; // transfer current momentum
            }
        }

        // Reset player position and clear velocity so player doesn't keep Rigidbody momentum
        if (player != null)
        {
            player.transform.position = respawnPoint;
            if (playerRb != null)
            {
                playerRb.linearVelocity = Vector2.zero;
                playerRb.angularVelocity = 0f;
            }
        }

        transform.position = respawnPoint;
    }

    private void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            RespawnNow();
        }
    }
}
