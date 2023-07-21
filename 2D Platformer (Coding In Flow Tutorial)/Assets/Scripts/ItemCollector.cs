using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int kiwis = 0;

    [SerializeField] private Text kiwisText;
    [SerializeField] private AudioSource collectionSoundEffect;

    //Override of the OnTriggerEnter2D method (for Trigger objects)
    private void OnTriggerEnter2D(Collider2D collision) //the argument is the collision object
    {
        if(collision.gameObject.CompareTag("Kiwi")) //If the collision object has the Kiwi tag
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            kiwis++;
            kiwisText.text = "Kiwis: " + kiwis; //text is an easier way to access the Text component instead of using GetComponent
        }
    }
}
