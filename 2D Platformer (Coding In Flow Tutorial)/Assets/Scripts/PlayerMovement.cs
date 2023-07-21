using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //These are all references to Unity components
    private Rigidbody2D rb; //creates a rigidbody variable to access the component in Unity
    private Animator anim;
    private SpriteRenderer sprite; //variable used to switch between left and right animations
    private BoxCollider2D coll; //Reference to the BoxCollider2D component of the Player

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f; //IMPORTANT: [SerializeField] is used to change the value directly in Unity
    [SerializeField] private float jumpForce = 14f;

    [SerializeField] private LayerMask jumpableGround;

    private enum MovementState { idle, running, jumping, falling }

    [SerializeField] private AudioSource jumpSoundEffect;
 
    // Start is called before the first frame update
    private void Start()
    {
        //Better put the all the GetComponent calls in the Start so they're only done once
        rb = GetComponent<Rigidbody2D>(); //We need to access the Rigidbody 2D component of the Player
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
            //We don't use GetButtonDown with Left and Right for the movements, 
            //instead we use the value of the horizontal axis which is between -1 and +1 (gradient between them)
        dirX = Input.GetAxisRaw("Horizontal"); //When we press left this will be set to -1 and to +1 with right
            //Raw means that the value will immediately return to 0 as we stop pressing the button
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y); 
            //Only x and y axis, THE DIRECTION OF THE VELOCITY DEPENDS ON THE SIGN OF DirX (- or +)
            //velocity on the y axis remains the same as the frame before, only the x changes

            //Better than GetKeyDown because GetButtonDown uses the Unity Input System, which is flexible and not hard coded
            //The string now contains the button name instead of the key (better)
            //GetButtonDown: if the Space button is pressed (not hold) in any frame of the game
        if(Input.GetButtonDown("Jump") && IsGrounded()) 
        {
            jumpSoundEffect.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce); //velocity on the x axis remains the same as the frame before, only the y changes 
        } 

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if(dirX > 0f) //If for the running animation
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if(dirX < 0)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if(rb.velocity.y > .1f) //If for the jumping animation
        {
            state = MovementState.jumping;
        }
        else if(rb.velocity.y < -.1f) //If we have a downward force applied
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state); //(int) is a cast to turn the enum into its corresponding int value
    }

    private bool IsGrounded() //Method that decides if the player is touching the ground or not
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround); //cheks if we are overlapping a jumpable Ground with the BoxCast
        //Boxcast creates a box around the Player the same shape of the box collider
        //0f for the rotation
        //Vector2.down moves the box down by .1f (a bit of downward offset) to check when the Boxcast overlaps with the ground
        //the offset is only downward because we don't want to detect left or right collisions
    }
}