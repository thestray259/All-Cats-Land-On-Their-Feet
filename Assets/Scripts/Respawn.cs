using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] Transform respawnLocation;
    [SerializeField] GameObject player; // use cat fixed, not Player 
    //this script is to be put on the RESPAWN PLATFORMS
    // if player colliders with respawn boundaries, place player back at respawnLocation 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Colliding wth Player. ");
            collision.collider.GetComponent<Rigidbody>().position = respawnLocation.position;
            collision.collider.GetComponent<Rigidbody>().rotation = respawnLocation.rotation;
        }
    }
}
