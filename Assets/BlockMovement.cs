using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    public Vector2 targetPosition;
    public float speed = 2f;
    public float up = 0;
    public float side = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            side += 0.5f;
            // targetPosition = transform.position + new Vector3(0.5f, 0, 0);
            // targetPosition = new Vector2(Mathf.Ceil(targetPosition.x), Mathf.Ceil(targetPosition.y));
        }

        if (Input.GetKey(KeyCode.A))
        {
            side += -0.5f;
            // targetPosition = transform.position + new Vector3(-0.5f, 0, 0);
            // targetPosition = new Vector2(Mathf.Floor(targetPosition.x), Mathf.Ceil(targetPosition.y));
        }

        if (Input.GetKey(KeyCode.W))
        {
            up += 0.5f;
        }

        // targetPosition = transform.position + new Vector3(0, 0.1f, 0);

        targetPosition = transform.position + new Vector3(side, up, 0);
        targetPosition = new Vector2(Mathf.Floor(targetPosition.x), Mathf.Ceil(targetPosition.y));

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
        up = 0;
        side = 0;
    }

    void OnCollisionStay2D(Collision2D other)
    {
        targetPosition = transform.position;
    }
}
