using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    public Vector2 targetPosition;
    public float speed = 3f;
    public bool movable = true;

    GameObject spawner;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (movable)
        {
            if (Input.GetKey(KeyCode.D))
            {
                targetPosition = transform.position + new Vector3(0.5f, 0, 0);
                targetPosition = new Vector2(Mathf.Ceil(targetPosition.x), targetPosition.y);
            }

            else if (Input.GetKey(KeyCode.A))
            {
                targetPosition = transform.position + new Vector3(-0.5f, 0, 0);
                targetPosition = new Vector2(Mathf.Floor(targetPosition.x), targetPosition.y);
            }

            targetPosition = new Vector2(targetPosition.x, targetPosition.y + .03f);
        }
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);

        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (movable)
        {
            targetPosition = new Vector2(Mathf.Round(targetPosition.x), Mathf.Round(targetPosition.y * 2f) * 0.5f);
            movable = false;

            //spawn a new block


            spawner = GameObject.Find("BlockSpawner");
            spawner.GetComponent<BlockSpawner>().spawnBlock();
        }
    }
}
