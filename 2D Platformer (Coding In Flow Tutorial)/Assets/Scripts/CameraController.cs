using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    //We can't use GetComponent because the Transform component is from another object (the Player)

    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z); //keep the z value already set in unity
        //transform is an easier way to access the Transform component instead of using GetComponent
    }
}
