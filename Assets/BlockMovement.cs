using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    public Vector2 targetPosition;
<<<<<<< HEAD
    public float speed = 3f;
    public bool movable = true;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
    }
=======
    public float speed = 2f;
    public float up = 0;
    public float side = 0;
>>>>>>> 1858161f41fc479d00aee17542eab1ca729a6710

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

    void OnCollisionStay2D(Collision2D other)
    {
        targetPosition = new Vector2(Mathf.Round(targetPosition.x), Mathf.Round(targetPosition.y * 2f) * 0.5f);
        movable = false;
    }
}
