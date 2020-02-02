using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public GameObject villager;
    Transform raycastShooter;
    Rigidbody2D villagerBody;
    

    Vector3 moveRight;

    RaycastHit2D blockInFront;

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        //walk forward one unit at a time
        //walk backward if its at a pit...

        //basically check to see if there is a block right in front...

        moveRight = new Vector3(0.5f, 0f);
        raycastShooter = villager.GetComponent<RayCastOrigin>();

        Debug.DrawRay(villager.transform.position, moveRight, Color.cyan, 1, false);
        blockInFront = Physics2D.Raycast(villager.transform.position, moveRight, 1.0f);
        print(blockInFront.collider);
        villagerBody = villager.GetComponent<Rigidbody2D>();



        if (blockInFront.collider == null)
        {
            villagerBody.velocity = moveRight;
        }
        /*
        //try to make it so that it just moves left forever after it hits something in front of it...
        else
        {
            villagerBody.velocity = Vector2.left;
        }
        */
    }
}
