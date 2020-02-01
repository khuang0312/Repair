using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawning : MonoBehaviour
{
    Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        print(mainCamera.pixelRect);
        
        //Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        destroyObjectIfOffScreen();
    }

    void destroyObjectIfOffScreen()
    {
        if (gameObject.GetComponent<SpriteRenderer>().isVisible == false)
        {
            Destroy(gameObject);
        }
    }
}
