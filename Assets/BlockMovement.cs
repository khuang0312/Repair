using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    public Vector2 targetPosition;
    public float speed = 2f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            targetPosition = transform.position + new Vector3(0.5f, 0, 0);
            targetPosition = new Vector2(Mathf.Ceil(targetPosition.x), Mathf.Ceil(targetPosition.y));
        }
        else if (Input.GetKey(KeyCode.A))
        {
            targetPosition = transform.position + new Vector3(-0.5f, 0, 0);
            targetPosition = new Vector2(Mathf.Floor(targetPosition.x), Mathf.Ceil(targetPosition.y));
        }

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
    }

    void OnCollisionStay2D(Collision2D other)
    {
        targetPosition = transform.position;
    }
}
