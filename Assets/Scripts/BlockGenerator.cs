using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    // Spawns the base blocks needed to create the bridge...

    public int maxNumberOfBlocks;

    //  Sets the inclusive y range that a block can generate in...
    public int lowerHeightBound;
    public int upperHeightBound;

    public GameObject block;
    Vector3 spawnPoint;

    //this is needed to get the right edge of the screen in world coordinates
    Camera mainCamera;

    void Start()
    {
        //for each block, generate a vector...
        for (int i = 0; i < maxNumberOfBlocks; ++i)
        {
            //randomly pick a block from a selection and instantiate it
            spawnPoint = generateVector();
           
            Instantiate(block, spawnPoint, Quaternion.identity);
        }
    }

    Vector3 generateVector()
    {
        mainCamera = Camera.main;

        int x = Random.Range(Mathf.CeilToInt(mainCamera.ScreenToWorldPoint(new Vector3(0, 0)).x), 
            Mathf.CeilToInt(mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.pixelWidth, 0)).x));

        int y = Random.Range(lowerHeightBound, upperHeightBound + 1);

        Vector3 spawnPoint = new Vector3(x, y);

        //check to make sure that there isn't a collider there...
        while (Physics2D.OverlapPoint(spawnPoint))
        {
            x = Random.Range(1, Mathf.CeilToInt(mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.pixelWidth, 0)).x));
            y = Random.Range(lowerHeightBound, upperHeightBound);
            spawnPoint = new Vector3(x, y);
        }

        return spawnPoint;
    }
    


}
