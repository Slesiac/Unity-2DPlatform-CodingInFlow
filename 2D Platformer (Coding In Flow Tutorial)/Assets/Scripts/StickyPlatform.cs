using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    // Let's check if the Player touches the box collider of the platform in order to parent the Player to the platform
    
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.name == "Player") // if the collision is with the Player
        {
            collision.gameObject.transform.SetParent(transform); // Player = child of the moving platform
        }   
    }

    private void OnTriggerExit2D(Collider2D collision) // EXIT for when the player leaves the platform
    {
        if(collision.gameObject.name == "Player") 
        {
            collision.gameObject.transform.SetParent(null); 
        }   
    }
}
