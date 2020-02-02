using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    //Spawns the next faller
    //The vector generator is slightly different...
    //It generates only vectors at the bottom of the screen and below...

    //positive values force the blocks down...
    public int yOffset = 0;
    public int blocksAvailable = 12;
    public List<GameObject> blocksArray = new List<GameObject>();
    Camera mainCamera;

    private void Start()
    {
        //spawn the first block
        spawnBlock();
    }

    public void spawnBlock()
    {
        //pick a random block from the array
        int index = Random.Range(0, blocksAvailable);

        //Quaternion.identity means no rotation
        GameObject.Instantiate(blocksArray[index], generateVector(), Quaternion.identity);
    }

    Vector3 generateVector()
    {
        mainCamera = Camera.main;

        //the left edge of the screen, and the bottom of the screen
        int leftCorner = Mathf.CeilToInt( mainCamera.ScreenToWorldPoint(Vector3.zero).x );
        int screenBottom = Mathf.CeilToInt(mainCamera.ScreenToWorldPoint(Vector3.zero).y);
        
        //the right edge of the screen
        int rightCorner = Mathf.CeilToInt(mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.pixelWidth, 0)).x) + 1;

        //create a vector that is below the frustum
        int x = Random.Range(leftCorner, rightCorner);
        return new Vector3(x, screenBottom - yOffset);
    }
}
