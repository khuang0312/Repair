using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    //nothing is added because processKeyInputs always returns a Direction
    //and we need a Direction object that doesn't do anything to satisfy
    //the function having to return something and because processMovement
    //always needs a Direction object...

    enum Direction
    {
        left,
        right,
        up,
        nothing
    }

    public Rigidbody2D block;
    private float thrust = 1.0f;
    Direction direction;


    void Update()
    {
       direction = processKeyInputs();
       
    }

    private void FixedUpdate()
    {
        processMovement(direction);
    }

    Direction processKeyInputs()
    {
        //GetAxisRaw returns either 1 or -1 compared to...
        //its counterpart GetAxis which returns values between
        //-1 and 1

        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            return Direction.left;
        }

        else if (Input.GetAxisRaw("Horizontal") == 1)
        {
            return Direction.right;
        }
        
        else if (Input.GetAxisRaw("Vertical") == 1)
        {
            return Direction.up;
        }

        return Direction.nothing;
    }

    void processMovement(Direction direction)
    {
        switch (direction)
        {
            case Direction.nothing:
                block.velocity = Vector2.zero;
                break;

            case Direction.left:
                //there's no transform.left so i had to
                //scalar multiply transform.right by -1 to 
                //get a transfrom.left

                //block.AddForce(transform.right * -1 * thrust);
                block.velocity = Vector2.left;
                break;

            case Direction.right:
                //block.AddForce(transform.right * thrust);
                block.velocity = Vector2.right;
                break;

            case Direction.up:
                //block.AddForce(transform.up * thrust);
                block.velocity = Vector2.up;
                break;
        }
    }
}
